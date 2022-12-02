FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
COPY ["src/Api/Template.Api.csproj", "src/Api/"]
COPY ["src/Domain/Template.Domain.csproj", "src/Domain/"]
COPY ["src/Core/Template.Core.csproj", "src/Core/"]
COPY ["src/Data/Template.Data.csproj", "src/Data/"]
RUN dotnet restore "src/Api/Template.Api.csproj"
COPY . .
WORKDIR /src/Api
RUN ls
RUN dotnet build "Template.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Template.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Template.Api.dll"]
