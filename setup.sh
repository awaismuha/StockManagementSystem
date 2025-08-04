#!/bin/bash

# Stock Management System - Auto Setup Script for Linux/macOS
# This script will automatically set up and run the Stock Management System

echo "🚀 Stock Management System - Auto Setup"
echo "====================================="

# Check if .NET 9.0 is installed
echo "Checking .NET 9.0 installation..."
if command -v dotnet &> /dev/null; then
    DOTNET_VERSION=$(dotnet --version)
    if [[ $DOTNET_VERSION == 9.* ]]; then
        echo "✅ .NET 9.0 found: $DOTNET_VERSION"
    else
        echo "❌ .NET 9.0 not found. Current version: $DOTNET_VERSION"
        echo "Please install .NET 9.0 SDK from: https://dotnet.microsoft.com/download/dotnet/9.0"
        exit 1
    fi
else
    echo "❌ .NET not found. Please install .NET 9.0 SDK"
    exit 1
fi

# Check if SQL Server is available (for Linux/macOS, we'll use a different approach)
echo "Checking database setup..."
echo "Note: For Linux/macOS, you may need to install SQL Server or use Docker"

# Restore packages for both projects
echo "Restoring NuGet packages..."
dotnet restore StockManagementSystem.Api
dotnet restore StockManagementSystem.Web

if [ $? -ne 0 ]; then
    echo "❌ Failed to restore packages"
    exit 1
fi

# Update database
echo "Setting up database..."
cd StockManagementSystem.Api
dotnet ef database update
cd ..

# Start both applications
echo "Starting applications..."
echo "API will run on: http://localhost:5280"
echo "Web will run on: http://localhost:5232"
echo "Swagger UI: http://localhost:5280/swagger"
echo ""

# Start API in background
echo "Starting API..."
cd StockManagementSystem.Api
dotnet run &
API_PID=$!
cd ..

# Wait a moment for API to start
sleep 3

# Start Web in background
echo "Starting Web Application..."
cd StockManagementSystem.Web
dotnet run &
WEB_PID=$!
cd ..

# Wait a moment for Web to start
sleep 3

echo ""
echo "🎉 Stock Management System is starting up!"
echo "=========================================="
echo "📱 Web Application: http://localhost:5232"
echo "🔧 API Documentation: http://localhost:5280/swagger"
echo "🔑 Default Admin Login: admin@stock.com / Admin@123"
echo ""

# Try to open web application in default browser
if command -v xdg-open &> /dev/null; then
    # Linux
    xdg-open http://localhost:5232 &
elif command -v open &> /dev/null; then
    # macOS
    open http://localhost:5232 &
fi

echo "✅ Setup complete! The application should now be running."
echo "To stop the applications, press Ctrl+C"
echo ""

# Wait for user to stop the applications
trap "echo 'Stopping applications...'; kill $API_PID $WEB_PID 2>/dev/null; exit" INT
wait 