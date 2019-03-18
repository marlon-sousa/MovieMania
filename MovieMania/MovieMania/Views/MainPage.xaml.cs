using Autofac;
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

			if (_moviesViewModel == null)
			{
				_moviesViewModel = App.Container.Resolve<IMoviesViewModel>();
				BindingContext = _moviesViewModel;
			}

			try
			{
				await _moviesViewModel.loadMoviesIfNeeded();
			}
			catch (Exception ex)
			{ // failed to query movies
				if (_moviesViewModel.count == 0)
				{
					await DisplayAlert("Error", "Unable to query list of upcoming movies.\n Please try again in some minuts", "Ok");
				}
			}
		}

		private void onMovieSelected(object sender, ItemTappedEventArgs e)
		{
			MovieDetailsPage page = new MovieDetailsPage((Movie)e.Item);
			Navigation.PushAsync(page);
		}

			private async void onItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			try
			{
				await _moviesViewModel.loadMoviesIfNeeded(((Movie)e.Item));
			}
			catch (Exception ex)
			{
				// we have failed to query movies, but the list already contains items. Fail silently.
			}
		}

		private IMoviesViewModel _moviesViewModel = null;
	}
}