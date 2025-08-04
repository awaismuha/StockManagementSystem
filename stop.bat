@echo off
echo 🛑 Stopping Stock Management System...
echo =====================================

echo Stopping dotnet processes...
taskkill /F /IM "dotnet.exe" >nul 2>&1
if %errorlevel% equ 0 (
    echo ✅ Stopped dotnet processes
) else (
    echo No dotnet processes found
)

echo Stopping StockManagementSystem processes...
taskkill /F /IM "StockManagementSystem*.exe" >nul 2>&1
if %errorlevel% equ 0 (
    echo ✅ Stopped StockManagementSystem processes
) else (
    echo No StockManagementSystem processes found
)

echo Cleaning up build artifacts...
if exist "StockManagementSystem.Api\bin" rmdir /s /q "StockManagementSystem.Api\bin" >nul 2>&1
if exist "StockManagementSystem.Api\obj" rmdir /s /q "StockManagementSystem.Api\obj" >nul 2>&1
if exist "StockManagementSystem.Web\bin" rmdir /s /q "StockManagementSystem.Web\bin" >nul 2>&1
if exist "StockManagementSystem.Web\obj" rmdir /s /q "StockManagementSystem.Web\obj" >nul 2>&1
echo ✅ Cleaned up build artifacts

echo.
echo ✅ All Stock Management System processes stopped!
echo You can now run run.bat to start the applications again.
pause 