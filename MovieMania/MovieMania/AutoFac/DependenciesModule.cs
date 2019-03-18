using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Autofac;
using Tiny.RestClient;

namespace MovieMania.AutoFac
{
	public class DependenciesModule : Module
	{

		private const String BASE_TMDB_URL = "TMDB.BaseURL";

		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			registerConfigManager(builder);
			builder.RegisterType<HttpClient>().AsSelf().SingleInstance();
			registerGenreService(builder);
			registerUpcomingMovieService(builder);
			builder.RegisterType<MoviesViewModel>().As<IMoviesViewModel>().SingleInstance();
		}

		private void registerConfigManager(ContainerBuilder builder)
		{
			builder.Register(c =>
			{

				Stream embeddedResourceStream = App.Current.GetType().Assembly.GetManifestResourceStream("ConfigManager.config.json");
				using (StreamReader streamReader = new StreamReader(embeddedResourceStream))
				{
					String jsonString = streamReader.ReadToEnd();
					return ConfigManager.create(jsonString);
				}
			}).As<IConfigManager>().SingleInstance();
		}

		private void registerGenreService(ContainerBuilder builder)
		{
			builder.Register(c =>
			{
				IConfigManager config = c.Resolve<IConfigManager>();
				String baseURL = config.get(BASE_TMDB_URL);
				return new GenreService(new TinyRestClient(c.Resolve<HttpClient>(), baseURL), config);
			}).As<IGenreService>().SingleInstance();
		}

		private void registerUpcomingMovieService(ContainerBuilder builder)
		{
			builder.Register(c =>
			{
				IConfigManager config = c.Resolve<IConfigManager>();
				String baseURL = config.get(BASE_TMDB_URL);
				return new UpcomingMoviesService(
				new TinyRestClient(c.Resolve<HttpClient>(), baseURL),
				c.Resolve<IGenreService>(),
				config);
			}).As<IUpcomingMoviesService>().SingleInstance();
		}
	}

}
