# CashFlow
## Pendências
- RabbitMQ
  - Servidor em docker com roteiro de instalação.
  - Send message no Service
  - Receive message no WorkService


# Rodando a Aplicação

## Banco Postgres

docker pull postgres

md "C:\PostgreSQL"

docker run --name teste-postgres -e "POSTGRES_PASSWORD=Postgres2018!" -p 5432:5432 -v "C:\PostgreSQL":/var/lib/postgresql/data -d postgres

// ConnectionString:
//      User ID=postgres;Password=Postgres2018!;Host=localhost;Port=5432;Database=postgres;Pooling=false
