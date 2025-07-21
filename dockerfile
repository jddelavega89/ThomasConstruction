# Usa la imagen de .NET SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos del proyecto
COPY . .

# Restaura dependencias y publica
RUN dotnet publish -c Release -o /app --no-restore

# Imagen final para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Puerto en el que corre la app (puedes omitir si usas Kestrel por defecto)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ThomasConstruction.dll"]
