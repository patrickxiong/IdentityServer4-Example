using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "cookie";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("cookie")
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                    options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5000";
                    options.ClientId = "oidcClient";
                    options.ClientSecret = "SuperSecretPassword";

                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.ResponseMode = "query";

                    // options.CallbackPath = "/signin-oidc"; // default redirect URI
                    
                    // options.Scope.Add("oidc"); // default scope
                    // options.Scope.Add("profile"); // default scope
                    options.Scope.Add("api1.read");
                    options.SaveTokens = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
