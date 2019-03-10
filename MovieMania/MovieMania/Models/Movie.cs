using System;
using System.Collections.Generic;
using System.Text;

namespace MovieMania
{
	class Movie
	{

		private const String imagesUriBase = "https://image.tmdb.org/t/p/w185_and_h278_bestv2";
		private string _posterPath;
		private string _backDropPath;

		public String genre
		{
			get
			{
				if (genres.Count > 1)
				{
					return genres[0];
				}
				else
				{
					return "undefinned";
				}
			}
		}

		public String title { get; set; }
		public string release_date { get; set; }
		public String poster_path
		{
			get
			{
				return _posterPath;
			}
			set
			{
				_posterPath = (imagesUriBase + value);
			}
		}

		public String backdrop_path
		{
			get
			{
				return _backDropPath;
			}
			set
			{
				_backDropPath = (imagesUriBase + value);
			}
		}

		public List<int> genre_ids { get; set; }

		public List<String> genres { get; set; }
	}
}
