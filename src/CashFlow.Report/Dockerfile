#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CashFlow.Report/CashFlow.Report.csproj", "CashFlow.Report/"]
RUN dotnet restore "CashFlow.Report/CashFlow.Report.csproj"
COPY . .
WORKDIR "/src/CashFlow.Report"
RUN dotnet build "CashFlow.Report.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashFlow.Report.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashFlow.Report.dll"]