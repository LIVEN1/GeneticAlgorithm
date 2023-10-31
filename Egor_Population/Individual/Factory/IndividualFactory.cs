namespace Egor_Population.Individual.Factory;

public class IndividualFactory : IIndividualFactory
{
    private const int COUNT_OF_CHROMOSOME = 10;

    public Individual Create(Chromosome.Chromosome param) => new Individual(param);

    public Individual Create(Tuple<Individual, Individual> param)
    {
        var random = new Random();
        var crossoverPoint = random.Next(1, COUNT_OF_CHROMOSOME - 1);
        var child = param.Item1.Chromosome.BinaryRepresentation.Substring(0, crossoverPoint) +
                    param.Item2.Chromosome.BinaryRepresentation.Substring(crossoverPoint);
        
        return Create(new Chromosome.Chromosome(child));
    }
}