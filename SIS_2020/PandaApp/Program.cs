namespace PandaApp
{
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using SIS.MvcFramework;

    public class Program
    {
        public static async Task Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            await WebHost.StartAsync(new Startup());
        }
    }
}
