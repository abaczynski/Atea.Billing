using Billing.Api.Mappings;
using Billing.Core.Interfaces;
using Billing.Core.Services;
using Billing.HttpHandlers.PaymentGateway.Paypal;
using Billing.HttpHandlers.PaymentGateway.PayU;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Billing.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Billing.Api", Version = "v1" });
            });

            services.AddAutoMapper(typeof(OrderProfile));

            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IGatewayProvider, PaymentGatewayProvider>();
            services.AddScoped<IPaymentGateway, PaypalPaymentGateway>();
            services.AddScoped<IPaymentGateway, PayUPaymentGateway>();
            services.AddScoped<IReceiptBuilder, ReceiptBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Billing.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
