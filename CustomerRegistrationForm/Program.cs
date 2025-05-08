using CustomerRegistrationForm.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors ( options =>
{
    options.AddPolicy ( "AllowReactApp" ,
        builder =>
        {
            builder.WithOrigins ( "http://localhost:5173" ) 
                   .AllowAnyHeader ( )
                   .AllowAnyMethod ( );
        } );
} );


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc ( "v1" , new OpenApiInfo
        {
        Title = "Customer Registration API" ,
        Version = "v1" ,
        Description = "API for managing customer personal and employment details"
        } );
} );

builder.Services.AddSingleton<CustomerRepository> ( new CustomerRepository (
    builder.Configuration.GetConnectionString ( "DefaultConnection" ) ) );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c => c.SwaggerEndpoint ( "/swagger/v1/swagger.json" , "Customer Registration API v1" ) );
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors ( "AllowReactApp" );


app.MapControllers();

app.Run();
