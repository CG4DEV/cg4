using System.Collections.Generic;
using System.Linq;
using CG4.Impl.Kafka.Consumer;
using Microsoft.Extensions.Configuration;
using ProjectName.Core.Common;

namespace ProjectName.Consumer.WebApp
{
    public interface IAppSettings
    {
        
    }
    
    public class AppSettings : IAppSettings, IKafkaConsumerSettings, IConnectionSettings
    {
        public AppSettings(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("ProjectName");
            MaxTimeoutMsec = config.GetValue<int>("Kafka:MaxTimeoutMsec");
            MaxThreadsCount = config.GetValue<int>("Kafka:MaxThreadsCount");

            Config = config.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);
            
            ApplyKafkaAliases(config, Config);
        }

        public int MaxTimeoutMsec { get; set; }
        
        public int MaxThreadsCount { get; set; }
        
        public Dictionary<string, string> Config { get; set; }
        
        public string ConnectionString { get; set; }
        
        private static void ApplyKafkaAliases(IConfiguration configuration, Dictionary<string, string> config)
        {
            var aliases = configuration.GetSection("Kafka:Aliases").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            foreach (var item in aliases)
            {
                var value = configuration[$"Kafka:{item.Key}"];

                if (!string.IsNullOrEmpty(value))
                {
                    config[item.Value] = value;
                }
            }
        }
    }
}