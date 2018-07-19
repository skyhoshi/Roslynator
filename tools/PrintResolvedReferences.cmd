@echo off

"C:\Program Files\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild" "..\src\Tools\Documentation.CommandLine\Documentation.CommandLine.csproj" ^
 /t:PrintResolvedReferences ^
 /p:Configuration=Debug ^
 /m

pause