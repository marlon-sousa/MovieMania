using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MovieMania
{
	interface IMoviesViewModel
	{
		int count { get; }
		bool isLoading { get; }
		ObservableCollection<Movie> movies { get; }

		Task loadMoviesIfNeeded(Movie movie = null);
		bool isLast(Movie movie);
	}
}