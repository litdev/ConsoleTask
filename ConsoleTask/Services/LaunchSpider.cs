namespace ConsoleTask.Services;

public class LaunchSpider : ITransient
{
    private readonly ILogger _logger;

    private readonly DemoService _demoService;
    private readonly Demo2Service _demo2Service;
    private readonly Demo3Service _demo3Service;
    private readonly IConfiguration _config;

    public LaunchSpider(ILogger<LaunchSpider> logger,
        DemoService demoService,
        Demo2Service demo2Service,
        Demo3Service demo3Service,
        IConfiguration config)
    {
        _logger = logger;
        _demoService = demoService;
        _demo2Service = demo2Service;
        _demo3Service = demo3Service;
        _config = config;
    }

    public async Task Start(string[] args)
    {
        if (args.Length != 0)
        {
            _logger.LogInformation("启动参数：" + string.Join('/', args));
        }

        var taskList = new List<Task>();
        taskList.Add(Task.Run(() => _demoService.Test(1, 2)));
        taskList.Add(Task.Run(() => _demo2Service.Test(3, 2)));
        taskList.Add(Task.Run(() => _demo3Service.Test(44, 2)));


        try
        {
            Task.WaitAll(taskList.ToArray());
        }
        catch (AggregateException ex)
        {
            foreach (var item in ex.InnerExceptions)
            {
                _logger.LogError(item, "发生异常");
            }
        }

        _logger.LogInformation("所有服务启动完成");

        if (App.Configuration.Get<bool>("AutoExit")) Environment.Exit(0);
    }
}