var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.AddLxhCommonServer(opts =>
{
    opts.UseAll = true;
    opts.UseGrpcServer = false;
    opts.UseAllFilter = false;
    opts.UseIOC = true;
    opts.UseDynamicApi = true;
    //opts.WebPort = 9998;
    //opts.GrpcPort = 7101;
    opts.NameSpace = null;
    opts.FilterSpace = null;
    opts.GrpcSpace = null;
    opts.IOCSpace = null;
});
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
