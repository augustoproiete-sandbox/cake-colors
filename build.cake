var target = Argument<string>("target", "build");

Task("Cake Colors Demo")
    .Does(() =>
{
    System.Threading.Thread.Sleep(2000);

    Verbose("Hello from {0}! This is a Verbose message.", "Cake");
    System.Threading.Thread.Sleep(2000);

    Information("Hello from {0}! This is an Information message.", "Cake");
    System.Threading.Thread.Sleep(2000);

    Warning("Hello from {0}! This is a Warning message.", "Cake");
    System.Threading.Thread.Sleep(2000);

    Error("Hello from {0}! This is an Error message.", "Cake");
    System.Threading.Thread.Sleep(2000);
});

Task("clean")
    .IsDependentOn("Cake Colors Demo")
    .Does(() =>
{
    CleanDirectories("./**/^{bin,obj}");
});

Task("restore")
    .IsDependentOn("clean")
    .Does(() =>
{
    DotNetCoreRestore("./ConsoleApp/ConsoleApp.sln", new DotNetCoreRestoreSettings
    {
        LockedMode = true,
    });
});

Task("build")
    .IsDependentOn("restore")
    .DoesForEach(new[] { "Debug", "Release" }, (configuration) =>
{
    DotNetCoreBuild("./ConsoleApp/ConsoleApp.sln", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        NoRestore = true,
        NoIncremental = false,
    });
});

RunTarget(target);
