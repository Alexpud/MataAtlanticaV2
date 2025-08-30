namespace MataAtlanticaV2.Domain.Entidades;

public class ItemProduto
{
    public Guid Id { get; set; }

    public string CodigoDeBarra { get; set; }

    public Produto Produto { get; set; }
}
