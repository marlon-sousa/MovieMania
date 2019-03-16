using System;
using System.Collections.Generic;
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
			builder.Register(c => ConfigManager.create()).As<IConfigManager>().SingleInstance();
			builder.RegisterType<HttpClient>().AsSelf().SingleInstance();
			registerGenreService(builder);
			registerUpcomingMovieService(builder);
			builder.RegisterType<MoviesViewModel>().As<IMoviesViewModel>().SingleInstance();
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
