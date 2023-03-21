namespace ConsoleApp1;

public class MyLogger: IDisposable
{
    private StreamWriter _sw;
    private FileStream _fs;
    public MyLogger()
    {
        _fs = File.Create(Directory.GetCurrentDirectory().Split("bin")[0] + "log.txt");
        _sw = new StreamWriter(_fs);
        
    }
    public void Dispose()
    {
        _sw.Dispose();
    }

    public void WriteLog(string message)
    {
        _sw.WriteLine(message);
    }
}