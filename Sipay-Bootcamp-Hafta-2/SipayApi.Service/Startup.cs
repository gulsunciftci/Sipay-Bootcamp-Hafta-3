using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SipayApi.DataAccess.ApplicationDbContext;
using SipayApi.DataAccess.Repository;
using SipayApi.DataAccess.Unitofw;
using SipayApi.Schema;

namespace SipayApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sipay Api Collection", Version = "v1" });
        });


        ////dbcontext
        var dbType = Configuration.GetConnectionString("DbType");

        //if (dbType == "PostgreSql")
        //{
        //    var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
        //    services.AddDbContext<SimDbContext>(opts =>
        //    opts.UseNpgsql(dbConfig));

            //}
        if (dbType == "Default")
        {
            services.AddDbContext<SimDbContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
        }


        services.AddScoped<IUnitOfWork, UnitOfWork>();
       



        //Mapper'� services k�sm�na Dependency injection ile register ettim
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        services.AddSingleton(config.CreateMapper());

    }

  
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sipay v1"));
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
