var target = Argument<string>("target", "build");

Task("Cake Colors Demo")
    .Does(() =>
{
    Console.Out.WriteLine("\u001b[44;1mHello from {0}! This is written to stdout (1).\u001b[0m", "Cake");
    Console.Out.Flush();

    Console.Error.WriteLine("\u001b[41mHello from {0}! This is written to stderr (1).\u001b[0m", "Cake");
    Console.Error.Flush();

    Console.Out.WriteLine("\u001b[44;1mHello from {0}! This is written to stdout (2).\u001b[0m", "Cake");
    Console.Out.Flush();

    Console.Error.WriteLine("\u001b[41mHello from {0}! This is written to stderr (2).\u001b[0m", "Cake");
    Console.Error.Flush();

    Console.Out.WriteLine("\u001b[44;1mHello from {0}! This is written to stdout (3).\u001b[0m", "Cake");
    Console.Out.Flush();

    Console.Error.WriteLine("\u001b[41mHello from {0}! This is written to stderr (3).\u001b[0m", "Cake");
    Console.Error.Flush();

    Console.Out.WriteLine("\u001b[44;1mHello from {0}! This is written to stdout (4).\u001b[0m", "Cake");
    Console.Out.Flush();

    Console.Error.WriteLine("\u001b[41mHello from {0}! This is written to stderr (4).\u001b[0m", "Cake");
    Console.Error.Flush();
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
