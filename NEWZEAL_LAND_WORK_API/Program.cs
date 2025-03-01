using Microsoft.EntityFrameworkCore;
using NEWZEAL_LAND_WORK_API.Data;
using NEWZEAL_LAND_WORK_API.MapConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHttpClient();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZwalksDbcontext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("NzWalksConnectionStrings")));

builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
