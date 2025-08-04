# Stock Management System

A comprehensive stock management system built with ASP.NET Core 9.0, featuring a REST API backend and a web-based frontend.

## 🚀 Quick Start

### Option 1: One-Click Setup (Windows)
1. **Double-click** `run.bat` (Command Prompt) or `run.ps1` (PowerShell)
2. The script will automatically:
   - Check prerequisites
   - Stop any running processes
   - Clean up build artifacts
   - Restore packages
   - Set up the database
   - Start both applications
   - Open the web interface

### Option 2: Docker Setup
1. **Double-click** `run-docker.ps1` (requires Docker Desktop)
2. The script will:
   - Check Docker installation
   - Build and run containers
   - Start the complete system

### Option 3: Manual Setup

#### Prerequisites
- **.NET 9.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **SQL Server** (any of the following):
  - SQL Server LocalDB (recommended for development)
  - SQL Server Express
  - SQL Server Developer/Standard/Enterprise

#### Installation Steps

1. **Clone or download** the project
2. **Open terminal/command prompt** in the project root
3. **Restore packages:**
   ```bash
   dotnet restore StockManagementSystem.Api
   dotnet restore StockManagementSystem.Web
   ```
4. **Set up database:**
   ```bash
   cd StockManagementSystem.Api
   dotnet ef database update
   cd ..
   ```
5. **Run the applications:**

   **Terminal 1 (API):**
   ```bash
   cd StockManagementSystem.Api
   dotnet run
   ```

   **Terminal 2 (Web):**
   ```bash
   cd StockManagementSystem.Web
   dotnet run
   ```

## 🛑 Stopping the Application

### Windows
- **PowerShell**: Run `.\stop.ps1`
- **Command Prompt**: Run `stop.bat`
- **Manual**: Close the terminal windows running the applications

### Linux/macOS
- Press `Ctrl+C` in the terminal running the applications
- Or use the process management in `setup.sh`

## 🌐 Access Points

- **Web Application**: http://localhost:5232
- **API Documentation**: http://localhost:5280/swagger
- **API Endpoints**: http://localhost:5280/api

## 🔑 Default Login

- **Email**: admin@stock.com
- **Password**: Admin@123

## 📋 Features

### Core Modules
- **Authentication & Authorization** - JWT-based security
- **Product Management** - Add, edit, delete products
- **Category Management** - Organize products by categories
- **GRN (Goods Received Note)** - Track incoming inventory
- **GIN (Goods Issue Note)** - Track outgoing inventory
- **Stock Reports** - Real-time inventory tracking

### Report Types
- **Real-time Stock** - Current inventory levels
- **Stock Summary** - Overview of all products
- **Stock Valuation** - Financial value of inventory
- **Stock Movements** - History of stock changes
- **Low Stock Alerts** - Products running low

### Audit Features
- **Complete audit trail** of all operations
- **User activity logging**
- **Data change tracking**

## 🛠 Configuration

### Database Connection
The system automatically detects and uses the best available database:

1. **LocalDB** (default for development)
2. **SQL Server Express**
3. **Custom SQL Server instance**

To use a custom database, update the connection string in:
```
StockManagementSystem.Api/appsettings.json
```

### JWT Configuration
JWT settings are configured in `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "YourSecretKeyHere",
    "Issuer": "StockManagementSystem"
  }
}
```

## 🏗 Project Structure

```
StockManagementSystem/
├── StockManagementSystem.Api/          # REST API Backend
│   ├── Controllers/                    # API endpoints
│   ├── Models/                         # Data models & DbContext
│   ├── Migrations/                     # Database migrations
│   └── Program.cs                      # API startup
├── StockManagementSystem.Web/          # Web Frontend
│   ├── Controllers/                    # MVC controllers
│   ├── Views/                          # Razor views
│   ├── Models/                         # View models
│   └── wwwroot/                        # Static files
├── run.ps1                             # PowerShell setup script
├── run.bat                             # Batch setup script
├── stop.ps1                            # PowerShell stop script
├── stop.bat                            # Batch stop script
├── run-docker.ps1                      # Docker setup script
├── setup.sh                            # Linux/macOS setup script
├── docker-compose.yml                  # Docker configuration
└── README.md                           # This file
```

## 🔧 Development

### Adding New Features
1. **API Changes**: Modify controllers in `StockManagementSystem.Api/Controllers/`
2. **Database Changes**: Create new migrations with `dotnet ef migrations add MigrationName`
3. **UI Changes**: Modify views in `StockManagementSystem.Web/Views/`

### Database Migrations
```bash
cd StockManagementSystem.Api
dotnet ef migrations add MigrationName
dotnet ef database update
```

## 🐛 Troubleshooting

### Common Issues

**1. File Locked Error**
- Run `.\stop.ps1` or `stop.bat` to stop all processes
- Then run `.\run.ps1` or `run.bat` again

**2. Database Connection Error**
- Ensure SQL Server is running
- Check connection string in `appsettings.json`
- Try using LocalDB: `Server=(localdb)\MSSQLLocalDB`

**3. Port Already in Use**
- API uses port 5280
- Web uses port 5232
- Change ports in `launchSettings.json` if needed

**4. .NET Not Found**
- Install .NET 9.0 SDK
- Verify installation: `dotnet --version`

**5. Package Restore Fails**
- Check internet connection
- Clear NuGet cache: `dotnet nuget locals all --clear`
- Try: `dotnet restore --force`

**6. Process Already Running**
- The setup scripts automatically handle this
- Manual fix: Use Task Manager to end dotnet.exe processes

### Getting Help
1. Check the console output for error messages
2. Verify all prerequisites are installed
3. Ensure no firewall/antivirus is blocking the applications
4. Use the stop scripts to clean up before restarting

## 📝 License

This project is open source and available under the MIT License.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

---

**Happy Stock Managing! 📦📊**