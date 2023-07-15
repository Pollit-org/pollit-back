using Microsoft.EntityFrameworkCore;
using Pollit.Application.Polls.GetPollResults;

namespace Pollit.Infra.EfCore.NpgSql.Projections.Polls;

public class PollResultsProjection : IPollResultsProjection
{
    private readonly PollitDbContext _context;

    public PollResultsProjection(PollitDbContext context)
    {
        _context = context;
    }
    
    public Task<PollResults> GetPollResultsAsync(GetPollResultsQuery query)
    {
        var dbQuery = _context.PollResultsRawItems.FromSqlRaw(BuildSqlQuery(query));
        var rawItems = dbQuery.ToList();
        var result = new PollResults
        {
            PollId = query.PollId,
            Options = rawItems
                .GroupBy(pollResultRawItem => pollResultRawItem.OptionTitle)
                .Select(pollResultRawItemsGroupedByOption => new PollOptionResults()
                {
                    Title = pollResultRawItemsGroupedByOption.Key,
                    VotesPerFeature = pollResultRawItemsGroupedByOption
                        .GroupBy(pollResultRawItem => pollResultRawItem.Feature)
                        .Select(pollResultRawItemsGroupedByOptionThenByFeature => new PollOptionFeatureResults()
                        {
                            Name = pollResultRawItemsGroupedByOptionThenByFeature.Key,
                            Intervals = pollResultRawItemsGroupedByOptionThenByFeature
                                .Select(pollResultRawItem => new PollOptionFeatureResultsInterval()
                                {
                                    Label = pollResultRawItem.IntervalLabel,
                                    VotesCount = pollResultRawItem.VotesCount
                                }).ToList()
                        }).ToList()
                }).ToList()
        };

        return Task.FromResult(result);
    }

