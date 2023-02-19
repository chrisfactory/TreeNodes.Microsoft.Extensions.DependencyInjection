# TreeNodes.Microsoft.Extensions.DependencyInjection


This package makes it possible to make a stack of expenses modular in the form of nodes (CreateNode).
Nodes can be added or merged like reusable bricks.
In the case of singleton instances, only one instance will be built and shared.
For example, it is possible to create a logging policy as follows:
       
       var loggingNode = new ServiceCollection()
                                     .AddLogging(b => b.AddConsole())
                                     .CreateNode("logging.strategy");

       var services = new ServiceCollection() + loggingNode;
Or
       
       var services = new ServiceCollection()
       loggingNode.ConnectTo(services);//to inject the stack
Or
       
       var services = loggingNode.CreateBranch();

The logging brick can be retrieved later via the provider:

       services. GetNode("logging.strategy");


Go to [samples](https://github.com/chrisfactory/TreeNodes.Microsoft.Extensions.DependencyInjection/tree/master/Samples) for more details..

How to get it
--------------------------------
Use NuGet Package Manager to install the package or use any of the following commands in NuGet Package Manager Console.
 
[![Nuget.org client](http://img.shields.io/nuget/v/TreeNodes.Microsoft.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/TreeNodes.Microsoft.Extensions.DependencyInjection/)
```	
PM> Install-Package TreeNodes.Microsoft.Extensions.DependencyInjection
```
## Status
[![CI status](https://github.com/chrisfactory/TreeNodes.Microsoft.Extensions.DependencyInjection/workflows/CI/badge.svg)](https://github.com/chrisfactory/TreeNodes.Microsoft.Extensions.DependencyInjection/actions/workflows/ci-build-analysis.yml?query=branch%3Amaster)

[![Publish status](https://github.com/chrisfactory/TreeNodes.Microsoft.Extensions.DependencyInjection/workflows/publish-nuget/badge.svg)](https://github.com/chrisfactory/TreeNodes.Microsoft.Extensions.DependencyInjection/actions/workflows/release.yml)
## License
[MIT](https://choosealicense.com/licenses/mit/)