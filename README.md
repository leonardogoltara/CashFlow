# CashFlow
## Pendências
- RabbitMQ
  - SendMessage no Service
  - Processa no WorkService
- Implementar o WorkService


# Rodando a Aplicação

## Banco Postgres

docker pull postgres

docker pull dpage/pgadmin4

docker network create postgres-network

md "C:\PostgreSQL"

docker run --name teste-postgres --network=postgres-network -e "POSTGRES_PASSWORD=Postgres2018!" -p 5432:5432 -v "C:\PostgreSQL":/var/lib/postgresql/data -d postgres

docker run --name teste-pgadmin --network=postgres-network -p 15432:80 -e "PGADMIN_DEFAULT_EMAIL=lsgolt94@gmail.com" -e "PGADMIN_DEFAULT_PASSWORD=PgAdmin2018!" -d dpage/pgadmin4

// ConnectionString:
//      User ID=postgres;Password=Postgres2018!;Host=localhost;Port=5432;Database=postgres;Pooling=false
