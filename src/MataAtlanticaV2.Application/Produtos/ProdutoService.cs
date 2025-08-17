using MataAtlanticaV2.Domain.Entidades;
using MataAtlanticaV2.Domain.Repository;

namespace MataAtlanticaV2.Application.Produtos;

public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
{
    public async Task<List<Produto>> Listar()
    {
        return await produtoRepository.Listar();
    }
}