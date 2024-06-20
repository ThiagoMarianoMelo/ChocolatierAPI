using Chocolatier.API.Configurations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureDataBase(builder.Configuration);


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
