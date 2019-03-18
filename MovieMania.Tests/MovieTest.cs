using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMania.Tests
{
	[TestFixture]
	public class MovieTest
	{
		[Test]
		public void shouldReturnFistGenre()
		{
			System.Collections.Generic.List<String> l = new System.Collections.Generic.List<String>
			{
				"genre1",
				"genre2"
			};
			Movie movie = new Movie();
			movie.genresList = l;
			Assert.That(movie.genre, Is.EqualTo("genre1"), "Genre property must return the first genre in the genres list");
		}

		[Test]
		public void shouldReturnUmndefinedAsGenre()
		{
			System.Collections.Generic.List<String> l = new System.Collections.Generic.List<String>();
			Movie movie = new Movie();
			movie.genresList = l;
			Assert.That(movie.genre, Is.EqualTo("umdefined"));
		}

		[Test]
		public void shouldReturnGenresAsString()
		{
			Movie movie = new Movie();
			movie.genresList = loadGenres("genre1", "genre2", "genre3");
			Assert.That(movie.genres, Is.EqualTo("genre1, genre2, genre3"));
		}

		private System.Collections.Generic.List<String> loadGenres(params String[] movies)
		{
			System.Collections.Generic.List<String> l = new System.Collections.Generic.List<String>();
			foreach (String m in movies)
			{
				l.Add(m);
			}
			return l;
		}

		[Test]
		public void ShouldConcatenateBaseUrlInImages()
		{
			Movie movie = new Movie();
			movie.backdrop_path = "/backdrop/1.jpg";
			movie.poster_path = "/poster/1.jpg";
			Assert.That(movie.backdrop_path, Is.EqualTo(Movie.IMAGES_URI_BASE + "/backdrop/1.jpg"));
			Assert.That(movie.poster_path, Is.EqualTo(Movie.IMAGES_URI_BASE + "/poster/1.jpg"));
		}
	}
}
