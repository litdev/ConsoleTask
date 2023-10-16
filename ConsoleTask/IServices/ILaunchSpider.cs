namespace ConsoleTask.IServices;

public interface ILaunchSpider
{
    public Task Start(string[] args);
}