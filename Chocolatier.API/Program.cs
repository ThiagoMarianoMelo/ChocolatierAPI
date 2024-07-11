using Chocolatier.API.Configurations;
using Chocolatier.API.Profiles;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureDataBase(builder.Configuration);
builder.Services.ConfigureMediator();
builder.Services.AddAutoMapper(typeof(RequestToDomainProfile));
builder.Services.AddIdentityConfiguration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SyncMigrations();

app.Run();
