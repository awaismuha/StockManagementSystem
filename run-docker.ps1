# Stock Management System - Docker Setup Script
# This script will run the entire system using Docker

Write-Host "🐳 Stock Management System - Docker Setup" -ForegroundColor Green
Write-Host "=========================================" -ForegroundColor Green

# Check if Docker is installed
Write-Host "Checking Docker installation..." -ForegroundColor Yellow
try {
    $dockerVersion = docker --version
    Write-Host "✅ Docker found: $dockerVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ Docker not found. Please install Docker Desktop" -ForegroundColor Red
    Write-Host "Download from: https://www.docker.com/products/docker-desktop" -ForegroundColor Red
    exit 1
}

# Check if Docker is running
Write-Host "Checking if Docker is running..." -ForegroundColor Yellow
try {
    docker info > $null 2>&1
    Write-Host "✅ Docker is running" -ForegroundColor Green
} catch {
    Write-Host "❌ Docker is not running. Please start Docker Desktop" -ForegroundColor Red
    exit 1
}

# Build and run with Docker Compose
Write-Host "Building and starting containers..." -ForegroundColor Yellow
Write-Host "This may take a few minutes on first run..." -ForegroundColor Yellow

try {
    docker-compose up --build -d
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Containers started successfully!" -ForegroundColor Green
    } else {
        Write-Host "❌ Failed to start containers" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "❌ Error starting containers: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Wait for services to be ready
Write-Host "Waiting for services to be ready..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

Write-Host ""
Write-Host "🎉 Stock Management System is running in Docker!" -ForegroundColor Green
Write-Host "===============================================" -ForegroundColor Green
Write-Host "📱 Web Application: http://localhost:5232" -ForegroundColor Cyan
Write-Host "🔧 API Documentation: http://localhost:5280/swagger" -ForegroundColor Cyan
Write-Host "🔑 Default Admin Login: admin@stock.com / Admin@123" -ForegroundColor Cyan
Write-Host ""

Write-Host "Press any key to open the web application..." -ForegroundColor Yellow
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

# Open web application in default browser
Start-Process "http://localhost:5232"

Write-Host ""
Write-Host "✅ Docker setup complete!" -ForegroundColor Green
Write-Host ""
Write-Host "Useful Docker commands:" -ForegroundColor Yellow
Write-Host "  docker-compose logs -f    # View logs" -ForegroundColor Gray
Write-Host "  docker-compose down       # Stop containers" -ForegroundColor Gray
Write-Host "  docker-compose restart    # Restart containers" -ForegroundColor Gray
Write-Host ""
Write-Host "To stop the application, run: docker-compose down" -ForegroundColor Yellow 