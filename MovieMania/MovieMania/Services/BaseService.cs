using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Tiny.RestClient;

namespace MovieMania
{
	class BaseService
	{

		private const String BASE_URL = "TMDB.BaseURL";

	public BaseService(HttpClient client, String path)
	{

			this._restClient = new TinyRestClient(client, $"{configManager.get(BASE_URL)}{configManager.get(path)}");
	}
	
		private ConfigManager configManager = ((App)App.Current).configManager;

		protected TinyRestClient _restClient;
	}
}
