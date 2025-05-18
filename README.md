# Translation Manager Documentation

## Introduction
The Translation Manager provides a complete solution for managing application translations with a Blazor-based UI and REST API.



![image](https://github.com/user-attachments/assets/b364e562-d9ca-4316-a280-efda34bd0329)
## Features

- **Web-based Management UI**
  - Add, edit, and delete translations
  - Search and filter functionality
  - Multi-format export/import

- **Supported Formats**
  - JSON
  - YAML
  - Excel/CSV

- **API Access**
  - RESTful endpoints
  - Caching support
  - Culture-specific retrieval

## Installation

### Prerequisites

- .NET 9.0+
- SQL Server
- Redis 

### Setup Steps

1. Clone repository:
   ```bash
   git clone https://github.com/yourusername/translation-manager.git
   ```

2. Configure database in `appsettings.json`:
   ```json
   "ConnectionStrings": {
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

| Parameter | Type   | Description                   |
|-----------|--------|-------------------------------|
| `key`     | string | Translation key               |
| `culture` | string | Culture code (e.g., `en-US`)  |

### Response Codes

- `200 OK`: Returns translated string  
- `404 Not Found`: Missing translation

### Example Request
```bash
curl -X GET "https://yourapi.com/api/translations/welcome_message/en-US"
```

## UI Usage

### Managing Translations

- **Adding**
  - Click "Add New Translation"
  - Enter key and culture values

- **Searching**
  - Use search box
  - Supports key/value filtering

### Import/Export Formats

| Format | Extension     | Notes              |
|--------|---------------|--------------------|
| JSON   | `.json`       | Hierarchical structure |
| YAML   | `.yaml`/`.yml`| Human-readable     |
| Excel  | `.xlsx`/`.csv`| Spreadsheet format  |


