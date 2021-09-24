# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src/Api.Main
# COPY ["src/Api.Main/Api.Main.csproj", "/src/Api.Main/"]
# RUN dotnet restore "Api.Main.csproj"
# COPY . .

# RUN dotnet publish "src/Api.Main/Api.Main.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT [ "dotnet", "Api.Main.dll" ]


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src/Api.Main
COPY ["src/Api.Main/Api.Main.csproj", "/src/Api.Main"]
RUN dotnet restore "Api.Main.csproj"
COPY . .

RUN dotnet build "src/Api.Main/Api.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish app/publish .
ENTRYPOINT ["dotnet", "Api.Main.dll"]