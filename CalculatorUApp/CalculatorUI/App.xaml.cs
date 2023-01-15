using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CalculatorUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Process process = Process.GetCurrentProcess();
            if (Process.GetProcesses().Where(p => p.ProcessName == process.ProcessName).Count() > 1)
            {
                App.Current.Shutdown();
            }
        }
    }
}
