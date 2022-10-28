docker pull postgres

md "C:\PostgreSQL"

docker run --name teste-postgres -e "POSTGRES_PASSWORD=Postgres2018!" -p 5432:5432 -v "C:\PostgreSQL":/var/lib/postgresql/data -d postgres