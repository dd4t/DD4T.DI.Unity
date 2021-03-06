
[![AppVeyor](https://ci.appveyor.com/api/projects/status/github/dd4t/DD4T.DI.Unity?branch=master&svg=true&passingText=master)](https://ci.appveyor.com/project/DD4T/dd4t-di-unity)

[![AppVeyor](https://ci.appveyor.com/api/projects/status/github/dd4t/DD4T.DI.Unity?branch=develop&svg=true&passingText=develop)](https://ci.appveyor.com/project/DD4T/dd4t-di-unity)

# dd4t-di-unity

Unity Dependency injection container

## Release 2.5

- Upgraded reference to DD4T.Core


## How to 

1. Install Nuget package: `Install-Package DD4T.DI.Unity` [http://www.nuget.org/packages/DD4T.DI.Unity](http://www.nuget.org/packages/DD4T.DI.Unity "DD4T.DI.Unity")
2. Add `DD4T.DI.Unity` namespace to your usings
3. Call the `UseDD4T` method on your Unity `Microsoft.Practices.Unity.UnityContainer` interface.

>     IUnityContainer container = new UnityContainer();
>     //set all your custom apllication binding here.
>     
>     container.UseDD4T();



UseDD4T will Register all default class provided by the DD4T framework.

If you need to override the default classes: (i.e. the DefaultPublicationResovler) Register your class before the method call `UseDD4T`

>     IUnityContainer container = new UnityContainer();
>     //set all your custom apllication binding here.
>     container.RegisterType<IPublicationResolver, MyCustomPublicationResovler>(new ContainerControlledLifetimeManager());
>     container.UseDD4T();

