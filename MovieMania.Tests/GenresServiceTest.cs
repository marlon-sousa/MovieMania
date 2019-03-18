using Autofac.Extras.Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieMania;
using Moq;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Tiny.RestClient;
using System.Net.Http;

namespace MovieMania.Tests
{
	[TestFixture]
	class GenreServiceTest
	{
		private FluentMockServer _server;

		[OneTimeSetUp]
		public void StartMockServer()
		{
			_port = _randon.Next(35000, 35999);
			_server = FluentMockServer.Start(_port);
		}

		/*
		 * Attention:
		 * The GenreService is configured to update its cache should it last more than 24 hours.
		 * This is reazonable, since it wouldn't make sense to keep querying the genres definition end point over and over again for each movie.
		 * However, this causes problems in unit testing. Because we are creating several instances of the GenreService, the behaviour of having a centralized cache is showing up, which is exactly what we want in production.
		 * But for tests this is problematic because we are simulating downtimes in the genres Service. If we test the success call first, we can not test the failure call latter, because
		 * The genres data is already cached and so the failure call won't be even performed, unçless one wants to wait for 24 hours.
		 * So we need to execute the failure call first to prove that error handling is working and then the successful calls
		 */
		[Test, Order(1)]
		public async Task shouldReportEmptyListOfGenresOnFailure()
		{
			using (var mock = AutoMock.GetLoose())
			{
				IConfigManager configManager = setupConfigManager(mock).Object;
				setupGenresResponse();
				// configuring wrong mock server address
				GenreService genreService = new GenreService(new TinyRestClient(new HttpClient(), $"http://localhosts:{_port}"), configManager);
				List<int> ids = new List<int>() { 1, 2, 3 };
				List<String> genres = await genreService.getGenresFromIds(ids);
				Assert.That(genres.Count, Is.EqualTo(1));
				Assert.That(genres[0], Is.EqualTo("undefined"));
			}
		}


		[Test, Order(2)]
		public async Task ShouldLoadGenres()
		{
			using (var mock = AutoMock.GetLoose())
			{
				IConfigManager configManager = setupConfigManager(mock).Object;
				setupGenresResponse();
				GenreService genreService = new GenreService(new TinyRestClient(new HttpClient(), $"http://localhost:{_port}"), configManager);
				List<int> ids = new List<int>() { 1, 2, 3 };
				List<String> genres = await genreService.getGenresFromIds(ids);
				Assert.That(genres[0], Is.EqualTo("a"));
				Assert.That(genres[1], Is.EqualTo("c"));

			}
		}

		[Test]
		public async Task ShouldNotLoadUnknownGenresInGenresList()
		{
			using (var mock = AutoMock.GetLoose())
			{
				IConfigManager configManager = setupConfigManager(mock).Object;
				setupGenresResponse();
				GenreService genreService = new GenreService(new TinyRestClient(new HttpClient(), $"http://localhost:{_port}"), configManager);
				List<int> ids = new List<int>() { 1, 2, 3 };
				List<String> genres = await genreService.getGenresFromIds(ids);
				// response contains definitions for only two genres. We are asking for three
				Assert.That(genres.Count, Is.EqualTo(2));
			}
		}

		[Test]
		public async Task ShouldReturnGenreDescription()
		{
			using (var mock = AutoMock.GetLoose())
			{
				IConfigManager configManager = setupConfigManager(mock).Object;
				setupGenresResponse();
				GenreService genreService = new GenreService(new TinyRestClient(new HttpClient(), $"http://localhost:{_port}"), configManager);
				List<int> ids = new List<int>() { 1, 2, 3 };
				List<String> genres = await genreService.getGenresFromIds(ids);
				// response contains definitions for only two genres.
				Assert.That(genreService.getGenre(1), Is.EqualTo("a"));
				Assert.That(genreService.getGenre(2), Is.EqualTo("c"));
				Assert.That(genreService.getGenre(3), Is.EqualTo("undefined"));
			}
		}

		private Mock<IConfigManager> setupConfigManager(AutoMock mock)
		{
			var configManagerMock = mock.Mock<IConfigManager>();
			configManagerMock.Setup(m => m.get(GenreService.PATH, ""))
				.Returns("genreService");
			configManagerMock.Setup(m => m.get(GenreService.API_KEY, ""))
				.Returns("apikey");
			return configManagerMock;
		}


		void setupGenresResponse()
		{
			_server.Given(Request.Create().WithPath("/genreService").UsingGet())
				.RespondWith(
					Response.Create()
					.WithStatusCode(200)
					.WithBody(@"{ ""genres"": [{""id"": 1, ""name"": ""a""}, {""id"": 2, ""name"": ""c""}]}"));
		}

		[OneTimeTearDown]
		public void ShutdownServer()
		{
			_server.Stop();
		}

		private int _port = 0;
		private Random _randon = new Random();
	}
}
