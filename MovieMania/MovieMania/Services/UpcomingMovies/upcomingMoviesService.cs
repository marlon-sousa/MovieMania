using MovieMania.Services.UpcomingMovies;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace MovieMania
{

	class UpcomingMoviesService : IUpcomingMoviesService
	{
		private const String PATH = "TMDB.UpcomingURI";

		private const String API_KEY = "TMDB.Key";

		public UpcomingMoviesService(TinyRestClient client, IGenreService genreService, IConfigManager configManager)
		{
			this._genreService = genreService;
			this._restClient = client;
			this._configManager = configManager;
			System.Diagnostics.Debug.WriteLine("UpcomingMoviesService created");
		}

		public async Task<List<Movie>> getList(int page)
		{
			if (page > _totalPages)
			{
				throw new LastPageException();
			}
			UpcomingMovies movies = await _restClient.GetRequest(_configManager.get(PATH))
				.AddQueryParameter("api_key", _configManager.get(API_KEY))
				.AddQueryParameter("page", page)
				.ExecuteAsync<UpcomingMovies>();
			_totalPages = movies.total_pages;
			await resolveGenres(movies.results);
			return movies.results;
		}

		private async Task resolveGenres(List<Movie> movies)
		{
			foreach (Movie movie in movies)
			{
				movie.genresList = await _genreService.getGenresFromIds(movie.genre_ids);
			}
		}

		public int totalPages
		{
			get
			{
				return _totalPages;
			}
		}

		private IGenreService _genreService;
		private TinyRestClient _restClient;
		private IConfigManager _configManager;
		private int _totalPages = 1;

		class UpcomingMovies
		{
			public List<Movie> results { get; set; }
			public int total_pages { get; set; }
		}
	}
}
