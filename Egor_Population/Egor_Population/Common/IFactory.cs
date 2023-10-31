namespace Egor_Population.Common;

public interface IFactory<out T>
{
    T Create();
}

public interface IFactory<out T, in TParam>
{
    T Create(TParam param);
}