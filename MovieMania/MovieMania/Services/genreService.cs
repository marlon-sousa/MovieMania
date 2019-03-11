using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieMania
{
	class GenreService : BaseService
	{

		private const String PATH = "TMDB.GenresURI";

		public GenreService(HttpClient client) : base(client)
		{

		}

		public async Task<List<String>> getGenresFromIds(List<int> ids)
		{
			if (mustUpdateGenres())
			{
				await updateGenres();
			}
			List<String> genres = new List<string>();
			ids.ForEach(id =>
			{
				if (_genres.ContainsKey(id))
				{
					genres.Add(_genres[id]);
				}
			});
			if(genres.Count == 0)
			{
				genres.Add("undefined");
			}
				return genres;
		}

public String getGenre(int id)
{
if (_genres.ContainsKey(id))
{
			return _genres[id];
			}
			return "undefinned";
}



private async Task updateGenres()
{
			/*
			 * If we reached at this point then the list of human readable genres either is not retrieved at all or is too old.
			 * We must update it. Although the list of genres is unlikely to change, new genres may be added over time. As the amount of data
			 * transferred is not big, we can update it once a day.
			 * We will create a new dictionary and assign the new reference to the static dictionary here.
			 * This will ensure that other threads reading the dictionary won't be reading an object being changed.
			 * Although there is a theoretical probability of a memory viollation if the assignment operation isn't athomic, its likelihood is too small
			 * to justify a full implementation with all the locks needed to prevent it. The risks of that implementation would be greater than the risks of not implementing it.
			 */
			await semaphoreSlim.WaitAsync();
			try
			{ 
				if(! mustUpdateGenres())
					{ // someone else was updating while this thread was waiting
					return;
				}
				Dictionary<int, String> d = new Dictionary<int, string>();
				Genres genres = await _restClient.GetRequest(configManager.get(PATH))
				.AddQueryParameter("api_key", configManager.get(API_KEY))
					.ExecuteAsync<Genres>();
				genres.genres.ForEach (g => d[g.id] = g.name );
				_genres = d;
				_dateTime = DateTime.Now;
			}
			finally
			{
				semaphoreSlim.Release();
			}
}

private bool mustUpdateGenres()
{
			return (_dateTime == DateTime.MinValue|| (DateTime.Now.Subtract(_dateTime).TotalDays >= 1));
}

		private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
		private static Dictionary<int, String> _genres = new Dictionary<int, string>();
		private static DateTime _dateTime = DateTime.MinValue;

		class Genres
		{
		public List<Genre> genres { get; set; }
		}
	}
}
