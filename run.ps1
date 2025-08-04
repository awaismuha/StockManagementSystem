# Stock Management System - Auto Setup and Run Script
# This script will automatically set up and run the Stock Management System

Write-Host "🚀 Stock Management System - Auto Setup" -ForegroundColor Green
Write-Host "=====================================" -ForegroundColor Green

# Function to stop running processes
function Stop-RunningProcesses {
    Write-Host "Checking for running processes..." -ForegroundColor Yellow
    
    # Stop any running dotnet processes
    $dotnetProcesses = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue
    if ($dotnetProcesses) {
        Write-Host "Stopping running dotnet processes..." -ForegroundColor Yellow
        Stop-Process -Name "dotnet" -Force -ErrorAction SilentlyContinue
        Start-Sleep -Seconds 2
    }
    
    # Stop any running StockManagementSystem processes
    $stockProcesses = Get-Process -Name "StockManagementSystem*" -ErrorAction SilentlyContinue
    if ($stockProcesses) {
        Write-Host "Stopping running StockManagementSystem processes..." -ForegroundColor Yellow
        Stop-Process -Name "StockManagementSystem*" -Force -ErrorAction SilentlyContinue
        Start-Sleep -Seconds 2
    }
    
    # Clean up any locked files
    Write-Host "Cleaning up build artifacts..." -ForegroundColor Yellow
    Remove-Item -Path "StockManagementSystem.Api\bin" -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -Path "StockManagementSystem.Api\obj" -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -Path "StockManagementSystem.Web\bin" -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -Path "StockManagementSystem.Web\obj" -Recurse -Force -ErrorAction SilentlyContinue
}

# Function to wait for API to be ready
function Wait-ForApi {
    Write-Host "Waiting for API to be ready..." -ForegroundColor Yellow
    $maxAttempts = 30
    $attempt = 0
    
    while ($attempt -lt $maxAttempts) {
        try {
            $response = Invoke-WebRequest -Uri "http://localhost:5280/swagger" -UseBasicParsing -TimeoutSec 5 -ErrorAction Stop
            if ($response.StatusCode -eq 200) {
                Write-Host "✅ API is ready!" -ForegroundColor Green
                return $true
            }
        } catch {
            $attempt++
            Write-Host "Waiting for API... (attempt $attempt/$maxAttempts)" -ForegroundColor Gray
            Start-Sleep -Seconds 2
        }
    }
    
    Write-Host "⚠️  API may not be fully ready, but continuing..." -ForegroundColor Yellow
    return $false
}

# Check if .NET 9.0 is installed
Write-Host "Checking .NET 9.0 installation..." -ForegroundColor Yellow
try {
    $dotnetVersion = dotnet --version
    if ($dotnetVersion -like "9.*") {
        Write-Host "✅ .NET 9.0 found: $dotnetVersion" -ForegroundColor Green
    } else {
        Write-Host "❌ .NET 9.0 not found. Current version: $dotnetVersion" -ForegroundColor Red
        Write-Host "Please install .NET 9.0 SDK from: https://dotnet.microsoft.com/download/dotnet/9.0" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "❌ .NET not found. Please install .NET 9.0 SDK" -ForegroundColor Red
    exit 1
}

# Check if SQL Server LocalDB is available
Write-Host "Checking SQL Server LocalDB..." -ForegroundColor Yellow
try {
    $sqlcmd = sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "SELECT 1" -b
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ SQL Server LocalDB is available" -ForegroundColor Green
    } else {
        Write-Host "⚠️  SQL Server LocalDB not available, will try to use default SQL Server" -ForegroundColor Yellow
    }
} catch {
    Write-Host "⚠️  SQL Server LocalDB not available, will try to use default SQL Server" -ForegroundColor Yellow
}

# Stop any running processes and clean up
Stop-RunningProcesses

# Restore packages for both projects
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore StockManagementSystem.Api
dotnet restore StockManagementSystem.Web

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Failed to restore packages" -ForegroundColor Red
    exit 1
}

# Update database
Write-Host "Setting up database..." -ForegroundColor Yellow
Set-Location StockManagementSystem.Api
try {
    dotnet ef database update
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Database setup completed" -ForegroundColor Green
    } else {
        Write-Host "⚠️  Database setup failed, but continuing..." -ForegroundColor Yellow
    }
} catch {
    Write-Host "⚠️  Database setup failed, but continuing..." -ForegroundColor Yellow
}

# Return to root directory
Set-Location ..

# Start both applications
Write-Host "Starting applications..." -ForegroundColor Yellow
Write-Host "API will run on: http://localhost:5280" -ForegroundColor Cyan
Write-Host "Web will run on: http://localhost:5232" -ForegroundColor Cyan
Write-Host "Swagger UI: http://localhost:5280/swagger" -ForegroundColor Cyan
Write-Host ""

# Start API in background
Write-Host "Starting API..." -ForegroundColor Green
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PWD\StockManagementSystem.Api'; dotnet run"

# Wait for API to be ready
Wait-ForApi

# Start Web in background
Write-Host "Starting Web Application..." -ForegroundColor Green
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PWD\StockManagementSystem.Web'; dotnet run"

# Wait a moment for Web to start
Start-Sleep -Seconds 5

Write-Host ""
Write-Host "🎉 Stock Management System is starting up!" -ForegroundColor Green
Write-Host "==========================================" -ForegroundColor Green
Write-Host "📱 Web Application: http://localhost:5232" -ForegroundColor Cyan
Write-Host "🔧 API Documentation: http://localhost:5280/swagger" -ForegroundColor Cyan
Write-Host "🔑 Default Admin Login: admin@stock.com / Admin@123" -ForegroundColor Cyan
Write-Host ""
Write-Host "Press any key to open the web application in your browser..." -ForegroundColor Yellow
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

# Open web application in default browser
Start-Process "http://localhost:5232"

Write-Host ""
Write-Host "✅ Setup complete! The application should now be running." -ForegroundColor Green
Write-Host "To stop the applications, close the PowerShell windows that opened." -ForegroundColor Yellow
Write-Host "Or run: .\stop.ps1" -ForegroundColor Yellow 