    private string BuildSqlQuery(GetPollResultsQuery query) =>
        $"""
    WITH "Intervals" AS (
    SELECT * FROM (
    VALUES('age', 1, 15, null, null),
        ('age', 15, 18, null, null),
        ('age', 19, 25, null, null),
        ('age', 26, 35, null, null),
        ('age', 36, 45, null, null),
        ('age', 46, 55, null, null),
        ('age', 55, 70, null, null),
        ('age', 70, 85, null, null),
        ('age', 85, 120, null, null))
    AS X("Feature", "AgeFrom", "AgeTo", "Gender", "Country")
    UNION ALL
    SELECT * FROM (
    VALUES('gender', null, null, 0, null),
        ('gender', null, null, 1, null),
        ('gender', null, null, 2, null),
        ('gender', null, null, 3, null))
    AS X("Feature", "AgeFrom", "AgeTo", "Gender", "Country")
)
select
        "Feature",
       "Polls.Options"."Id" AS "OptionId",
       "Polls.Options"."Title" AS "OptionTitle",
       case 
           when "Feature" = 'age' then "Intervals"."AgeFrom" || ' - ' || "Intervals"."AgeTo"
           when 
             "Feature" = 'gender'
           then
             case 
                when "Intervals"."Gender" = 0 then 'Prefer not to say'
                when "Intervals"."Gender" = 1 then 'Male'
                when "Intervals"."Gender" = 2 then 'Female'
                else 'Other'
             end
           when "Feature" = 'country' then "Intervals"."Country"
       END
       AS "IntervalLabel",
       count(
           case
               when "Feature" = 'age'
               then
                   case when extract(year from age("Users"."Birthdate"::date::timestamp::timestamptz))::int between "Intervals"."AgeFrom" AND "Intervals"."AgeTo" then true else null end
               when "Feature" = 'gender'
               then
                   case when "Users"."Gender" = "Intervals"."Gender" then true else null end
               when "Feature" = 'country'
               then null
            end
           ) AS "VotesCount"
from "Polls"
         LEFT join "Polls.Options" on "Polls"."Id" = "Polls.Options"."PollId"
         LEFT join "Polls.Options.Votes" on "Polls.Options"."Id" = "Polls.Options.Votes"."PollOptionId"
         LEFT join "Users" on "Polls.Options.Votes"."VoterId" = "Users"."Id"
        LEFT JOIN "Intervals" on true = true
where "Polls"."Id" = '{query.PollId}'
GROUP BY "Intervals"."AgeFrom", "Intervals"."AgeTo", "Intervals"."Gender", "Intervals"."Country", "OptionId", "OptionTitle", "Feature"
ORDER BY  "Feature", "OptionId", "Intervals"."AgeFrom", "Intervals"."Gender", "Intervals"."Country", "OptionId";
""";

//     private string BuildSqlQueryOld(GetPollResultsQuery query) =>
//         $"""
// WITH "AgeIntervals" AS (
//     {
//         query.IntervalsDistribution switch {
//             GetPollResultsQuery.AgeIntervalsDistribution.EvenDemography => BuildEvenDemographyAgeIntervalsCteQuery(),
//             GetPollResultsQuery.AgeIntervalsDistribution.EvenRanges => BuildEvenRangesAgeIntervalsCteQuery(query.AgeGranularity), 
//             _ => throw new ArgumentOutOfRangeException() 
//         } 
//     }
// )
// SELECT 
//     "Votes"."OptionId" AS "OptionId",
//     "Votes"."OptionTitle" AS "OptionTitle",
//     'age' AS "Feature",
//     "AgeIntervals"."AgeIntervalFrom" || ' - ' || "AgeIntervals"."AgeIntervalTo" AS "IntervalLabel",
//     count(case when "Votes"."Age"::int between "AgeIntervals"."AgeIntervalFrom" AND "AgeIntervals"."AgeIntervalTo" then true else null end) AS "VotesCount" 
// FROM "AgeIntervals"
//          LEFT JOIN (select extract(year from age("Users"."Birthdate"::date::timestamp::timestamptz)) "Age",
//                            "Polls.Options"."Id" "OptionId",
//                            "Polls.Options"."Title" "OptionTitle"
//                     from "Polls"
//                              LEFT join "Polls.Options" on "Polls"."Id" = "Polls.Options"."PollId"
//                              LEFT join "Polls.Options.Votes" on "Polls.Options"."Id" = "Polls.Options.Votes"."PollOptionId"
//                              LEFT join "Users" on "Polls.Options.Votes"."VoterId" = "Users"."Id"
//                     where "Polls"."Id" = '{query.PollId.ToString()}'
// ) "Votes" ON true = true
// GROUP BY "AgeIntervals"."AgeIntervalFrom", "AgeIntervals"."AgeIntervalTo", "OptionId", "Feature"
// ORDER BY "AgeIntervals"."AgeIntervalFrom", "OptionId", "Feature";
// """;
//     
//     private FormattableString BuildEvenRangesAgeIntervalsCteQuery(int ageGranularity) =>
//     $"""
// SELECT 1 + i * {ageGranularity} AS "AgeIntervalFrom", (i + 1) * {ageGranularity} AS "AgeIntervalTo"
// FROM generate_series(0, CEIL(120 / ({ageGranularity}::float) - 1)::int) AS t(i)
// """;
//     
//     private FormattableString BuildEvenDemographyAgeIntervalsCteQuery() =>
//         $"""
// SELECT * FROM (
//    VALUES(1, 15),
//          (15, 18),
//          (19, 25),
//          (26, 35),
//          (36, 45),
//          (46, 55),
//          (55, 70),
//          (70, 85),
//          (85, 120))
//     AS Ranges("AgeIntervalFrom", "AgeIntervalTo")
// """;
}

public class PollResultsRawItem
{
    public Guid OptionId { get; set; }
    
    public string OptionTitle { get; set; }
    
    public string Feature { get; set; }
    
    public string IntervalLabel { get; set; }
    
    public int VotesCount { get; set; }
}