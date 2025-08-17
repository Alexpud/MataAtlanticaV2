namespace MataAtlanticaV2.Domain.Entidades;

public class Produto
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Categoria { get; set; }

    public string Marca { get; set; }

    public DateTime CriadoEm { get; set; }

    public DateTime? UltimaAtualizacao { get; set; }
}