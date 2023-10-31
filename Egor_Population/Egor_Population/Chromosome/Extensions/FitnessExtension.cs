namespace Egor_Population.Chromosome.Extensions;

public static class FitnessExtension
{
    public static double CalculateFitness(this Chromosome chromosome) =>
        chromosome.BinaryRepresentation.Count(c => c == '1');
}