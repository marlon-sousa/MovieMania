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
		}
		
		protected override async void OnAppearing()
		{
		if(moviesViewModel == null)
		{
				moviesViewModel = await MoviesViewModel.createAsync();
				BindingContext = moviesViewModel;
		}
		}

		private MoviesViewModel moviesViewModel = null;
	}
}
