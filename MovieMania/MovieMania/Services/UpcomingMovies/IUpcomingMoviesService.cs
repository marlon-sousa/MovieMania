using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieMania
{
	public interface IUpcomingMoviesService
	{
		Task<List<Movie>> getList(int page);
		int totalPages { get; }
	}
}