namespace ConsoleTask.Services;

public class Demo3Service : ITransient
{
    private readonly ISqlSugarClient _db;

    public Demo3Service(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task Test(int a, int b)
    {
        //Any查询
        //_db.Queryable<InquirySpider>().Any(x => x.Title == "demo");
        //添加
        //await _db.Insertable(new InquirySpider() { }).ExecuteCommandAsync();

        await Task.Delay(1500);
        Console.WriteLine($"【Demo3】，线程Id：【{Environment.CurrentManagedThreadId}】，Value:3333333");
    }
}