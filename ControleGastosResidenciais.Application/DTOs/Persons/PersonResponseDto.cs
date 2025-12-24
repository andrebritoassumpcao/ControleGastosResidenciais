namespace ControleGastosResidenciais.Application.DTOs.Persons;
public class PersonResponseDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public int Age { get; init; }
}
