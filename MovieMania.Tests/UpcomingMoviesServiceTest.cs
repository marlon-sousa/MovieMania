using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tiny.RestClient;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using MovieMania;

namespace MovieMania.Tests
{
	[TestFixture]
	class UpcomingMoviesServiceTest
	{

		[OneTimeSetUp]
		public void StartMockServer()
		{
			_port = _randon.Next(35000, 35999);
			_server = FluentMockServer.Start(_port);
		}

		[Test]
		public async Task ShouldThrowLastPageException()
		{
			using (var mock = AutoMock.GetLoose())
			{
				IConfigManager configManager = setupConfigManager(mock).Object;
				IGenreService genreService = setupGenreService(mock).Object;
				setupUpcomingServiceResponse();
				// default number of pages is 1
				UpcomingMoviesService upcomingMoviesService = new UpcomingMoviesService(new TinyRestClient(new HttpClient(), $"http://localhost:{_port}"), genreService, configManager);
				Assert.That(async () => await upcomingMoviesService.getList(3),
				Throws.TypeOf<MovieMania.LastPageException>());
			}
		}

		[Test]
		public async Task ShouldLoadMovies()
		{
			using (var mock = AutoMock.GetLoose())
			{
				IConfigManager configManager = setupConfigManager(mock).Object;
				IGenreService genreService = setupGenreService(mock).Object;
				setupUpcomingServiceResponse();
				UpcomingMoviesService upcomingMoviesService = new UpcomingMoviesService(new TinyRestClient(new HttpClient(), $"http://localhost:{_port}"), genreService, configManager);
				List<Movie> movies = await upcomingMoviesService.getList(1);
				Assert.That(movies.Count, Is.EqualTo(20));
			}
		}

		private Mock<IConfigManager> setupConfigManager(AutoMock mock)
		{
			var configManagerMock = mock.Mock<IConfigManager>();
			configManagerMock.Setup(m => m.get(UpcomingMoviesService.PATH, ""))
				.Returns("upcomingMoviesService");
			configManagerMock.Setup(m => m.get(GenreService.API_KEY, ""))
				.Returns("apikey");
			return configManagerMock;
		}

		private void setupUpcomingServiceResponse()
		{
			UpcomingMovies upcomingMovies = new UpcomingMovies();
			List<Movie> movies = new List<Movie>();
			upcomingMovies.total_pages = 2;
			for (int i = 1; i <= 20; ++i)
			{
				movies.Add(new Movie()
				{
					genre_ids = new List<int>() { 1, 2, 3 },
					title = $"movie{i}",
					id = i
				});
			}
			upcomingMovies.results = movies;
			_server.Given(Request.Create().WithPath("/upcomingMoviesService").UsingGet())
					.RespondWith(
						Response.Create()
						.WithStatusCode(200)
						.WithBodyAsJson(upcomingMovies));
		}


		private Mock<IGenreService> setupGenreService(AutoMock mock)
		{
			var genreServiceMock = mock.Mock<IGenreService>();
			genreServiceMock.Setup(srv => srv.getGenresFromIds(It.IsAny<List<int>>()))
			.ReturnsAsync(new List<string>() { "a", "b", "c" });
			return genreServiceMock;
		}

		[OneTimeTearDown]
		public void ShutdownServer()
		{
			_server.Stop();
		}

		private FluentMockServer _server;
		private Random _randon = new Random();
		private int _port;
	}
}
