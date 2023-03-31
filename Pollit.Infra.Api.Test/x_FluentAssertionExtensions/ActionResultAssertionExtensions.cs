using FluentAssertions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Pollit.Infra.Api.Test.x_FluentAssertionExtensions;

public static class ActionResultAssertionExtensions
{
    public static ObjectResultAssertions WithError(this ObjectResultAssertions objectResultAssertions, string? error = null)
    {
        return objectResultAssertions.WithValueMatch<ProblemDetails>(value => error == null || value.Detail == error);
    } 
}