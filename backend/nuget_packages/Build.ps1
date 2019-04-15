$vs_instance = Get-VSSetupInstance
$buildPath = Get-VSSetupInstance | Where {$_.DisplayName -eq "Visual Studio Community 2017"} | select InstallationPath
$buildPath = $buildPath.InstallationPath + "\MSBuild\15.0\bin\MSBuild.exe"

#build projects
& $buildPath 'c:\Users\Domer\source\repos\Security.V2.1\backend\Security\Security.csproj' '/p:TargetFrameworkVersion=v4.6;Configuration=Release;OutDir=..\nuget_packages\lib' '/tv:15.0'
& $buildPath 'c:\Users\Domer\source\repos\Security.V2.1\backend\SecurityHttp\SecurityHttp.csproj' '/p:TargetFrameworkVersion=v4.6;Configuration=Release;OutDir=..\nuget_packages\lib' '/tv:15.0'
& $buildPath 'c:\Users\Domer\source\repos\Security.V2.1\backend\Security.Web\Security.Web.csproj' '/p:TargetFrameworkVersion=v4.6;Configuration=Release;OutDir=..\nuget_packages\lib' '/tv:15.0'

#nuget packaging
./nuget.exe pack Security.nuspec
./nuget.exe pack SecurityHttp.nuspec
./nuget.exe pack Security.Web.nuspec