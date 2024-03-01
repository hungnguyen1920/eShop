FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["eShop.Api/eShop.Api.csproj", "eShop.Api/"]
COPY ["eShop.Application/eShop.Application.csproj", "eShop.Application/"]
COPY ["eShop.Data/eShop.Data.csproj", "eShop.Data/"]
COPY ["eShop.ViewModels/eShop.ViewModels.csproj", "eShop.ViewModels/"]
COPY ["eShop.Utilities/eShop.Utilities.csproj", "eShop.Utilities/"]

RUN dotnet restore "eShop.Api/eShop.Api.csproj"
RUN dotnet restore "eShop.Data/eShop.Data.csproj"
RUN dotnet restore "eShop.Application/eShop.Application.csproj"
RUN dotnet restore "eShop.ViewModels/eShop.ViewModels.csproj"
RUN dotnet restore "eShop.Utilities/eShop.Utilities.csproj"
COPY . .
WORKDIR "/src/eShop.Api"
RUN dotnet build "eShop.ViewModels.csproj" -c Release -o /app/build
RUN dotnet build "eShop.Data.csproj" -c Release -o /app/build
RUN dotnet build "eShop.Utilities.csproj" -c Release -o /app/build
RUN dotnet build "eShop.Application.csproj" -c Release -o /app/build
RUN dotnet build "eShop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShop.Api.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShop.Api.dll"]
