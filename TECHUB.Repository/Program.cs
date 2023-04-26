// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using TECHUB.Repository.Context;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddDbContext<DatabaseContext>();