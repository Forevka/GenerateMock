FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GenerateMock.WebUI/GenerateMock.WebUI.csproj", "GenerateMock.WebUI/"]
RUN dotnet restore "GenerateMock.WebUI/GenerateMock.WebUI.csproj"
COPY . .
WORKDIR "/src/GenerateMock.WebUI"
RUN dotnet build "GenerateMock.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GenerateMock.WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GenerateMock.WebUI.dll"]