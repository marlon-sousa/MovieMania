using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieMania
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetailsPage : ContentPage
	{
		private Movie movie;

		public MovieDetailsPage (Movie movie)
		{
			InitializeComponent ();
			this.movie = movie;
		}
		
		protected override void OnAppearing()
		{
			BindingContext = this.movie;
		}
	}
}