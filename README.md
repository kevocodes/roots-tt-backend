# Roots Backend Technical Test

API REST en .NET 9 con Entity Framework Core y PostgreSQL.
Incluye pruebas unitarias con xUnit.
Dockerizada con perfiles para **desarrollo** (API + Postgres) y **tests** (solo ejecuta las pruebas).

---

## Requisitos

### Sin Docker

* [.NET SDK 9](https://dotnet.microsoft.com/)
* PostgreSQL 14+ corriendo en `localhost:5432` con una base `rootsdb` y credenciales:

    * usuario: `postgres`
    * password: `mysecretpassword`
      *(puedes cambiarlos, ver “Configuración”)*

### Con Docker

* Docker 24+
* Docker Compose v2

---

## Configuración

### Sin Docker (local)

La API obtiene la cadena de conexión desde, en este orden:

1. **Variables de entorno** (tienen prioridad):
   `ConnectionStrings__Postgres="Host=localhost;Port=5432;Database=rootsdb;Username=postgres;Password=mysecretpassword"`
2. `appsettings.Development.json` (si `ASPNETCORE_ENVIRONMENT=Development`)
3. `appsettings.json` (por defecto)

Con esta opción debes tener **PostgreSQL local** levantado con la base/usuario/clave que configures.

### Con Docker (compose)

Crea un archivo **`.env`** en la raíz del repositorio con las credenciales de Postgres que quieras usar en el entorno de desarrollo:

```
POSTGRES_USER=postgres
POSTGRES_PASSWORD=mysecretpassword
POSTGRES_DB=rootsdb
```

El `docker compose` usa estas variables para provisionar la base de datos y la API lee su cadena de conexión a través de las variables de entorno definidas en el compose.

---

## Ejecutar la API (sin Docker)

1. Restaurar y compilar

```bash
dotnet restore
dotnet build
```

2. Ejecutar en modo Development

```bash
export ASPNETCORE_ENVIRONMENT=Development
dotnet run --project RootsTechnicalTest.Api
```
La API queda publicada en `http://localhost:<puerto asignado>/scalar`.

---

## Ejecutar la API (con Docker)

Perfil de desarrollo (API + Postgres):

```bash
# Levantar
docker compose --profile dev up -d

# Logs de la API
docker compose logs -f api

# Eliminar contenedores y limpiar el volumen
docker compose --profile dev down -v
```

La API queda publicada en `http://localhost:8080/scalar`.

---

## Ejecutar tests y cobertura

### Sin Docker

```bash
# Ejecutar pruebas + cobertura
dotnet test RootsTechnicalTest.Tests \
  --collect:"XPlat Code Coverage" \
  --settings coverlet.runsettings \
  --results-directory TestResults \
  -v:m
```

Los artefactos de cobertura quedan en `TestResults/**/coverage.cobertura.xml`.

### Con Docker (perfil de pruebas)

```bash
# Ejecuta un contenedor efímero que corre los tests
docker compose --profile test run --rm tests
```

Si `TestResults/` quedó creada por el contenedor y tienes problemas de permisos en tu host, puedes **borrar la carpeta desde un contenedor**:

```bash
docker compose --profile test run --rm tests bash -lc 'rm -rf /src/TestResults'
```

---
