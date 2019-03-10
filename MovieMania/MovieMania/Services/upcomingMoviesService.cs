using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace MovieMania
{

	class UpcomingMoviesService : BaseService
	{
	private const String baseUri = "http://192.168.0.104/movies";

	public UpcomingMoviesService(HttpClient client): base(client, baseUri)
	{
			this._genreService = new GenreService(client);
	}

	public async Task<List<Movie>> getList()
	{
			UpcomingMovies movies = await _restClient.GetRequest("upcoming.json")
			.ExecuteAsync<UpcomingMovies>();
			await resolveGenres(movies.results);
			return movies.results;
		}

		private async Task resolveGenres(List<Movie> movies)
		{
		foreach(Movie movie in movies)
		{
				movie.genres = await _genreService.getGenresFromIds(movie.genre_ids);
		}
		}

		private GenreService _genreService;

		class UpcomingMovies
		{
			public List<Movie> results { get; set; }
		}
	}
}
