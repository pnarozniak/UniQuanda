﻿namespace UniQuanda.Core.Domain.Entities.App;

public class AppUserEntity
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string Nickname { get; set; }
    public string? LastName { get; set; }
    public string? AboutText { get; set; }
    public string? Avatar { get; set; }
    public string? Banner { get; set; }
    public int? QuestionsAmount { get; set; }
    public int? AnswersAmount { get; set; }
    public int? Points { get; set; }
    public IEnumerable<AcademicTitleEntity>? Titles { get; set; }
    public IEnumerable<UniversityEntity>? Universities { get; set; }
}