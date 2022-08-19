var builder = WebApplication.CreateBuilder(args);

builder.AddLxhCommonServer(opts => 
{
    opts.UseAll = false;
    opts.UseGrpcServer = false;
    opts.UseAllFilter = true;
    opts.UseIOC = true;
    opts.WebPort = 9998;
    opts.GrpcPort = 9999;
    opts.NameSpace = null;
    opts.FilterSpace = null;
    opts.GrpcSpace = null;
    opts.IOCSpace = null;
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
app.UseLxhCommon();
app.Run();
