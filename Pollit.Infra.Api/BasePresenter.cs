using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pollit.Application;

namespace Pollit.Infra.Api;

public class BasePresenter : IPresenter
{
    public IActionResult? ActionResult { get; protected set; }

    public void OkNoContent()
        => ActionResult = new NoContentResult();
    
    public void Conflict(string error) 
        => ActionResult = new ConflictObjectResult(new ProblemDetails {Title = "Conflict.", Detail = error});
    
    public void NotFound(string error) 
        => ActionResult = new NotFoundObjectResult(new ProblemDetails {Title = "Not found.", Detail = error});
    
    public void Unauthorized(string error) 
        => ActionResult = new UnauthorizedObjectResult(new ProblemDetails {Title = "Unauthorized.", Detail = error});
    
    public void Forbidden(string error) 
        => ActionResult = new ObjectResult(new ProblemDetails {Status = (int)HttpStatusCode.Forbidden, Title = "Forbidden.", Detail = error});
    
    public void BadRequest(string error) 
        => ActionResult = new BadRequestObjectResult(new ProblemDetails {Title = "Bad request.", Detail = error});
}