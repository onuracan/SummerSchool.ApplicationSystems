FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Presentation/SummerSchool.ApplicationSystems.WebApi/SummerSchool.ApplicationSystems.WebApi.csproj", "Presentation/SummerSchool.ApplicationSystems.WebApi/"]
COPY ["Core/SummerSchool.ApplicationSystems.Core/SummerSchool.ApplicationSystems.Core.csproj", "Core/SummerSchool.ApplicationSystems.Core/"]
COPY ["Core/SummerSchool.ApplicationSystems.Shared/SummerSchool.ApplicationSystems.Shared.csproj", "Core/SummerSchool.ApplicationSystems.Shared/"]
COPY ["Repository/SummerSchool.ApplicationSystems.Repository/SummerSchool.ApplicationSystems.Repository.csproj", "Repository/SummerSchool.ApplicationSystems.Repository/"]
COPY ["Service/SummerSchool.ApplicationSystems.Service/SummerSchool.ApplicationSystems.Service.csproj", "Service/SummerSchool.ApplicationSystems.Service/"]

RUN dotnet restore "Presentation/SummerSchool.ApplicationSystems.WebApi/SummerSchool.ApplicationSystems.WebApi.csproj"

COPY . .

WORKDIR "/src/Presentation/SummerSchool.ApplicationSystems.WebApi"
RUN dotnet build "SummerSchool.ApplicationSystems.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SummerSchool.ApplicationSystems.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SummerSchool.ApplicationSystems.WebApi.dll"]