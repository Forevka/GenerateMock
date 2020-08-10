FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GenerateMock.WebApi/GenerateMock.WebApi.csproj", "GenerateMock.WebApi/"]
RUN dotnet restore "GenerateMock.WebApi/GenerateMock.WebApi.csproj"
COPY . .
WORKDIR "/src/GenerateMock.WebApi"
RUN dotnet build "GenerateMock.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GenerateMock.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GenerateMock.WebApi.dll"]