using BitSpy.Api;
using BitSpy.Api.Endpoints.Internal;
using BitSpy.Api.Middleware;
using BitSpy.Api.Repositories;
using BitSpy.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IMetricService, MetricService>();
builder.Services.AddScoped<ITraceService, TraceService>();

builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IMetricRepository, MetricRepository>();
builder.Services.AddScoped<ITraceRepository, TraceRepository>();
builder.Services.AddScoped<ILongTermTraceRepository, LongTermTraceRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

await DatabaseInitializer.InitializeAsync(builder.Configuration);

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors("AllowAllOrigins");
app.UseSwagger();
app.UseSwaggerUI();
app.UseEndpoints();
app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin();
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
});
app.Run();