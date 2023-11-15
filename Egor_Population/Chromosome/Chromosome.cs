namespace Egor_Population.Chromosome;

public class Chromosome
{
    public readonly string BinaryRepresentation;
    public readonly int Profitability;
    
    public Chromosome(string binaryRepresentation)
    {
        BinaryRepresentation = binaryRepresentation;
    }

    public Chromosome(string binaryRepresentation, int profitability)
    {
        BinaryRepresentation = binaryRepresentation;
        Profitability = profitability;
    }
}