set msbuild=%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set command=%msbuild% CustomerManagement.msbuild

echo %command%
call %command%