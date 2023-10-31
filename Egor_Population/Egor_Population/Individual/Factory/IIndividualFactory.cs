using Egor_Population.Common;

namespace Egor_Population.Individual.Factory;

public interface IIndividualFactory : IFactory<Individual, Chromosome.Chromosome>, IFactory<Individual, Tuple<Individual, Individual>>
{
    
}