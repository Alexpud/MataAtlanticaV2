using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MataAtlanticaV2.Api.Apis;

public static class UsuariosApi
{
    public static WebApplication AddUsuariosApi(this WebApplication app)
    {
        var usuariosGroup = app.MapGroup("/usuarios");

        usuariosGroup
            .MapGet("{id:guid}", (Guid id) =>
            {
                return Results.Ok("asdasd");
            })
            .Produces((int)HttpStatusCode.OK, typeof(string))
            .Produces((int)HttpStatusCode.BadRequest);

        return app;
    }
}