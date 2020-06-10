using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Cosmos;
using gerappa_test01_api.Data;
using gerappa_test01_api.Models;

namespace gerappa_test01_api
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
            services.AddCors();
            services.AddControllers();
            services.AddSingleton<ICosmosClientProvider>(InitializeCosmosClientInstanceAsync(Configuration).GetAwaiter().GetResult());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(config =>
            {
                config.WithOrigins("http://localhost:3000");
                config.AllowAnyHeader();
                config.AllowAnyMethod();

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

        private static async Task<CosmosClientProvider> InitializeCosmosClientInstanceAsync(IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection("CosmosDb");
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string keyName = configurationSection.GetSection("KeyName").Value;
            string key = configuration[keyName];


            Microsoft.Azure.Cosmos.Fluent.CosmosClientBuilder clientBuilder = new Microsoft.Azure.Cosmos.Fluent.CosmosClientBuilder(account, key);
            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()
                                .WithSerializerOptions(new CosmosSerializationOptions()
                                {
                                    IgnoreNullValues = true,
                                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                                })
                                .Build();

            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(typeof(Client).Name.ToLower(), "/id");
            await database.Database.CreateContainerIfNotExistsAsync(typeof(Pizza).Name.ToLower(), "/id");
            await database.Database.CreateContainerIfNotExistsAsync(typeof(Order).Name.ToLower(), "/id");

            return new CosmosClientProvider(client, databaseName);
        }
    }
}
