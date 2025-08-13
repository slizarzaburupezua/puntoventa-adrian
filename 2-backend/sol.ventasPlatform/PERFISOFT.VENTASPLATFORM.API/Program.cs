using FluentValidation.AspNetCore;
using PERFISOFT.VENTASPLATFORM.API.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});

// Configuración de  
builder.Services.AddCors(options =>
{
    //DEV   
    options.AddPolicy("NewPolitic", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });

    //PROD
    //options.AddPolicy(name: MyAllowSpecificOrigins,
    //              policy =>
    //              {
    //                  policy.WithOrigins("https://xxxxxxxx.com")
    //                        .AllowAnyHeader()
    //                        .AllowAnyMethod();
    //              });

});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

#region SwaggerConf
builder.Services.SetSwaggerConfig(builder.Configuration);
#endregion

#region JWT
builder.Services.SetJwtConfig(builder.Configuration);
#endregion

#region Context SQLSERVER
builder.Services.SetDBConnection(builder.Configuration);
#endregion

#region Dependency Injection
builder.Services.SetInjection(builder.Configuration);
#endregion

#region Automapper
builder.Services.SetAutoMapper(builder.Configuration);
#endregion

#region HttpClient
builder.Services.SetHttpClientConfig(builder.Configuration);
#endregion

#region Logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("log/VENTASLog.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
#endregion

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// PROD
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(MyAllowSpecificOrigins); // PROD
  app.UseCors("NewPolitic"); //DEV

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
