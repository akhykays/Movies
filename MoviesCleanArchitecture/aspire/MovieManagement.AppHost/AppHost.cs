// https://codewithmukesh.com/blog/clean-architecture-dotnet/

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MovieManagement_Api>("api");

builder.Build().Run();
