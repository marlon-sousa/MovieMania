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

		public static ConfigManager create(String jsonString)
		{
			if (_configManager == null)
			{
				Dictionary<String, String> configs = JsonConvert.DeserializeObject<Dictionary<String, String>>(jsonString);
				_configManager = new ConfigManager(configs);
			}
			return _configManager;
		}

		private static ConfigManager _configManager = null;

		private ConfigManager(Dictionary<string, string> configs)
		{
			_configs = configs;
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
