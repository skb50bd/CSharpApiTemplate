FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Template.Api/Template.Api.csproj", "Template.Api/"]
COPY ["Template.Domain/Template.Domain.csproj", "Template.Domain/"]
COPY ["Template.Core/Template.Core.csproj", "Template.Core/"]
COPY ["Template.Data/Template.Data.csproj", "Template.Data/"]
RUN dotnet restore "Template.Api/Template.Api.csproj"
COPY . .
WORKDIR "/src/Template.Api"
RUN dotnet build "Template.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Template.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Template.Api.dll"]
