using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PocDoctor.TokenAuth;
using Microsoft.IdentityModel.Tokens;
using PocDoctor.Repositiries;
using PocDoctor.Entitiess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PocDoctor
{
    public class Startup
    {
        const string TokenAudience = "ExampleAudience";
        const string TokenIssuer = "ExampleIssuer";

        private TokenAuthOptions tokenOptions;

        private const String SecretKey = "Abcdefghijklmenaaskjdakbaksjdbaksjbdakjsdbkajbfdkjb";
        private readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(SecretKey));


        public void ConfigureServices(IServiceCollection services)
        {
            tokenOptions = new TokenAuthOptions()
            {
                Audience = TokenAudience,
                Issuer = TokenIssuer,
                SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256Signature)
            };


            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddSingleton<TokenAuthOptions>(tokenOptions);

            services.AddSingleton<DatabaseRepo>();

            services.AddSingleton<ApplicationContext>();

            services.AddMvc();

        }


        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = SigningKey,
                    ValidAudience = tokenOptions.Audience,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.FromMinutes(0)
                }
            });

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
