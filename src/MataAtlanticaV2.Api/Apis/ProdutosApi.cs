using MataAtlanticaV2.Application.Produtos;
using MataAtlanticaV2.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlanticaV2.Api.Apis;

public static class ProdutosApi
{
    public static WebApplication RegisterProdutosApi(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("api/produtos");

        group.MapGet("/", async ([FromServices] IProdutoService produtoService) =>
        {
            return Results.Ok(await produtoService.Listar());
        })
            .Produces<List<Produto>>((int)HttpStatusCode.OK);

        return webApplication;
    }
}