using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MovieMania
{
	public class ConfigManager : IConfigManager
	{

		public static ConfigManager create()
		{
			if (_configManager == null)
			{
				string[] resourceNames = App.Current.GetType().Assembly.GetManifestResourceNames();

				foreach (string resourceName in resourceNames)
				{
					System.Diagnostics.Trace.WriteLine(resourceName);
				}
				Stream embeddedResourceStream = App.Current.GetType().Assembly.GetManifestResourceStream("ConfigManager.config.json");
				using (StreamReader streamReader = new StreamReader(embeddedResourceStream))
				{
					String jsonString = streamReader.ReadToEnd();
					Dictionary<String, String> configs = JsonConvert.DeserializeObject<Dictionary<String, String>>(jsonString);
					_configManager = new ConfigManager(configs);
				}
			}
			return _configManager;
		}

		private static ConfigManager _configManager = null;

		private ConfigManager(Dictionary<string, string> configs)
		{
			_configs = configs;
			System.Diagnostics.Debug.WriteLine("ConfigManager created");
		}

		public String get(String key, String defaultValue = "")
		{
			if (_configs.ContainsKey(key))
			{
				return _configs[key];
			}
			return defaultValue;
		}


		Dictionary<String, String> _configs;
	}
}
