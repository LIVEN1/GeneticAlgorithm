namespace Egor_Population.Chromosome.Extensions;

public static class MutationExtension
{
    private const double MUTATION_RATE = 0.1;
    
    public static Chromosome Mutate(this Chromosome chromosome)
    {
        var mutatedChromosome = chromosome.BinaryRepresentation.ToCharArray();
        var random = new Random();

        var profitability = random.Next(1000, 3000);

        for (int i = 0; i < 10; i++)
        {
            if (random.NextDouble() < MUTATION_RATE)
            {
                mutatedChromosome[i] = (mutatedChromosome[i] == '0') ? '1' : '0';
            }
        }

        return new Chromosome(new string(mutatedChromosome), profitability);
    }
}