FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Test/Test.csproj Test/
RUN dotnet restore Test/Test.csproj
COPY . .
WORKDIR /src/Test
RUN dotnet build Test.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Test.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Test.dll"]
