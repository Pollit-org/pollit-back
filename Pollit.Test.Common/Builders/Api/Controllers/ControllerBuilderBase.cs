using Pollit.Infra.Api.AuthenticatedUserProviders;
using Pollit.Infra.Api.Controllers;

namespace Pollit.Test.Common.Builders.Api.Controllers;

public abstract class ControllerBuilderBase<TController> : IFluentBuilder<TController> where TController : PollitControllerBase
{
    protected IAuthenticatedUserProvider _authenticatedUserProvider = new HardcodedAuthenticatedUserProvider();
    
    public ControllerBuilderBase<TController> WithAuthenticatedUserProvider(IAuthenticatedUserProvider authenticatedUserProvider)
    {
        _authenticatedUserProvider = authenticatedUserProvider;
        return this;
    }
    
    public abstract TController Build();
}