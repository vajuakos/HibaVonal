@echo off
setlocal

echo Futo dotnet folyamatok leallitasa...
taskkill /IM dotnet.exe /F >nul 2>&1

timeout /t 2 /nobreak >nul

echo HibaVonal API inditasa...
start "HibaVonal.API" cmd /k "cd /d C:\Users\mailn\HibaVonal\HibaVonal.API && dotnet run --launch-profile https"

timeout /t 2 /nobreak >nul

echo HibaVonal Client inditasa...
start "HibaVonal.Client" cmd /k "cd /d C:\Users\mailn\HibaVonal\HibaVonal.Client && dotnet run --launch-profile https"

echo.
echo Elinditasi parancsok kiadva.
echo API: https://localhost:7056
echo Client: https://localhost:7174
pause
