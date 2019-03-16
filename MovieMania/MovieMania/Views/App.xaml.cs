using Autofac;
using MovieMania.AutoFac;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MovieMania
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			var builder = new ContainerBuilder();
			builder.RegisterModule(new DependenciesModule());
			_container = builder.Build();
			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}



		public  static IContainer Container
		{
		get
		{
				return _container;
		}
		}

		private static IContainer _container;
	}
}
