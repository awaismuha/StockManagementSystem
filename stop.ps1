# Stock Management System - Stop Script
# This script will stop all running Stock Management System processes

Write-Host "🛑 Stopping Stock Management System..." -ForegroundColor Red
Write-Host "=====================================" -ForegroundColor Red

# Stop any running dotnet processes
Write-Host "Stopping dotnet processes..." -ForegroundColor Yellow
$dotnetProcesses = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue
if ($dotnetProcesses) {
    Write-Host "Found $($dotnetProcesses.Count) dotnet process(es)" -ForegroundColor Yellow
    Stop-Process -Name "dotnet" -Force -ErrorAction SilentlyContinue
    Write-Host "✅ Stopped dotnet processes" -ForegroundColor Green
} else {
    Write-Host "No dotnet processes found" -ForegroundColor Gray
}

# Stop any running StockManagementSystem processes
Write-Host "Stopping StockManagementSystem processes..." -ForegroundColor Yellow
$stockProcesses = Get-Process -Name "StockManagementSystem*" -ErrorAction SilentlyContinue
if ($stockProcesses) {
    Write-Host "Found $($stockProcesses.Count) StockManagementSystem process(es)" -ForegroundColor Yellow
    Stop-Process -Name "StockManagementSystem*" -Force -ErrorAction SilentlyContinue
    Write-Host "✅ Stopped StockManagementSystem processes" -ForegroundColor Green
} else {
    Write-Host "No StockManagementSystem processes found" -ForegroundColor Gray
}

# Clean up build artifacts
Write-Host "Cleaning up build artifacts..." -ForegroundColor Yellow
Remove-Item -Path "StockManagementSystem.Api\bin" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "StockManagementSystem.Api\obj" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "StockManagementSystem.Web\bin" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "StockManagementSystem.Web\obj" -Recurse -Force -ErrorAction SilentlyContinue
Write-Host "✅ Cleaned up build artifacts" -ForegroundColor Green

Write-Host ""
Write-Host "✅ All Stock Management System processes stopped!" -ForegroundColor Green
Write-Host "You can now run .\run.ps1 to start the applications again." -ForegroundColor Yellow 