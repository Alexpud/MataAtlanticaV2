using MataAtlanticaV2.Domain.Entidades;

namespace MataAtlanticaV2.Application.Produtos.Dtos;

public class ProdutoDto
{
    public string Id { get; set; }
    public static ProdutoDto CreateFrom(Produto produto)
    {
        return new ProdutoDto();
    }
}