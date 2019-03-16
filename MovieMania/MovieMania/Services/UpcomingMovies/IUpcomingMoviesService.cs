using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieMania
{
	interface IUpcomingMoviesService
	{
		Task<List<Movie>> getList(int page);
		int totalPages { get; }
	}
}