@echo on
set Filters= +[ProjectName*]* ^
-[*ProjectName.WebApp*]* -[*ProjectName.Web*]* ^
-[*ProjectName.Consumer*]* -[*ProjectName.Consumer.WebApp*]* ^
-[*ProjectName.Domain*]* -[*ProjectName.Migrations*]* ^
-[*ProjectName.Integration.Tests*]*

set DotNet=C:\Program Files
set OpenCover=opencover\4.7.1221
set ReportGenerator=reportgenerator\5.1.10

if exist "%~dp0reports" rmdir "%~dp0reports" /s /q
if exist "%~dp0coverage" rmdir "%~dp0coverage" /s /q
mkdir "%~dp0reports"
mkdir "%~dp0coverage"

call :RunOpenCover

if %errorlevel% equ 0 (
 call :RunReportGenerator
)
exit /b %errorlevel%

:RunOpenCover
for /r %%i in (.\ProjectName.Integration.Tests\*.csproj) do (
"%USERPROFILE%\.nuget\packages\%OpenCover%\tools\OpenCover.Console.exe" ^
-oldstyle ^
-register:user ^
-target:"%DotNet%\dotnet\dotnet.exe" ^
-targetargs:"test -p:Platform=x64 tests\ProjectName.Integration.Tests\%%~nxi" ^
-filter:"%Filters%" ^
-mergebyhash ^
-skipautoprops ^
-excludebyattribute:"*.GeneratedCodeAttribute;*.ExcludeFromCodeCoverageAttribute" ^
-output:"%~dp0reports\report.xml"
)

TIMEOUT /T 300 
exit /b %errorlevel%

TIMEOUT /T 300 
exit /b %errorlevel%

:RunReportGenerator
dotnet "%USERPROFILE%\.nuget\packages\%ReportGenerator%\tools\net6.0\ReportGenerator.dll" ^
-reports:"%~dp0reports\*.xml" ^
-targetdir:"%~dp0coverage\"

TIMEOUT /T 300 
exit /b %errorlevel%
exit /b %errorlevel%