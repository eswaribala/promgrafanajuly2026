using InventoryAPI.Contexts;
using InventoryAPI.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json.Converters;
using InventoryAPI.Configurations;
using GraphQL.Server;
using InventoryAPI.Schemas;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
//using Serilog.Sinks.Elasticsearch;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddControllers(options =>
//{
//    options.RespectBrowserAcceptHeader = true;
//    options.ReturnHttpNotAcceptable = true;
//})
//    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
//    .AddXmlSerializerFormatters()

//.AddMvcOptions(options => options.OutputFormatters.Add(new CsvOutputFormatter()));



builder.Services.AddDbContext<InventoryContext>(options =>
options.UseSqlServer(configuration.
GetConnectionString("InvConn")));

builder.Services.AddDbContext<InventoryIdentityContext>(options =>
options.UseSqlServer(configuration.
GetConnectionString("IdentityInvConn")));

builder.Services.AddTransient<ICategoryRepo, CategoryRepo>();
builder.Services.AddTransient<IProductRepo, ProductRepo>(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//swagger authorize
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
//builder.Services.ConfigureOptions<SwaggerConfiguration>();

//builder.Services.AddApiVersioning(x =>
//{
//    x.DefaultApiVersion = new ApiVersion(1, 0);
//    x.AssumeDefaultVersionWhenUnspecified = true;
//    x.ReportApiVersions = true;
//    x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
//});
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
var policyName = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                            .WithOrigins("http://localhost:*", "")
                             //.WithOrigins("http://localhost:4200")
                             // specifying the allowed origin
                             // .WithMethods("GET") // defining the allowed HTTP method
                             //.AllowAnyOrigin()
                             // .WithHeaders(HeaderNames.ContentType, "ApiKey")
                             .AllowAnyMethod()
                            .AllowAnyHeader(); // allowing any header to be sent
                      });
});

builder.Services.AddScoped<InventorySchema>();
builder.Services.AddGraphQL()
               .AddSystemTextJson()
               .AddGraphTypes(typeof(InventorySchema), ServiceLifetime.Scoped);
//builder.Services.AddTransient<IProductPublisher, ProductPublisher>();

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<InventoryIdentityContext>()
    .AddDefaultTokenProviders();

//Dictionary<string, Object> secretData = new VaultConfiguration(configuration)
//             .GetSecret().Result;
var SecretData = configuration["Secret"];

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Add services to the container.

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretData))
    };
});

//elastic log
//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()

//    .WriteTo.Debug()
//    .WriteTo.Console()
//    .WriteTo.EventCollector("http://localhost:8088/services/collector/event", "6383193b-2e4d-4e9c-b5e3-ca0802c3e42")
//    //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new
//    //    Uri(configuration["ElasticConfiguration:Uri"]))
//    //{
//    //    AutoRegisterTemplate = true,
//    //    IndexFormat = $"InventoryIndex-{DateTime.UtcNow:yyyy-MM}"
//    //})
//    //.Enrich.WithProperty("Environment", environment)
//    //.Enrich.WithProperty("ApplicationName", "Inventory")
//   // .Enrich.WithProperty("Data", "Product Details")
//   // .ReadFrom.Configuration(configuration)
//    .CreateLogger();
Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.EventCollector(
                    "http://localhost:8088",
                    "413f75e1-3f11-4be4-bc93-2e8fce0e5a7b")
               //.Enrich.WithProperty("Serilog.Sinks.Splunk.Sample", "ViaEventCollector")
               //.Enrich.WithProperty("Serilog.Sinks.Splunk.Sample.TestType", "AddCustomFields")
               .CreateLogger();

builder.Host.UseSerilog();



var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}
app.UseGraphQL<InventorySchema>();
app.UseGraphQLPlayground(options: new PlaygroundOptions());

app.UseHttpsRedirection();
app.UseCors(policyName);

app.UseAuthentication();
app.UseAuthorization();
app.UseMetricServer();
app.UseHttpMetrics();
app.MapControllers();
app.UseSwagger();

app.Run();
