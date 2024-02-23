//--------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="IEEG"> 
// Copyright (c) IEEG Instituto Electoral del Estado de Guanajuato. All rights reserved. 
// </copyright> 
//--------------------------------------------------------------------------------------

using TestingBack.SERVICE.AutoMapperProfile.NombreProyecto;
using TestingBack.SERVICE.Service.NombreProyecto;
using TestingBack.INFRASTRUCTURE.UnitOfWork;
using TestingBack.INFRASTRUCTURE.Context;
using TestingBack.CORE.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using System.Reflection;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerUI;
using Amazon.S3;
using TestingBack.SERVICE.Service.AWS;
using Amazon.Runtime;

namespace TestingBack.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {

        //Si se hace el deploy a producci�n a trav�s de las variables de entorno se detecta que cadena de conexi�n tomar

        string connectionString = Configuration.GetConnectionString("Connection");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy(name: "Cors",
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Testing Api",
                    Version = "v1",
                    Description = "Documentaci�n de la API",
                    Contact = new OpenApiContact
                    {
                        Name = "INSG",
                        Email = "notificaciones@mail.com.mx"
                    },
                    Extensions = new Dictionary<string, IOpenApiExtension>
                    {
                        {"x-logo", new OpenApiObject
                            {
                                {"url", new OpenApiString("https://www.businessintelligence.info/resources/imagenes-bi/adventureworks2008_schema.gif")},
                                {"altText", new OpenApiString("INSG")}
                            }
                        }
                    }
                });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            options.EnableAnnotations();
        });

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(ProjectService), typeof(ProjectService));
        services.AddTransient(typeof(ProductService), typeof(ProductService));
        services.AddTransient(typeof(ProductCategoryService), typeof(ProductCategoryService));
        services.AddTransient(typeof(ProductSubcategoryService), typeof(ProductSubcategoryService));

        #region AWS
        var awsOption = Configuration.GetAWSOptions();
        awsOption.Credentials = new BasicAWSCredentials(Configuration["AwsS3Archivos:AWSAccessKey"], Configuration["AwsS3Archivos:AWSSecretKey"]);
        awsOption.Region = Amazon.RegionEndpoint.USEast1;
        services.AddDefaultAWSOptions(awsOption);

        services.AddAWSService<IAmazonS3>();
        services.AddTransient(typeof(AwsEmailService), typeof(AwsEmailService));
        services.AddTransient(typeof(AwsArchivosService), typeof(AwsArchivosService));
        #endregion

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapperProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Plantilla Base V1");
                options.RoutePrefix = string.Empty;
                options.DocExpansion(DocExpansion.None);
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors(options =>
        {
            options.WithOrigins("*");
            options.AllowAnyMethod();
            options.AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}