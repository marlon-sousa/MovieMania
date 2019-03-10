using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Tiny.RestClient;

namespace MovieMania
{
	class BaseService
	{

	public BaseService(HttpClient client, String baseUri)
	{
			this._restClient = new TinyRestClient(client, baseUri);
	}

		protected TinyRestClient _restClient;
	}
}
