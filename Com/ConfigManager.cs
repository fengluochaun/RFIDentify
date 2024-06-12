using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Com
{
	public class ConfigManager
	{
		// 获取字符串列表
		public static List<string> GetStringListFromConfig(string key)
		{
			string value = System.Configuration.ConfigurationManager.AppSettings[key]!;
			if (!string.IsNullOrEmpty(value))
			{
				return value.Split(',').ToList(); 
			}
			return new List<string>();
		}

		// 保存字符串列表
		public static void SaveStringListToConfig(string key, List<string> list)
		{
			string value = string.Join(",", list);
			SaveValueToConfig(key, value);
		}

		// 获取字符串
		public static string GetStringFromConfig(string key, string defaultValue = "")
		{
			return ConfigurationManager.AppSettings[key] ?? defaultValue;
		}

		// 获取整数
		public static int GetIntFromConfig(string key, int defaultValue = 0)
		{
			if (int.TryParse(ConfigurationManager.AppSettings[key], out int result))
			{
				return result;
			}
			return defaultValue;
		}

		// 获取布尔值
		public static bool GetBoolFromConfig(string key, bool defaultValue = false)
		{
			if (bool.TryParse(ConfigurationManager.AppSettings[key], out bool result))
			{
				return result;
			}
			return defaultValue;
		}

		// 保存值
		public static void SaveValueToConfig(string key, string value)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (config.AppSettings.Settings[key] != null)
			{
				config.AppSettings.Settings[key].Value = value;
			}
			else
			{
				config.AppSettings.Settings.Add(key, value);
			}
			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}

		// 删除配置项
		public static void RemoveValueFromConfig(string key)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (config.AppSettings.Settings[key] != null)
			{
				config.AppSettings.Settings.Remove(key);
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings");
			}
		}

		// 检查配置项是否存在
		public static bool ConfigKeyExists(string key)
		{
			return ConfigurationManager.AppSettings[key] != null;
		}
	}
}
