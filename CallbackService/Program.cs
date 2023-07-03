using CallbackService;
using CallbackService.JobManager;
using CallbackService.Repository;
using CallbackService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<NotificationService>();
builder.Services.AddHostedService<NotificationSendJob>();
builder.Services.AddSingleton<SmsClient>();
builder.Services.AddTransient<IDatabaseService, DatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();