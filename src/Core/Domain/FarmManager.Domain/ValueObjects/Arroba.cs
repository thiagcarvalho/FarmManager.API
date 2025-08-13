namespace FarmManager.Domain.ValueObject;

public record Arroba
{
    public decimal Value { get; private set; }

    public Arroba(decimal weight)
    {
        Value = weight;
    }

    public decimal ToKilograms() => Value * 15.0m;
    public override string ToString()
    {
        return $"{Value}";
    }
}
