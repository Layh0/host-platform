// App.xaml.cs
using System.Windows;
using practika_2.Data;

namespace practika_2.Wpf
{
    public partial class App : Application
    {
        public static DatabaseHelper Db { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Строка подключения к PostgreSQL
            string connectionString = "Host=localhost;Database=dbup2;Username=postgres;Password=sa";
            Db = new DatabaseHelper(connectionString);
        }
    }
}