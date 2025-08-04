@echo off
echo 🧪 Testing API Endpoints...
echo ===========================

echo.
echo Testing Products endpoint...
powershell -Command "try { $response = Invoke-WebRequest -Uri 'http://localhost:5280/api/Product' -UseBasicParsing -TimeoutSec 10; if ($response.StatusCode -eq 200) { Write-Host '✅ Products endpoint working!' -ForegroundColor Green; $products = $response.Content | ConvertFrom-Json; Write-Host \"Found $($products.Count) products\" -ForegroundColor Cyan } } catch { Write-Host '❌ Products endpoint failed: ' $_.Exception.Message -ForegroundColor Red }"

echo.
echo Testing Categories endpoint...
powershell -Command "try { $response = Invoke-WebRequest -Uri 'http://localhost:5280/api/Category' -UseBasicParsing -TimeoutSec 10; if ($response.StatusCode -eq 200) { Write-Host '✅ Categories endpoint working!' -ForegroundColor Green; $categories = $response.Content | ConvertFrom-Json; Write-Host \"Found $($categories.Count) categories\" -ForegroundColor Cyan } } catch { Write-Host '❌ Categories endpoint failed: ' $_.Exception.Message -ForegroundColor Red }"

echo.
echo Testing GRNs endpoint...
powershell -Command "try { $response = Invoke-WebRequest -Uri 'http://localhost:5280/api/GRN' -UseBasicParsing -TimeoutSec 10; if ($response.StatusCode -eq 200) { Write-Host '✅ GRNs endpoint working!' -ForegroundColor Green; $grns = $response.Content | ConvertFrom-Json; Write-Host \"Found $($grns.Count) GRNs\" -ForegroundColor Cyan } } catch { Write-Host '❌ GRNs endpoint failed: ' $_.Exception.Message -ForegroundColor Red }"

echo.
echo Testing GINs endpoint...
powershell -Command "try { $response = Invoke-WebRequest -Uri 'http://localhost:5280/api/GIN' -UseBasicParsing -TimeoutSec 10; if ($response.StatusCode -eq 200) { Write-Host '✅ GINs endpoint working!' -ForegroundColor Green; $gins = $response.Content | ConvertFrom-Json; Write-Host \"Found $($gins.Count) GINs\" -ForegroundColor Cyan } } catch { Write-Host '❌ GINs endpoint failed: ' $_.Exception.Message -ForegroundColor Red }"

echo.
echo Testing Audit Logs endpoint...
powershell -Command "try { $response = Invoke-WebRequest -Uri 'http://localhost:5280/api/AuditLog' -UseBasicParsing -TimeoutSec 10; if ($response.StatusCode -eq 200) { Write-Host '✅ Audit Logs endpoint working!' -ForegroundColor Green; $logs = $response.Content | ConvertFrom-Json; Write-Host \"Found $($logs.Count) audit logs\" -ForegroundColor Cyan } } catch { Write-Host '❌ Audit Logs endpoint failed: ' $_.Exception.Message -ForegroundColor Red }"

echo.
echo 🎉 API Testing Complete!
echo If all endpoints show ✅, the circular reference issue is resolved.
echo.
pause 