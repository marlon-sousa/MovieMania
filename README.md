# MovieMania
Conceptual xamarin application listing upcoming movies


## what is it?

The application queries movies from the TMDB (The movies database) and shows a list of the upcoming titles.

On taping one item, a screen containing details of that movie is shown.


## Considerations

This is a highly conceptual, in progress project.

at this point, we do have a very good bases established to support future development. Specifically, dependencies within the project are being managed by a container, which makes it easier for testing.

Unit tests are also in place, with a very good ground for more integrated testing strategies.

ViewModels, models and Services are tested individually and services are already being tested with a Mock API server.


## external libraries

This project uses the following packages:

###[Autofac](https://autofac.org/)

The Autofac package is used to manage dependencies inside the project.

The container creates automatically, based on a well defined, isolated class, the whole graph of dependencies for each ViewModel. Views then only need to ask the container for the appropriate ViewModel and all the objects are constructed without extra coding.


###[Tiny.RestClient](https://github.com/jgiacomini/Tiny.RestClient)

This is a very nice, fluent and yet powerful HTTP rest client.

As .net does not seem to have a strong spec for this kind of client, I was not able to easily use interfaces to inject the rest client on the services.

Even though, the only coupling happens within the services, so should we need to change the rest client we would need to change only some lines on each services and also on their tests.


### [NUnit](https://nunit.org/)


Although this is nnot a dependency itself, integrating and using NUnit on my Visual Studio 2017 was not exactly easy.

First of all, the tests templates and runners were not installed. I updated everything I possibly could and, even so, no templates at all were made available.

I decided then to install the [NUnit Templates for Visual Studio](https://marketplace.visualstudio.com/items?itemName=NUnitDevelopers.NUnitTemplatesforVisualStudio).

This interestingly installed, alongside with a whole bunch of other stuff, the missing templates, even for MS Tests and XUnit).

Whether it also installed automatically other nuget packages I do not know.

By looking at my test project, I can see the NUnit and NUnit3TestAdapter packages installed.


### [Autofac.Extras.Moq](https://github.com/autofac/Autofac.Extras.Moq)

This was responsible for Mocks providing for the unit tests.


###[WireMock.Net](https://github.com/WireMock-Net/WireMock.Net)

This provided the http mock server to allow integrated tests for the services.


## build instructions

The project should run seamlessly on Visual Studio 2017. Te only additional procedure is to edit the json files containing static configuration for the projects.

These files are automatically embedded in the MovieMania main assembly as part of the build process and are located in the config folder of the MovieMania project and they are separated per build target, in debug and release sub folders. Right now the only thing you need to change is the api key configuration.


## conclusion

Thank you for checking this project. Feel free to give back advises and suggestions. We never stop to learn in technology, and this is one of the key things that make this one of the greatest areas of study and work.