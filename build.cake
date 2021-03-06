var target = Argument<string>("target", "build");

Task("Cake Colors Demo")
    .Does(() =>
{
    Verbose("Hello from {0}! This is a Verbose message.", "Cake");
    Information("Hello from {0}! This is an Information message.", "Cake");
    Warning("Hello from {0}! This is a Warning message.", "Cake");
    Error("Hello from {0}! This is an Error message.", "Cake");
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
