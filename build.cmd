SET MsBuildPath=C:\Windows\Microsoft.NET\Framework64\v4.0.30319
SET NuGetExe=.nuget\nuget.exe
%MsBuildPath%\MsBuild.exe CrowFx.msbuild /t:Crowfx /p:NuGetExe=%NuGetExe%
%MsBuildPath%\MsBuild.exe Crow.Library\Crow.Library.csproj /t:rebuild
%MsBuildPath%\MsBuild.exe CrowSample\CrowSample.csproj /t:rebuild