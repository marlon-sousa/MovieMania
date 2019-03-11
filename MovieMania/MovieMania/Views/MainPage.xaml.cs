using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieMania
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			lvMovies.ItemTapped += onMovieSelected;
			lvMovies.ItemAppearing += onItemAppearing;
		}
		
		protected override async void OnAppearing()
		{
		if(moviesViewModel == null)
		{
				moviesViewModel = await MoviesViewModel.createAsync();
				BindingContext = moviesViewModel;
		}
		}

		private void onMovieSelected(object sender, ItemTappedEventArgs e)
		{
			MovieDetailsPage page = new MovieDetailsPage((Movie)e.Item);
			Navigation.PushAsync(page);
		}

		private async void onItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			if (shouldUpdate(((Movie)e.Item)))
			{
				await moviesViewModel.getNext();
				System.Diagnostics.Debug.WriteLine("obtendo mais vinte itens");
			}

		}

		private bool shouldUpdate(Movie movie)
		{
			return (!moviesViewModel.isLoading && moviesViewModel.count != 0 && moviesViewModel.isLast(movie));
		}

		private MoviesViewModel moviesViewModel = null;
	}
}
