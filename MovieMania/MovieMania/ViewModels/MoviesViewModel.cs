using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieMania
{
    class MoviesViewModel
    {
		public static async Task<MoviesViewModel> createAsync()
		{
			List<Movie> listMovies = await upcomingMovies.getList();
			ObservableCollection<Movie> tmpCollection = new ObservableCollection<Movie>();
			foreach(Movie movie in listMovies)
			{
				tmpCollection.Add(movie);
			}
			return new MoviesViewModel(tmpCollection);
		}

		private MoviesViewModel(ObservableCollection<Movie> c)
		{
			this.movies = c;
		}

		public ObservableCollection<Movie> movies { get; set;  }
		private static UpcomingMoviesService upcomingMovies = new UpcomingMoviesService(new HttpClient());
    }
}
