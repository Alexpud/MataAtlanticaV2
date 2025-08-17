using MataAtlanticaV2.Domain.Entidades;

namespace MataAtlanticaV2.Domain.Repository;

public interface IProdutoRepository
{
    Task<List<Produto>> Listar();
}