using Egor_Population.Chromosome.Extensions;
using Egor_Population.Chromosome.Factory;
using Egor_Population.Individual.Factory;
using Microsoft.Extensions.Logging;

namespace Egor_Population;

public class PopulationManager
{
    private readonly int _populationSize = 100;

    public IReadOnlyList<Individual.Individual> Individuals => _individuals;
    private readonly List<Individual.Individual> _individuals = new List<Individual.Individual>();

    public IReadOnlyList<Individual.Individual> CurrentGenerationPopulation => _currentGenerationPopulation;
    private readonly List<Individual.Individual> _currentGenerationPopulation = new List<Individual.Individual>();

    private int _currentGeneration = 0;

    private readonly IChromosomeFactory _chromosomeFactory;
    private readonly IIndividualFactory _individualFactory;
    private readonly ILogger<PopulationManager> _logger;

    public PopulationManager(IChromosomeFactory chromosomeFactory,
        IIndividualFactory individualFactory, ILogger<PopulationManager> logger)
    {
        _chromosomeFactory = chromosomeFactory;
        _individualFactory = individualFactory;
        _logger = logger;
    }

    public IReadOnlyList<Individual.Individual> ToCurrentGeneration(int lastGeneration)
    {
        _currentGenerationPopulation.Clear();
        var bestFitness = 0.0;

        for (var generation = _currentGeneration; generation < lastGeneration; generation++)
        {
            var parent1 = SelectParent();
            var parent2 = SelectParent(parent1);
            var child = _individualFactory.Create(
                new Tuple<Individual.Individual, Individual.Individual>(parent1, parent2));
            var mutatedChromosome = child.Chromosome.Mutate();
            var bestFitnessInGeneration = mutatedChromosome.CalculateFitness();
            if (bestFitness < bestFitnessInGeneration)
                bestFitness = bestFitnessInGeneration;

            var mutatedChild = new Individual.Individual(mutatedChromosome);
            _currentGenerationPopulation.Add(mutatedChild);
            _individuals.Add(mutatedChild);

            _currentGeneration++;
            _logger.LogInformation(
                "Generation : {generation} | Best Fitness : {bestFitness}", _currentGeneration,
                bestFitnessInGeneration);
        }

        _logger.LogInformation("Best Chromosome: {bestChromosome}",
            _currentGenerationPopulation.Max(x => x.Chromosome.BinaryRepresentation));
        _logger.LogInformation("Best Fitness: {bestFitness}", bestFitness);

        return _currentGenerationPopulation;
    }


    public IReadOnlyList<Individual.Individual> GenerateInitialPopulation()
    {
        _logger.LogInformation("Generation Initial Population");

        for (int i = 0; i < _populationSize; i++)
        {
            var chromosome = _chromosomeFactory.Create();
            var individual = _individualFactory.Create(chromosome);
            _logger.LogInformation("Created Individual With Fitness: {fitness} and Chromosome: {chromosome}",
                chromosome.CalculateFitness(), chromosome.BinaryRepresentation);
            _individuals.Add(individual);
        }
        
        return _individuals;
    }

    private Individual.Individual SelectParent(Individual.Individual currentIndividual = null)
    {
        var totalFitness = _individuals.Sum(individual => individual.Chromosome.CalculateFitness());
        var random = new Random();
        var randomValue = random.NextDouble() * totalFitness;
        var cumulativeFitness = 0.0;

        foreach (var individual in _individuals)
        {
            if (individual == currentIndividual)
                continue;

            cumulativeFitness += individual.Chromosome.CalculateFitness();
            if (cumulativeFitness >= randomValue)
                return individual;
        }

        return _individuals.Last();
    }
}