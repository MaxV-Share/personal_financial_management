#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PersonalFinancialManagement.API/PersonalFinancialManagement.API.csproj", "PersonalFinancialManagement.API/"]
COPY ["PersonalFinancialManagement.Services.Excels/PersonalFinancialManagement.Services.Excels.csproj", "PersonalFinancialManagement.Services.Excels/"]
COPY ["PersonalFinancialManagement.Services/PersonalFinancialManagement.Services.csproj", "PersonalFinancialManagement.Services/"]
COPY ["PersonalFinancialManagement.EFCore/PersonalFinancialManagement.EFCore.csproj", "PersonalFinancialManagement.EFCore/"]
COPY ["PersonalFinancialManagement.Common/PersonalFinancialManagement.Common.csproj", "PersonalFinancialManagement.Common/"]
COPY ["PersonalFinancialManagement.Common.Model/PersonalFinancialManagement.Common.Models.csproj", "PersonalFinancialManagement.Common.Model/"]
COPY ["PersonalFinancialManagement.Repositories/PersonalFinancialManagement.Repositories.csproj", "PersonalFinancialManagement.Repositories/"]
COPY ["PersonalFinancialManagement.Models/PersonalFinancialManagement.Models.csproj", "PersonalFinancialManagement.Models/"]
RUN dotnet restore "PersonalFinancialManagement.API/PersonalFinancialManagement.API.csproj"
COPY . .
WORKDIR "/src/PersonalFinancialManagement.API"
RUN dotnet build "PersonalFinancialManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalFinancialManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalFinancialManagement.API.dll"]