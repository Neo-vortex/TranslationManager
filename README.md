# Translation Manager Documentation

## Introduction

Managing translations using RESX files is outdated and inefficient—it often leads to merge conflicts and requires recompiling the application for even minor changes, making it unsuitable for agile workflows.
The Translation Manager provides a complete solution for managing application translations with a Blazor-based UI and REST API.
![image](https://github.com/user-attachments/assets/b364e562-d9ca-4316-a280-efda34bd0329)
## Features

* **Web-based Management UI**

  * Add, edit, and delete translations
  * Search and filter functionality
  * Multi-format export/import

* **Supported Formats**

  * JSON
  * YAML
  * Excel/CSV

* **API Access**

  * RESTful endpoints
  * Caching support (5 min TTL for each translation)
  * Culture-specific retrieval
  * Optional parameter injection into translation strings

## Installation

### Prerequisites

* .NET 9.0+
* SQL Server
* Redis

### Setup Steps

1. Clone repository:

   ```bash
   git clone https://github.com/yourusername/translation-manager.git
   ```

2. Configure database in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "Redis" : "localhost:6379",
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TranslationManager;Trusted_Connection=True;"
   }
   ```

3. Apply migrations:

   ```bash
   dotnet ef database update
   ```

4. Run application:

   ```bash
   dotnet run
   ```

## API Documentation

### Get Translation Endpoint

```http
GET /api/translations/{key}/{culture}
```

| Parameter    | Type   | Description                                                                             |
| ------------ | ------ | --------------------------------------------------------------------------------------- |
| `key`        | string | Translation key                                                                         |
| `culture`    | string | Culture code (e.g., `en-US`)                                                            |
| `parameters` | string | *(Optional)* Comma-separated values to inject into placeholders like `{0}`, `{1}`, etc. |

### Response Codes

* `200 OK`: Returns translated string
* `404 Not Found`: Missing translation or culture-specific value
* `400 Bad Request`: Format mismatch with parameters

### Example Request

```bash
curl -X GET "https://yourapi.com/api/translations/welcome_message/en-US?parameters=John,3"
```

If the stored translation is:

```
"Hello, {0}! You have {1} new messages."
```

The response will be:

```
"Hello, John! You have 3 new messages."
```

## Docker

### Run using Docker

```bash
docker run -d \
  -p 8080:8080 \
  -p 8081:8081 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ConnectionStrings__DefaultConnection="Host=postgres;Port=5432;Database=Translation;Username=postgres;Password=postgres" \
  -e ConnectionStrings__Redis="redis:6379" \
  --name translation-manager \
  docker.io/realneovortex/traslationmanagersharp
```

## UI Usage

### Managing Translations

* **Adding**

  * Click "Add New Translation"
  * Enter key and culture values

* **Searching**

  * Use search box
  * Supports key/value filtering

### Import/Export Formats

| Format | Extension      | Notes                  |
| ------ | -------------- | ---------------------- |
| JSON   | `.json`        | Hierarchical structure |
| YAML   | `.yaml`/`.yml` | Human-readable         |
| Excel  | `.xlsx`/`.csv` | Spreadsheet format     |
