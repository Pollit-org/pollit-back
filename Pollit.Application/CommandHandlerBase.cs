// using Pollit.Domain.Users;
// using Pollit.SeedWork;
//
// namespace Pollit.Application;
//
// public abstract class OperationHandlerBase
// {
// }
//
// public abstract class OperationHandlerBase<TCommand, TPresenter> : OperationHandlerBase 
//     where TCommand : IOperation
//     where TPresenter : IPresenter
// {
//     public async Task HandleAsync(UserId? userId, TCommand command, TPresenter presenter)
//     {
//         try
//         {
//             if (!await IsAuthorized(userId, command))
//             {
//                 presenter.Forbidden(ApplicationError.ForbiddenAccess);
//
//                 return;
//             }
//
//             await HandleAsync(new AuthorizedOperation<TCommand>(command, userId), presenter);
//         }
//         catch (PollitDomainException e)
//         {
//             throw PollitApplicationException.FromDomainException(e);
//         }
//     }
//
//     protected abstract Task HandleAsync(AuthorizedOperation<TCommand> authorizedCommand, TPresenter presenter);
//     
//     protected abstract Task<bool> IsAuthorized(UserId? userId, TCommand command);
// }