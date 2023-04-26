﻿using Octokit;
using Language = MetricHunter.Core.Languages.Language;

namespace MetricHunter.Application.Git;

public class GitInput
{
    public Language? Language { get; set; }
    public string? Topic { get; set; }
    public int Count { get; set; } = 100;
    public SortDirection Order { get; set; }
}