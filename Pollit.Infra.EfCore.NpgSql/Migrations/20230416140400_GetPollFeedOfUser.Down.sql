drop function "Polls.GetPollFeedOfUser"(userId UUID);

drop table "Polls.Projections.GetPollFeedItems";

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
    select
        "PollId",
        json_agg(
                to_json(
                        json_build_object('Id', "Id", 'Title', "Title", 'VotesCount', coalesce("VotesCount", 0))
                    )
            ) as "Options",
        coalesce(sum("VotesCount"), 0) as "TotalVotesCount"
    from "Polls.Options"
             left join (
        select "PollOptionId", count(*) as "VotesCount" from "Polls.Options.Votes" group by "PollOptionId"
    ) POV on "Id" = POV."PollOptionId"
    group by "PollId"
) PO on "Polls"."Id" = PO."PollId"
         join "Users" on "AuthorId" = "Users"."Id";