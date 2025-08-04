@echo off
echo 🚀 Stock Management System - Auto Setup
echo =====================================

REM Stop any running processes first
echo Stopping any running processes...
taskkill /F /IM "dotnet.exe" >nul 2>&1
taskkill /F /IM "StockManagementSystem*.exe" >nul 2>&1
timeout /t 2 /nobreak >nul

REM Clean up build artifacts
echo Cleaning up build artifacts...
if exist "StockManagementSystem.Api\bin" rmdir /s /q "StockManagementSystem.Api\bin" >nul 2>&1
if exist "StockManagementSystem.Api\obj" rmdir /s /q "StockManagementSystem.Api\obj" >nul 2>&1
if exist "StockManagementSystem.Web\bin" rmdir /s /q "StockManagementSystem.Web\bin" >nul 2>&1
if exist "StockManagementSystem.Web\obj" rmdir /s /q "StockManagementSystem.Web\obj" >nul 2>&1

REM Check if .NET 9.0 is installed
echo Checking .NET 9.0 installation...
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ .NET not found. Please install .NET 9.0 SDK
    pause
    exit /b 1
)

for /f "tokens=*" %%i in ('dotnet --version') do set DOTNET_VERSION=%%i
echo ✅ .NET found: %DOTNET_VERSION%

REM Restore packages for both projects
echo Restoring NuGet packages...
dotnet restore StockManagementSystem.Api
dotnet restore StockManagementSystem.Web

if %errorlevel% neq 0 (
    echo ❌ Failed to restore packages
    pause
    exit /b 1
)

REM Update database
echo Setting up database...
cd StockManagementSystem.Api
dotnet ef database update
cd ..

REM Start both applications
echo Starting applications...
echo API will run on: http://localhost:5280
echo Web will run on: http://localhost:5232
echo Swagger UI: http://localhost:5280/swagger
echo.

REM Start API in background
echo Starting API...
start "Stock Management API" cmd /k "cd /d %CD%\StockManagementSystem.Api && dotnet run"

REM Wait a moment for API to start
timeout /t 5 /nobreak >nul

REM Start Web in background
echo Starting Web Application...
start "Stock Management Web" cmd /k "cd /d %CD%\StockManagementSystem.Web && dotnet run"

REM Wait a moment for Web to start
timeout /t 5 /nobreak >nul

echo.
echo 🎉 Stock Management System is starting up!
echo ==========================================
echo 📱 Web Application: http://localhost:5232
echo 🔧 API Documentation: http://localhost:5280/swagger
echo 🔑 Default Admin Login: admin@stock.com / Admin@123
echo.

REM Open web application in default browser
start http://localhost:5232

echo ✅ Setup complete! The application should now be running.
echo To stop the applications, close the command prompt windows that opened.
echo Or run: stop.bat
pause 