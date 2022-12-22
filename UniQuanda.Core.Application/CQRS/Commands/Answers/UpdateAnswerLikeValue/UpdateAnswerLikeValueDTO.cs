using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.UpdateAnswerLikeValue;

public class UpdateAnswerLikeValueRequestDTO
{
    [Required]
    public int? IdAnswer { get; set; }

    [Required]
    [Range(-1, 1)]
    public int? LikeValue { get; set; }
}

public class UpdateAnswerLikeValueResponseDTO
{
    public bool? IsUpdateSuccessful { get; set; }
    public int LikeValue { get; set; }
    public int Likes { get; set; }
}