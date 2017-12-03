using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Blog.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      WebHost.CreateDefaultBuilder(args)
        .UseApplicationInsights()
        .UseUrls("http://*:5500")
        .UseStartup<Startup>()
        .Build()
        .Run();
    }
  }
}
