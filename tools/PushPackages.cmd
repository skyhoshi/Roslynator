@echo off

set /p _apiKey=Enter API Key: 
set _source=https://api.nuget.org/v3/index.json
set _rootPath=..\src

set _version=1.9.0-rc

set _filePath=%_rootPath%\Analyzers.CodeFixes\bin\Release\Roslynator.Analyzers.%_version%.nupkg
dotnet nuget push "%_filePath%" -k %_apiKey% -s %_source%

set _filePath=%_rootPath%\CodeFixes\bin\Release\Roslynator.CodeFixes.%_version%.nupkg
dotnet nuget push "%_filePath%" -k %_apiKey% -s %_source%

set _version=1.0.0-rc3

set _filePath=%_rootPath%\CSharp\bin\Release\Roslynator.CSharp.%_version%.nupkg
dotnet nuget push "%_filePath%" -k %_apiKey% -s %_source%

set _filePath=%_rootPath%\CSharp.Workspaces\bin\Release\Roslynator.CSharp.Workspaces.%_version%.nupkg
dotnet nuget push "%_filePath%" -k %_apiKey% -s %_source%

echo OK
pause
