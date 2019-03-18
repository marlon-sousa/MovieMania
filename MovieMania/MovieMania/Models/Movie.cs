using System;
using System.Collections.Generic;
using System.Text;

namespace MovieMania
{
	public class Movie
	{

		public const String IMAGES_URI_BASE = "https://image.tmdb.org/t/p/w185_and_h278_bestv2";
		private string _posterPath;
		private string _backDropPath;

		public String genre
		{
			get
			{
				if (null != genresList && genresList.Count > 0)
				{
					return genresList[0];
				}
				return "umdefined";
			}
		}

		public String genres
		{
			get
			{
				if (null != genresList)
				{
					return String.Join(", ", genresList);
				}
				return "undefined";
			}
		}

		public String title { get; set; }
		public int id { get; set; }
		public String overview { get; set; }
		public string release_date { get; set; }
		public String poster_path
		{
			get
			{
				return _posterPath;
			}
			set
			{
				_posterPath = (IMAGES_URI_BASE + value);
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
				_backDropPath = (IMAGES_URI_BASE + value);
			}
		}

		public List<int> genre_ids { get; set; }

		public List<String> genresList { get; set; }
	}
}
