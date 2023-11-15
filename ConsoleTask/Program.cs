using ConsoleTask;
using ConsoleTask.Services;

Serve.RunNative(services =>
{
    services.AddFileLogging("logs/{0:yyyy}-{0:MM}-{0:dd}.log", options =>
    {
        options.FileNameRule = fileName =>
        {
            return string.Format(fileName, DateTime.UtcNow);
        };
    });
    services.AddVirtualFileServer();
    services.AddRemoteRequest();
    services.AddSqlsugarSetup(App.Configuration);
}, includeWeb: false, args: args);

var launch = App.GetRequiredService<LaunchSpider>();
launch.Start(args);
Console.WriteLine("主线程运行完毕");
Console.ReadKey();