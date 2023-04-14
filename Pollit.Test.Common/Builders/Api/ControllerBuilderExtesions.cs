using Pollit.Infra.Api;
using Pollit.Infra.Api.Controllers;
using Pollit.Test.Common.Builders.Api.Controllers;

namespace Pollit.Test.Common.Builders.Api;

public static class ControllerBuilderExtensions
{
    public static TControllerBuilder WithAuthenticatedUser<TController, TControllerBuilder>(this TControllerBuilder controllerBuilder)
        where TControllerBuilder : ControllerBuilderBase<TController>
        where TController : PollitControllerBase
    {
        return controllerBuilder.WithAuthenticatedUser<TController, TControllerBuilder>(Guid.NewGuid());
    }
    
    public static TControllerBuilder WithAuthenticatedUser<TController, TControllerBuilder>(this TControllerBuilder controllerBuilder, Guid? userId)
        where TControllerBuilder : ControllerBuilderBase<TController>
        where TController : PollitControllerBase
    {
        controllerBuilder.WithAuthenticatedUserProvider(new HardcodedAuthenticatedUserProvider(userId));
        return controllerBuilder;
    }
}