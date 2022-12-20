namespace UniQuanda.Core.Domain.Entities.App;

public class UpdateAnswerLikeValueEntity
{
    public bool? IsUpdateSuccessful { get; set; }
    public int LikeValue { get; set; }
    public int Likes { get; set; }
    public int LikesIncreasedBy { get; set; }
}