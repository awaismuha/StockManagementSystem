@echo off
echo 🔄 Resetting Database with Test Data...
echo ======================================

echo Stopping any running processes...
taskkill /f /im dotnet.exe 2>nul
taskkill /f /im StockManagementSystem*.exe 2>nul

echo.
echo Dropping and recreating database...
cd StockManagementSystem.Api
dotnet ef database drop --force
dotnet ef database update
cd ..

echo.
echo ✅ Database reset complete!
echo The database now contains:
echo - 10 Categories (Electronics, Office Supplies, Furniture, etc.)
echo - 10 Products (Laptops, Mice, Chairs, etc.)
echo - 10 GRNs (Goods Received Notes)
echo - 12 GRN Items (with realistic quantities and prices)
echo - 10 GINs (Goods Issued Notes)
echo - 12 GIN Items (with realistic quantities)
echo - 10 Audit Logs (tracking all activities)
echo.
echo Default Admin Login: admin@stock.com / Admin@123
echo.
echo Run run-simple.bat to start the applications
pause 