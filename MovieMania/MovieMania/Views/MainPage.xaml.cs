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

			await _moviesViewModel.loadMoviesIfNeeded();
		}

		private void onMovieSelected(object sender, ItemTappedEventArgs e)
		{
			MovieDetailsPage page = new MovieDetailsPage((Movie)e.Item);
			Navigation.PushAsync(page);
		}

		private async void onItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			await _moviesViewModel.loadMoviesIfNeeded(((Movie)e.Item));
		}

		private IMoviesViewModel _moviesViewModel = null;
	}
}