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
		protected const String API_KEY = "TMDB.Key";

	public BaseService(HttpClient client)
	{

			this._restClient = new TinyRestClient(client, $"{configManager.get(BASE_URL)}");
	}
	
		protected ConfigManager configManager = ((App)App.Current).configManager;

		protected TinyRestClient _restClient;
	}
}
