namespace ConsoleTask.Services;

public class DemoService : ITransient
{
    private readonly ILogger<DemoService> _logger;

    public DemoService(ILogger<DemoService> logger)
    {
        _logger = logger;
    }

    public async Task Test(int a, int b)
    {
        await Task.Delay(2000);
        Console.WriteLine($"【Demo1】，线程Id：【{Environment.CurrentManagedThreadId}】，Value:11111111");
    }
}