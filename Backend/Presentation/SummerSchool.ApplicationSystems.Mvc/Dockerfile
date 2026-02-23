FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Presentation/SummerSchool.ApplicationSystems.Mvc/SummerSchool.ApplicationSystems.Mvc.csproj", "Presentation/SummerSchool.ApplicationSystems.Mvc/"]
COPY ["Core/SummerSchool.ApplicationSystems.Shared/SummerSchool.ApplicationSystems.Shared.csproj", "Core/SummerSchool.ApplicationSystems.Shared/"]

RUN dotnet restore "Presentation/SummerSchool.ApplicationSystems.Mvc/SummerSchool.ApplicationSystems.Mvc.csproj"

COPY . .

WORKDIR "/src/Presentation/SummerSchool.ApplicationSystems.Mvc"
RUN dotnet build "SummerSchool.ApplicationSystems.Mvc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SummerSchool.ApplicationSystems.Mvc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SummerSchool.ApplicationSystems.Mvc.dll"]