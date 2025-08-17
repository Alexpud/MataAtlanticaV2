using MataAtlanticaV2.Domain.Entidades;

namespace MataAtlanticaV2.Application.Produtos;
public interface IProdutoService
{
    Task<List<Produto>> Listar();
}