# Water Jug Challenge API

This API solves the **Water Jug Challenge** by finding the shortest sequence of steps to measure a specific volume of water using two jugs with fixed capacities. It also stores every solution attempt in a history log via a SQLite database through Entity Framework.


---

## Example

**Input:**
```json
{
  "jug1Capacity": 2,
  "jug2Capacity": 10,
  "target": 4
}
```

**Solution Output:**
```json
{
  "success": true,
  "message": "Success",
  "solution": [
    { "step": 1, "bucketX": 0, "bucketY": 2, "action": "Fill bucket Y" },
    { "step": 2, "bucketX": 2, "bucketY": 0, "action": "Transfer from bucket Y to X" },
    { "step": 3, "bucketX": 2, "bucketY": 2, "action": "Fill bucket Y" },
    { "step": 4, "bucketX": 4, "bucketY": 0, "action": "Transfer from bucket Y to X", "isFinalStep": true }
  ]
}
```

---

## API Endpoints

The API endpoints are documented and can be tested using Swagger.

Once the application is running, navigate to:

```
http://localhost:<port>/swagger
```

Replace `<port>` with the port number shown in your console (usually 5000 or 5001) to access the interactive Swagger UI where all endpoints are listed and testable.


### `POST /api/waterjugChallenge`

**Description:** Solves the water jug puzzle.

**Body Parameters:**
```json
{
  "jug1Capacity": int,
  "jug2Capacity": int,
  "target": int
}
```

**Response:**
- `200 OK` with solution if solvable
- `200 OK` with `success = false` if unsolvable

---

### `GET /api/history`

**Description:** Retrieves all previously attempted jug puzzles with their outcomes and timestamps.

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "jug1Capacity": 2,
      "jug2Capacity": 10,
      "target": 4,
      "solved": true,
      "solvedAt": "2025-06-27T02:00:00Z",
      "stepsJson": "[{...}]"
    },
    ...
  ]
}
```

---

## Getting Started

Clone the repository and navigate to the project folder. If you use Visual Studio, open the solution file.
Run the following commands in your terminal:

```bash
    dotnet restore     # Restores NuGet packages
    dotnet build       # Compiles the project
    dotnet run         # Starts the API
```
Note: The API uses a local SQLite database file. No additional database setup or migrations are required.


###

## Testing

Tests are organized under the `WaterJug.Tests` project:

- **Unit Tests** for `WaterJugService` logic (success and failure cases)
- **Integration Tests** for API endpoints using a custom WebApplicationFactory
- Run tests via:

```bash
dotnet test
```

---

## Project Structure

```text
WaterJug/
├── Controllers/         # API controllers
├── Data/                # AppDbContext
├── Models/              # Entities and DTOs
├── Services/            # Core solving logic
├── WaterJug.Tests/      # xUnit test project
└── Program.cs           # ASP.NET Core entry point
```

---

## Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Works on Windows/macOS/Linux

---

## Author

Developed by **Douglas Worm** 
