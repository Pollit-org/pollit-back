using Microsoft.AspNetCore.Mvc;

namespace Pollit.Infra.Api;

public class BasePresenter
{
    public IActionResult? ActionResult { get; protected set; }

    protected void OkNoContent()
        => ActionResult = new NoContentResult();
    
    protected void Conflict(string error) 
        => ActionResult = new ConflictObjectResult(new ProblemDetails {Title = "Conflict.", Detail = error});
    
    protected void NotFound(string error) 
        => ActionResult = new NotFoundObjectResult(new ProblemDetails {Title = "Not found.", Detail = error});
    
    protected void Unauthorized(string error) 
        => ActionResult = new UnauthorizedObjectResult(new ProblemDetails {Title = "Unauthorized.", Detail = error});
}