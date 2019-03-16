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
	class MoviesViewModel : IMoviesViewModel
	{
		/*
			public static async Task<MoviesViewModel> createAsync()
			{
				MoviesViewModel moviesViewModel = new MoviesViewModel();
				await moviesViewModel.getMovies();
				return moviesViewModel;
			}
			*/

		public MoviesViewModel(IUpcomingMoviesService upcomingMovies)
		{
			this._upcomingMovies = upcomingMovies;
			System.Diagnostics.Debug.WriteLine("MoviesViewModel created");
		}

		public async Task loadMoviesIfNeeded(Movie movie = null)
		{
			if (shouldUpdate(movie))
			{
				System.Diagnostics.Debug.WriteLine($"obtendo mais vinte itens. Página {nextPage()}");
				try
				{
					int page = nextPage();
					_isLoading = true;
					List<Movie> listMovies = await _upcomingMovies.getList(page);
					listMovies.ForEach(m => _movies.Add(m));
					_page = page;
				}
				finally
				{
					_isLoading = false;
				}
			}
		}

		public bool isLast(Movie movie)
		{
			if (movies.Count == 0)
			{
				return true;
			}
			return (null != movie && movie.id == _movies.Last().id);
		}

		private int nextPage()
		{
			if (count == 0)
			{
				return 1;
			}
			return (count / 20) + 1;
		}

		private bool shouldUpdate(Movie movie)
		{
			if (isLoading || nextPage() > _upcomingMovies.totalPages)
			{
				return false;
			}

			return isLast(movie);
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

		public ObservableCollection<Movie> movies
		{
			get
			{
				return _movies;
			}
		}

		int _page = 0;
		private ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();
		private bool _isLoading = false;
		private IUpcomingMoviesService _upcomingMovies; // = new UpcomingMoviesService(new HttpClient());

	}
}