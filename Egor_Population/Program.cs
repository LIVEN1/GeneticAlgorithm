using Egor_Population;
using Egor_Population.Chromosome.Factory;
using Egor_Population.Individual.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceCollection = new ServiceCollection();

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
        .AddConsole();
});

var populationLogger = loggerFactory.CreateLogger<PopulationManager>();
serviceCollection.AddSingleton<ILogger<PopulationManager>>(x => populationLogger);

serviceCollection.AddSingleton<PopulationManager>();
serviceCollection.AddSingleton<IIndividualFactory, IndividualFactory>();
serviceCollection.AddSingleton<IChromosomeFactory, ChromosomeFactory>();

var serviceProvider = serviceCollection.BuildServiceProvider();
var populationManager = serviceProvider.GetRequiredService<PopulationManager>();

populationManager.GenerateInitialPopulation();
var generation = int.TryParse(Console.ReadLine(), out var gen);
if (generation)
    populationManager.ToCurrentGeneration(gen);
else
    populationLogger.LogError("This is not a digit");