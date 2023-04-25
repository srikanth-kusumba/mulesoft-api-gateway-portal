using ControlPlane.Data;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using BlazorDateRangePicker;
using System.Collections.Generic;
using System;
using ControlPlane.IntegrationServices.AnyPointPlatform;


namespace ControlPlane
{
    public class Startup
    {

        public static string AWS_ARN { get; private set; }

        public static string AnyPoint_Authorization_Endpoint { get; private set; }
        public static string AnyPoint_Token_Endpoint { get; private set; }
        public static string AnyPoint_Domain { get; private set; }

        public static string AnyPoint_MasterOrganizationId { get; private set; }
        public static string AnyPoint_Organization_Id { get; private set; }
        
        public static string AnyPoint_BusinessGroup_Id { get; private set; }
        public static string AnyPoint_Environment_Id { get; private set; }
        public static string AnyPoint_RTF_Target_Id{ get; private set; }
        public static string AnyPoint_RTF_Target_Name { get; private set; }

        public static string Tool_Environment { get; private set; }

        public static string CloudWatch_LogGroup { get; private set; }

        public static string AnyPoint_ConnectedApp_ClientId { get; private set; }
        public static string AnyPoint_ConnectedApp_ClientSecret { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // ******
            // BLAZOR COOKIE Auth Code (begin)
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            // BLAZOR COOKIE Auth Code (end)
            // ******

            services.AddRazorPages();

            //services.AddServerSideBlazor();
            services.AddServerSideBlazor().AddCircuitOptions(o =>
            {
                o.DetailedErrors = true;
            });

            //Date Range Picker
            services.AddDateRangePicker(config =>
            {
                config.Attributes = new Dictionary<string, object>
                    {
                        { "class", "form-control form-control-sm" }
                    };
            });

            services.AddSingleton<APIManagementService>();
            services.AddSingleton<PlatformAPIService>();

            services.AddOptions();
            services.AddAuthorizationCore();

            //services.AddBlazoredSessionStorage(config => config.JsonSerializerOptions.WriteIndented = true);
            services.AddBlazoredSessionStorage();


            services.AddSwaggerGen().AddControllers();

            // ******
            // BLAZOR COOKIE Auth Code (begin)
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();

            //Implemented by Srikanth  => for Custom Authentication
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            services.AddSignalR();

            //services.AddSignalR().AddAzureSignalR(options =>
            //{
            //    options.ServerStickyMode = Microsoft.Azure.SignalR.ServerStickyMode.Required;
            //});

            // BLAZOR COOKIE Auth Code (end)
            // ******
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            //app.UsePathBase("/portal");
            AWS_ARN = Configuration["AWS_ARN"]; //Account ARN for us-west-2

            AnyPoint_Authorization_Endpoint = Configuration["AnyPoint:authorization_endpoint"];
            AnyPoint_Token_Endpoint = Configuration["AnyPoint:token_endpoint"];

            AnyPoint_Domain = Configuration["AnyPoint:Domain"];
            AnyPoint_Organization_Id = Configuration["AnyPoint:MasterOrganizationId"];
            AnyPoint_Organization_Id = Configuration["AnyPoint:OrganizationId"];
            AnyPoint_BusinessGroup_Id = Configuration["AnyPoint:BusinessGroupId"];

            AnyPoint_ConnectedApp_ClientId = Configuration["AnyPoint:ConnectedApp_ClientId"];
            AnyPoint_ConnectedApp_ClientSecret = Configuration["AnyPoint:ConnectedApp_ClientSecret"];

            if (env.IsDevelopment())  //Local or DEV
            {
                app.UseDeveloperExceptionPage();

                //For LOCAL / DEV
                Tool_Environment = env.EnvironmentName;
                AnyPoint_Authorization_Endpoint = Configuration["AnyPoint:DEV:authorization_endpoint"];
                AnyPoint_Token_Endpoint = Configuration["AnyPoint:DEV:token_endpoint"];
                AnyPoint_Environment_Id = Configuration["AnyPoint:DEV:env_id"];
                AnyPoint_RTF_Target_Id = Configuration["AnyPoint:DEV:rtf_target_id"];
                AnyPoint_RTF_Target_Name = Configuration["AnyPoint:DEV:rtf_target_name"];
              
                CloudWatch_LogGroup = Configuration["CloudWatch:Dev:LogGroup"];
            }
            else if (env.IsStaging()) //Beanstalk Dev environment
            {
                app.UseDeveloperExceptionPage();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                //For EB Dev Environment
                Tool_Environment = env.EnvironmentName;
               
                AnyPoint_Authorization_Endpoint = Configuration["AnyPoint:QA:authorization_endpoint"];
                AnyPoint_Token_Endpoint = Configuration["AnyPoint:QA:token_endpoint"];
                AnyPoint_Environment_Id = Configuration["AnyPoint:QA:env_id"];
                AnyPoint_RTF_Target_Id = Configuration["AnyPoint:QA:rtf_target_id"];
                AnyPoint_RTF_Target_Name = Configuration["AnyPoint:QA:rtf_target_name"];


                //Tool_Environment = Environment.GetEnvironmentVariable("EB_Environment") ?? "";
                CloudWatch_LogGroup = Configuration["CloudWatch:Dev:LogGroup"];
            }
            else //Production
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                //For SERVER
                Tool_Environment = env.EnvironmentName;
              
                AnyPoint_Authorization_Endpoint = Configuration["AnyPoint:PROD:authorization_endpoint"];
                AnyPoint_Token_Endpoint = Configuration["AnyPoint:PROD:token_endpoint"];
                AnyPoint_Environment_Id = Configuration["AnyPoint:PROD:env_id"];
                AnyPoint_RTF_Target_Id = Configuration["AnyPoint:PROD:rtf_target_id"];
                AnyPoint_RTF_Target_Name = Configuration["AnyPoint:PROD:rtf_target_name"];

                //Tool_Environment = Environment.GetEnvironmentVariable("EB_Environment") ?? "";
                CloudWatch_LogGroup = Configuration["CloudWatch:Pro:LogGroup"];
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ******
            // BLAZOR COOKIE Auth Code (begin)
            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            // BLAZOR COOKIE Auth Code (end)
            // ******

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
