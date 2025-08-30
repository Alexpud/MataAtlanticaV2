namespace MataAtlanticaV2.Domain.Entidades;

public class Marca
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Cnpj { get; set; }

    public DateTime CriadoEm { get; set; }

    public DateTime? UltimaAtualizacao { get; set; }
}