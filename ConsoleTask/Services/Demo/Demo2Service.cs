namespace ConsoleTask.Services;

public class Demo2Service : ITransient
{
    private readonly ILogger<Demo2Service> _logger;

    public Demo2Service(ILogger<Demo2Service> logger)
    {
        _logger = logger;
    }

    public async Task Test(int a, int b)
    {
        throw new Exception("测试Task中异常捕捉");
        await Task.Delay(2000);
        Console.WriteLine($"【Demo2】，线程Id：【{Environment.CurrentManagedThreadId}】，Value:2222");
    }
}