using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Smaple01;
using Smaple01.Application.Repositories;
using Smaple01.DBContexts;
using Smaple01.Services.MailService;
using Smaple01.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
  .AddXmlDataContractSerializerFormatters();


//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseSerilog();

//builder.Services.AddProblemDetails( options =>
//{
//    options.CustomizeProblemDetails = ctx =>
//    {
//        ctx.ProblemDetails.Extensions.Add("Additional Info", "Additional Info Example");
//        ctx.ProblemDetails.Extensions.Add("Server", Environment.MachineName);
//    };
//});
builder.Services.AddProblemDetails();

#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

builder.Services.AddSingleton<CitiesDataStore>();
builder.Services.AddDbContext<CityInfoContext>(dbContextOptions =>
dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:CityInfoDbConnectionSting"])
);

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IPointOfInterestRepository, PointOfInterestRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
               Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustbefromNewYork", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("city", "NewYork");
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.SubstituteApiVersionInUrl = true;
    options.GroupNameFormat = "'v'VVV";
});

builder.Services.AddSwaggerGen(options =>
{
    var xmlCommentsFile =
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlCommentsFullPath =
        Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    options.IncludeXmlComments(xmlCommentsFullPath);
});

builder.Services.AddTransient<
    IConfigureOptions<SwaggerGenOptions>,
    ConfigureSwaggerOptions>();


var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";

        var provider = app.Services
            .GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//app.Run(async (context)=>
//{
//    await context.Response.WriteAsync("Hello World");
//});
app.Run();