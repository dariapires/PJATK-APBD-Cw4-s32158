# Tut7 - REST API do zarządzania komputerami

Projekt ASP.NET Core Web API z Entity Framework Core Code First.
Aplikacja obsługuje komputery PC oraz ich komponenty zgodnie z diagramem z ćwiczenia.

## Technologie

- ASP.NET Core Web API
- Entity Framework Core Code First
- SQL Server LocalDB
- Swagger

## Endpointy

- `GET /api/pcs` - lista komputerów
- `GET /api/pcs/{id}/components` - komputer razem z komponentami
- `POST /api/pcs` - dodanie komputera
- `PUT /api/pcs/{id}` - edycja komputera
- `DELETE /api/pcs/{id}` - usunięcie komputera

## Uruchomienie

Jeżeli nie masz narzędzia do migracji EF Core, zainstaluj je:

```bash
dotnet tool install --global dotnet-ef
```

Baza danych jest ustawiona w `appsettings.json` na LocalDB:

```text
Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tut7Db;Integrated Security=True;TrustServerCertificate=True
```

Migracja jest już dodana do projektu, więc wystarczy wykonać:

```bash
dotnet ef database update
```

Potem uruchom projekt:

```bash
dotnet run
```

Swagger powinien być dostępny pod adresem z konsoli, np.:

```text
http://localhost:5077/swagger
```

## Struktura projektu

- `Models` - encje bazy danych
- `DTOs` - klasy używane w żądaniach i odpowiedziach API
- `Data` - `AppDbContext` i konfiguracja EF Core
- `Services` - logika aplikacji
- `Controllers` - endpointy API
- `Migrations` - migracja tworząca bazę danych

## Uwagi

Endpointy nie zwracają bezpośrednio encji z bazy danych. Dane są mapowane na DTO.
Operacje na bazie danych są wykonywane asynchronicznie przez `async/await`.
