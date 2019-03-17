using Autofac.Extras.Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieMania;
using Moq;

namespace MovieMania.Tests
{
	[TestFixture]
	class GenresServiceTest
	{
		/*
			[Test]
			public async Task ShouldLoadGenres()
			{
					using (var mock = AutoMock.GetLoose())
					{
						IConfigManager configManager = setupConfigManager(mock).Object;

					}
			}

			private Mock<IConfigManager> setupConfigManager(AutoMock mock)
			{
					var configManagerMock = mock.Mock<IConfigManager>();
					configManagerMock.Setup(m => m.get(GenreService.PATH))
					.Returns("path");
					configManagerMock.Setup(m => m.get(GenreService.API_KEY))
					.Returns("apikey");
					return configManagerMock;
				}
			*/
	}
}
