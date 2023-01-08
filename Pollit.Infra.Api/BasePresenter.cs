using Microsoft.AspNetCore.Mvc;

namespace Pollit.Infra.Api;

public class BasePresenter
{
    public IActionResult? ActionResult { get; protected set; }
    
    protected void Conflict(string message) 
        => ActionResult = new ConflictObjectResult(new ProblemDetails {Title = "Conflict.", Detail = message});
}