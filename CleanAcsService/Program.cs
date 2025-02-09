using CleanAcsService.Application.Interfaces;
using CleanAcsService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona as configura��es e vari�veis de ambiente
builder.Configuration.AddEnvironmentVariables();

// Adiciona os servi�os de controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// L� as vari�veis de ambiente necess�rias para os ACS:
string acsConnectionString = builder.Configuration["ACS_CONNECTION_STRING"];
string acsFromPhoneNumber = builder.Configuration["ACS_FROM_PHONE_NUMBER"];
string acsEmailSender = builder.Configuration["ACS_EMAIL_SENDER"];
string acsChatEndpoint = builder.Configuration["ACS_CHAT_ENDPOINT"];
string acsChatToken = builder.Configuration["ACS_CHAT_TOKEN"];

// Registra os servi�os customizados
builder.Services.AddScoped<IChatService>(provider =>
    new AcsChatService(acsChatEndpoint, acsChatToken));

builder.Services.AddScoped<ISmsService>(provider =>
    new AcsSmsService(acsConnectionString, acsFromPhoneNumber));

builder.Services.AddScoped<IEmailService>(provider =>
    new AcsEmailService(acsConnectionString, acsEmailSender));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
