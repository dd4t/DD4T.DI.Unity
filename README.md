
[![AppVeyor](https://ci.appveyor.com/api/projects/status/github/dd4t/dd4t-di-unity?branch=master&svg=true&passingText=master)](https://ci.appveyor.com/project/DD4T/dd4t-di-unity)

[![AppVeyor](https://ci.appveyor.com/api/projects/status/github/dd4t/dd4t-di-unity?branch=develop&svg=true&passingText=develop)](https://ci.appveyor.com/project/DD4T/dd4t-di-unity)

# dd4t-di-unity

Unity Dependency injection container



## How to 

1. Install Nuget package: `Install-Package DD4T.DI.Unity` [http://www.nuget.org/packages/DD4T.DI.Unity](http://www.nuget.org/packages/DD4T.DI.Unity "DD4T.DI.Unity")
2. Add `DD4T.DI.Unity` namespace to your usings;
3. Call the `UseDD4T` method on your Unity `Microsoft.Practices.Unity.UnityContainer` interface.

>     UnityContainer container = new UnityContainer();
>     //set all your custom apllication binding here.
>     
>     container.UseDD4T();



UseDD4T will Register all default class provided by the DD4T framework.

If you need to override the default classes: (i.e. the DefaultPublicationResovler) Register your class before the method call `UseDD4T`

>     UnityContainer container = new UnityContainer();
>     //set all your custom apllication binding here.
>     container.RegisterType<IPublicationResolver, MyCustomPublicationResovler>(new ContainerControlledLifetimeManager());
>     container.UseDD4T();

