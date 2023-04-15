using System.Collections.ObjectModel;
using OneOf;
using OneOf.Types;
using Pollit.Domain.Polls.Errors;
using Pollit.Domain.Polls.PollOptionTitles;
using Pollit.Domain.Polls.PollTitles;
using Pollit.Domain.Users;
using Pollit.SeedWork;

namespace Pollit.Domain.Polls;

[GenerateOneOf]
public partial class PollCreationResult : OneOfBase<Poll, PollMustHaveAtLeastTwoOptionsError> { }

[GenerateOneOf]
public partial class CastVoteResult : OneOfBase<Success, OptionDoesNotExistError, UserHasAlreadyVotedError> { }

public class Poll : EntityBase<PollId>
{
    [Obsolete("For EFCore 💩💩💩💩💩💩")]
    private Poll() { }
    
    public Poll(PollId id, UserId authorId, PollTitle title, IEnumerable<PollOption> options, IEnumerable<PollTag> tags, DateTime createdAt)
    {
        Id = id;
        AuthorId = authorId;
        Title = title;
        _options = options.ToList();
        Tags = new HashSet<PollTag>(tags);
        CreatedAt = createdAt;
    }

    public static PollCreationResult NewPoll(UserId authorId, PollTitle title, IEnumerable<PollOptionTitle> optionTitles, IEnumerable<PollTag> tags)
    {
        var id = PollId.NewPollId();
        var options = optionTitles.Select(optTitle => PollOption.NewPollOption(optTitle)).ToList();

        if (options.Count < 2)
            return new PollMustHaveAtLeastTwoOptionsError();
        
        return new Poll(id, authorId, title, options, new HashSet<PollTag>(tags), DateTime.UtcNow);
    }

    public sealed override PollId Id { get; protected set; }
    
    public UserId AuthorId { get; protected set; }

    public PollTitle Title { get; protected set; }

    private IList<PollOption> _options;
    public IReadOnlyCollection<PollOption> Options => _options.AsReadOnly();
        
    public HashSet<PollTag> Tags { get; protected set; }
    
    public DateTime CreatedAt { get; protected set; }

    public CastVoteResult CastVote(User voter, PollOptionId optionId)
    {
        if (UserHasVoted(voter))
            return new UserHasAlreadyVotedError();
        
        var option = _options.FirstOrDefault(opt => opt.Id == optionId);
        if (option is null)
            return new OptionDoesNotExistError();

        option.AddVote(voter.Id);

        return new Success();
    }

    private bool UserHasVoted(User user)
    {
        return Options.Any(o => o.Votes.Any(v => v.VoterId == user.Id));
    }
}