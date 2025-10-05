using Microsoft.AspNetCore.SignalR;
using SignalR_PushNotification.Models;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;

namespace SignalR_PushNotification.Services
{
    public class NewsSqlNotificationService
    {
        private readonly string _connectionString;
        private readonly IHubContext<NewsHub> _hubContext;
        private SqlTableDependency<News> _tableDependency;

        public NewsSqlNotificationService(IHubContext<NewsHub> hubContext, IConfiguration config)
        {
            _hubContext = hubContext;
            _connectionString = config.GetConnectionString("connection");
        }

        public void Start()
        {
            try
            {
                _tableDependency = new SqlTableDependency<News>(_connectionString);
                _tableDependency.OnChanged += TableDependency_OnChanged;
                _tableDependency.OnError += TableDependency_OnError;
                _tableDependency.Start();

                Console.WriteLine("✅ SQLTableDependency started and listening for changes...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error starting SQLTableDependency: {ex.Message}");
            }
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<News> e)
        {
            try
            {
                string message = "";

                switch (e.ChangeType)
                {
                    case ChangeType.Insert:
                        message = $"🆕 New News: {e.Entity.Title} - {e.Entity.Message}";
                        break;
                    case ChangeType.Update:
                        message = $"✏️ Updated: {e.Entity.Title}";
                        break;
                    case ChangeType.Delete:
                        message = $"❌ News Deleted (ID {e.Entity.Id})";
                        break;
                }

                if (!string.IsNullOrEmpty(message))
                {
                    Console.WriteLine("📢 Sending: " + message);
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error in OnChanged: " + ex.Message);
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine("❌ SQLTableDependency Error: " + e.Error.Message);
        }

        public void Stop()
        {
            _tableDependency?.Stop();
        }
    }
}