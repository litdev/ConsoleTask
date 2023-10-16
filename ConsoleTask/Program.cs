using ConsoleTask;

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
    services.AddRemoteRequest(options =>
    {
        options.AddHttpClient("default", c =>
        {
            c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36 Edg/117.0.2045.60");
        });
    });
    services.AddSqlsugarSetup(App.Configuration);
}, includeWeb: false, args: args);

var launch = App.GetRequiredService<ILaunchSpider>();
launch.Start(args);
Console.WriteLine("主线程运行完毕");
Console.ReadKey();