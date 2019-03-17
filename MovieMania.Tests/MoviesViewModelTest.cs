using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieMania;

namespace MovieMania.Tests
{
	[TestFixture]
	public class MoviesViewModelTest
	{

		[Test]
		public async Task shouldLoadMoviesOnStartup()
		{
			using (var mock = AutoMock.GetLoose())
			{
				var m = setupUpcomingMoviesMock(mock, 2);
				MoviesViewModel moviesViewModel = new MoviesViewModel(m.Object);
				await moviesViewModel.loadMoviesIfNeeded();
				Assert.That(moviesViewModel.count, Is.EqualTo(20));
			}
		}

		[Test]
		public async Task ShouldQueryMoviesWhenLastMovieIsShown()
		{
			using (var mock = AutoMock.GetLoose())
			{
				var m = setupUpcomingMoviesMock(mock, 2);
				MoviesViewModel moviesViewModel = new MoviesViewModel(m.Object);
				await moviesViewModel.loadMoviesIfNeeded();
				Assert.That(moviesViewModel.count, Is.EqualTo(20));
				Movie movie = new Movie()
				{
					id = 20
				};
				await moviesViewModel.loadMoviesIfNeeded(movie);
				Assert.That(moviesViewModel.count, Is.EqualTo(40));
			}
		}

		[Test]
		public async Task ShouldStopUpdatingWhenLastServicePageIsReached()
		{
			using (var mock = AutoMock.GetLoose())
			{
				var m = setupUpcomingMoviesMock(mock, 1);
				MoviesViewModel moviesViewModel = new MoviesViewModel(m.Object);
				await moviesViewModel.loadMoviesIfNeeded();
				Movie movie = new Movie()
				{
					id = 20
				};
				await moviesViewModel.loadMoviesIfNeeded(movie);
				Assert.That(moviesViewModel.count, Is.EqualTo(20));
			}
		}

		private Mock<IUpcomingMoviesService> setupUpcomingMoviesMock(AutoMock mock, int numPages)
		{
			var m = mock.Mock<IUpcomingMoviesService>();
			for (int i = 1; i <= numPages; i++)
			{
				m.Setup(srv => srv.getList(i))
				.ReturnsAsync(buildMoviesList(i));
			}
			m.Setup(srv => srv.totalPages)
			.Returns(numPages);
			return m;
		}

		private List<Movie> buildMoviesList(int page)
		{
			List<Movie> movies = new List<Movie>();
			for (int i = (page - 1) * 20; i < page * 20; ++i)
			{
				Movie movie = new Movie()
				{
					title = $"Movie{i + 1}",
					id = i + 1
				};
				movies.Add(movie);
			}
			return movies;
		}
	}
}