namespace Egor_Population.Chromosome.Factory;

public class ChromosomeFactory : IChromosomeFactory
{
    private readonly int _chromosomeLength = 10;
    private readonly Random _random = new Random();

    public Chromosome Create()
    {
        var chromosome = new char[_chromosomeLength];

        for (int i = 0; i < _chromosomeLength; i++)
        {
            chromosome[i] = (char)(_random.Next(2) + '0');
        }

        return new Chromosome(new string(chromosome));
    }
}