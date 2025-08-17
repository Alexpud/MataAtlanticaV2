using MataAtlanticaV2.Domain.Entidades;
using MataAtlanticaV2.Domain.Repository;

namespace MataAtlanticaV2.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    public Task<List<Produto>> Listar()
    {
        return Task.FromResult(new List<Produto>()
        {
            new Produto()
            {
                Id = Guid.NewGuid(),
            },
            new Produto()
            {
                Id = Guid.NewGuid(),
            }
        });
    }
}
