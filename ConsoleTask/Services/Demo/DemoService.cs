namespace ConsoleTask.Services;

public class DemoService : ITransient
{
    private readonly ILogger<DemoService> _logger;
    private readonly PushService _pushService;

    public DemoService(ILogger<DemoService> logger, PushService pushService)
    {
        _logger = logger;
        _pushService = pushService;
    }

    public async Task Test(int a, int b)
    {
        await Task.Delay(2000);
        Console.WriteLine($"【Demo1】，线程Id：【{Environment.CurrentManagedThreadId}】，Value:11111111");
        //模拟要循环的数据
        var modelList = new List<Model.TestModel>();
        for (int j = 1; j <= 37; j++)
        {
            modelList.Add(new TestModel()
            {
                Id = j,
                Name = $"名称{j}",
            });
        }

        //开始处理
        int i = 1;
        var taskList = new List<Task>();
        foreach (var item in modelList)
        {
            //每8条数据一组，执行
            if (i % 8 != 0)
            {
                taskList.Add(Task.Run(() => _pushService.Todo(item)));
                _logger.LogDebug($"不满一组，继续添加，当前是ModelList第{i}个，当前TaskList长度：{taskList.Count}");
            }
            else
            {
                taskList.Add(Task.Run(() => _pushService.Todo(item)));
                _logger.LogDebug($"满一组，等待线程执行完毕");
                try
                {
                    Task.WaitAll(taskList.ToArray());
                }
                catch (AggregateException ex)
                {
                    foreach (var item2 in ex.InnerExceptions)
                    {
                        _logger.LogError(item2, "发生异常");
                    }
                }
                finally
                {
                    taskList.Clear();
                }
            }

            i++;
        }
        if (taskList.Count > 0)
        {
            _logger.LogDebug($"最后不足一组，直接执行TaskList,当前TaskList长度：{taskList.Count}");
            try
            {
                Task.WaitAll(taskList.ToArray());
                taskList.Clear();
            }
            catch (AggregateException ex)
            {
                foreach (var item2 in ex.InnerExceptions)
                {
                    _logger.LogError(item2, "发生异常");
                }
            }
        }
    }
}

/// <summary>
/// 需要操作DB的服务
/// </summary>
public class PushService : ITransient
{
    private readonly ILogger<PushService> _logger;
    private readonly ISqlSugarClient _db;

    public PushService(ILogger<PushService> logger, ISqlSugarClient db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task Todo(TestModel model)
    {
        //因为上级调用使用了Task.WaitAll，这里必须使用_db.CopyNew()才能正常操作数据库，不然会报数据库连接已关闭之类的错误，例如：
        //await _db.CopyNew().Queryable<InquirySpider>().Where(x=>x.SId == 11).FirstAsync();
        //模拟耗时操作
        await Task.Delay(2000);
        Console.WriteLine($"线程Id：【{Environment.CurrentManagedThreadId}】，执行：{model.Id}----{model.Name}");
    }
}