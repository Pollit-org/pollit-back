drop view "Polls.GetPollFeed";
    
create view "Polls.GetPollFeed" as
select
    "Polls"."Id" as "PollId",
    "Polls"."Title",
    string_to_array("Polls"."Tags", ',') as "Tags",
    "Polls"."CreatedAt",
    "Options",
    "TotalVotesCount",
    "Users"."UserName" AS "Author"
from "Polls"
         join (
    select "PollId", array_agg("Title") as "Options", coalesce(sum("OptionVotesCount"), 0) as "TotalVotesCount"
    from "Polls.Options"
             left join (select "PollOptionId", count(*) as "OptionVotesCount" from "Polls.Options.Votes" group by "PollOptionId") POV on "Polls.Options"."Id" = POV."PollOptionId"
    group by "PollId"
) PO on "Polls"."Id" = PO."PollId"
         join "Users" on "AuthorId" = "Users"."Id";