namespace MovieMania
{
	public interface IConfigManager
	{
		string get(string key, string defaultValue = "");
	}
}