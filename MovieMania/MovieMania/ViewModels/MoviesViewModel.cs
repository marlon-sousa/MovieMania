using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieMania
{
	class MoviesViewModel
	{
		public static async Task<MoviesViewModel> createAsync()
		{
			MoviesViewModel moviesViewModel = new MoviesViewModel();
			await moviesViewModel.getMovies();
			return moviesViewModel;
		}

		private MoviesViewModel()
		{

		}

		public async Task getMovies(int page = 1)
		{
			_isLoading = true;
			try
			{
				List<Movie> listMovies = await upcomingMovies.getList(page);
				listMovies.ForEach(m => _movies.Add(m) );
				_page = page;
			}
			finally
			{
				_isLoading = false;
			}
		}

		public async Task getNext()
		{
			await getMovies(++ _page);
		}

		public bool isLast(Movie movie)
		{
		if(movies.Count ==0)
		{
		return true;
		}
			return (movie.id == _movies.Last().id);
		}

		public int count
		{
			get
			{
				return _movies.Count;
			}
		}

		public bool isLoading
		{
			get
			{
				return _isLoading;
			}
		}

		public ObservableCollection<Movie> movies {
		get
		{
				return _movies;
		}
		}

		int _page = 0;
		private ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();
		private bool _isLoading = false;
		private static UpcomingMoviesService upcomingMovies = new UpcomingMoviesService(new HttpClient());

	}
}