@echo off
SETLOCAL
set source=%cd%
set packages=%source%\packages
set outdir=%source%\bin

rem Cycle Directories
cd %source%\TimePeriod
set framework=netstandard2.0
call :routine
cd %source%\TimePeriodDemo
set framework=netcoreapp2.0
call :routine
cd %source%
goto done

:routine
dotnet restore --packages %packages%
dotnet build -o %outdir% -f %framework%
dotnet publish -o %outdir% -f %framework% -c Release -r win-x64
cd ..
goto:eof

:done
ENDLOCAL
echo Done Building
