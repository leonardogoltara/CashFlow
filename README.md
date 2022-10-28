# CashFlow
Aplicação de fluxo de caixa construida na arquitetura de microserviço em conjunto com as boas praticas de desenvolvimento, código limpo e os principios do SOLID.

## Desenho da arquitetura
![image](https://user-images.githubusercontent.com/3799361/198635042-23b5c12e-1088-4b2a-9a4c-31e0ad779659.png)

## Padrões de microserviço utilizados
- Shared database
- Messaging
- Externalized configuration

## Tecnologias e bibliotecas usadas
- .NET 6
- EntityFramework Core
- Postgres
- RabbitMQ
- AutoMapper
- Swagger

## Execução do projeto
### Obg: É necessario ter o docker instalado
- Executar os arquivos postgres.bat e rabbitmq.bat dentro da pasta "\doc"
- Executar os projetos no Visual Studio.
- Utilizar a collection do postman em docs para testar api em "\doc\CashFlow.postman_collection.json"

## Testes
- Testes unitários para validar as regras de negócio.
