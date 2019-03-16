using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieMania
{
	interface IGenreService
	{
		string getGenre(int id);
		Task<List<string>> getGenresFromIds(List<int> ids);
	}
}