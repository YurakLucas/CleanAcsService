# CleanAcsService

Este projeto é uma Web API em .NET Core que demonstra uma integração com os [Azure Communication Services (ACS)](https://azure.microsoft.com/services/communication-services/), utilizando os conceitos de Clean Code e Domain-Driven Design (DDD). A API expõe endpoints para envio de SMS, mensagens de Chat e emails.

## Estrutura do Projeto

A solução está organizada em múltiplos projetos, cada um representando uma camada da arquitetura:
```
CleanAcsService/
├── .gitignore
├── CleanAcsService.sln
├── src/
│ ├── CleanAcsService.API/ // Projeto da API (apresentação) 
│ │ ├── Controllers/
│ │ │ ├── SmsController.cs 
│ │ │ ├── ChatController.cs 
│ │ │ └── EmailController.cs 
│ │ ├── Program.cs
│ │ └── appsettings.json
│ ├── CleanAcsService.Application/ // Camada de aplicação (DTOs e Interfaces) 
│ │ ├── DTOs/ 
│ │ │ ├── SendSmsRequest.cs 
│ │ │ ├── SendChatMessageRequest.cs 
│ │ │ └── SendEmailRequest.cs
│ │ └── Interfaces/
│ │ ├── ISmsService.cs
│ │ ├── IChatService.cs
│ │ └── IEmailService.cs
│ ├── CleanAcsService.Domain/ // Camada de domínio (Entidades)
│ │ └── Entities/ 
│ │ ├── SmsMessage.cs
│ │ ├── ChatMessage.cs 
│ │ └── EmailNotification.cs
│ └── CleanAcsService.Infrastructure/ // Camada de infraestrutura (implementações dos serviços ACS)
│ └── Services/
│ ├── AcsSmsService.cs 
│ ├── AcsChatService.cs 
│ └── AcsEmailService.cs
└── tests/
└── CleanAcsService.API.Tests/ // Projeto de testes unitários
├── SmsControllerTests.cs
├── ChatControllerTests.cs 
└── EmailControllerTests.cs
```


## Pré-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download) (ou versão compatível)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou outro IDE de sua preferência
- Pacotes NuGet utilizados:
  - `Azure.Communication.Sms`
  - `Azure.Communication.Chat`
  - `Azure.Communication.Email`
  - `Swashbuckle.AspNetCore` (para Swagger)
  - `xUnit`, `Moq` (para testes unitários)

## Configuração e Variáveis de Ambiente

A API utiliza variáveis de ambiente para armazenar dados sensíveis, como chaves e connection strings. No arquivo `launchSettings.json` (localizado em `src/CleanAcsService.API/Properties/`), 

Endpoints Disponíveis

1. SMS
Endpoint: POST /api/sms/send

Descrição: Envia um SMS utilizando o Azure Communication Services.

Exemplo de Payload:
{
  "destination": "+11234567890",
  "content": "Conteúdo da mensagem SMS"
}

2. Chat
Endpoint: POST /api/chat/send

Descrição: Envia uma mensagem de chat para um thread especificado.

Exemplo de Payload:
{
  "threadId": "identificador-do-thread",
  "sender": "Nome do Remetente",
  "content": "Conteúdo da mensagem de chat"
}

3. Email
Endpoint: POST /api/email/send

Descrição: Envia um email utilizando o Azure Communication Services.

Exemplo de Payload:
{
  "to": "destinatario@dominio.com",
  "subject": "Assunto do Email",
  "body": "Conteúdo do email"
}

Executando os Testes Unitários
Os testes unitários estão localizados na pasta tests/CleanAcsService.API.Tests. Para executá-los, utilize o comando:

dotnet test

Você também pode utilizar o Test Explorer do Visual Studio para rodar e visualizar os resultados dos testes.

Tecnologias e Conceitos Utilizados
.NET Core & C#: Desenvolvimento da Web API.
Clean Code & DDD: Estruturação da aplicação em camadas (Domain, Application, Infrastructure e API).
Azure Communication Services (ACS): Integração com serviços de SMS, Chat e Email.
Swagger: Documentação e teste dos endpoints.
xUnit e Moq: Frameworks para testes unitários.

Licença
Este projeto está licenciado sob a MIT License.





