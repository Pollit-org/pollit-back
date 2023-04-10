﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Poll;
using Pollit.Domain.Poll.PollTitles;
using Pollit.Domain.Users;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Polls;

public class PollConfiguration : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.ToTable("Polls");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasDefaultValueSql("gen_random_uuid ()").IsRequired();
        
        builder.Property(p => p.Id).HasConversion(id => id.Value, id => new PollId(id));
        builder.Property(p => p.AuthorId).HasConversion(id => id.Value, id => new UserId(id));

        builder.Property(p => p.Tags).HasHashSetDelimiterSeparatedConversion(",", pollTag => pollTag.ToString(), pollTagString => new PollTag(pollTagString));
        builder.Property(p => p.Title).HasConversion(title => title.ToString(), titleString => new PollTitle(titleString)).HasMaxLength(PollTitle.MaxLength);

        builder.HasMany(p => p.Options).WithOne();
    }
}