using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;
        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> AddQuestionAsync(
            int contentId, int userId, 
            IEnumerable<(int order, int tagId)> tags, 
            string title, string rawText, string text, 
            IEnumerable<string> imageNames,
            DateTime creationTime,
            CancellationToken ct)
        {
            //create transaction 
            await using var ctx = await _context.Database.BeginTransactionAsync(ct);
            try { 
                //create content
                var content = new Content
                {
                    Id = contentId,
                    RawText = rawText,
                    Text = text,
                    ContentType = ContentTypeEnum.Question
                };
                await _context.Contents.AddAsync(content, ct);

                //create images and connect to content
                foreach (var imageName in imageNames)
                {
                    var image = new Image
                    {
                        URL = imageName
                    };
                    await _context.Images.AddAsync(image, ct);
                    var imageInContent = new ImageInContent
                    {
                        ContentIdNavigation = content,
                        ImageIdNavigation = image,
                    };
                    await _context.ImagesInContent.AddAsync(imageInContent, ct);
                }
                // create question
                var question = new Question
                {
                    ContentId = contentId,
                    CreatedAt = creationTime,
                    Header = title,
                
                };
                await _context.Questions.AddAsync(question, ct);

                //create tags in question
                foreach (var tag in tags)
                {
                    var tagInQuestion = new TagInQuestion
                    {
                        TagId = tag.tagId,
                        Order = tag.order,
                        QuestionIdNavigation = question
                    };
                    await _context.TagsInQuestions.AddAsync(tagInQuestion, ct);
                }
                //add creator interaction with question
                var creatorInteraction = new AppUserQuestionInteraction
                {
                    AppUserId = userId,
                    IsCreator = true,
                    IsFollowing = true,
                    IsViewed = true,
                    QuestionIdNavigation = question
                };

                await _context.AppUsersQuestionsInteractions.AddAsync(creatorInteraction, ct);

                var totalCount = tags.Count() + imageNames.Count() * 2 + 3;
                var rowsAdded = await _context.SaveChangesAsync(ct);
                if (rowsAdded != totalCount)
                {
                    await ctx.RollbackAsync(ct);
                    return 0;
                }
                await ctx.CommitAsync(ct);
                return question.Id;
            }
            catch
            {
                await ctx.RollbackAsync(ct);
                return 0;
            }
        }
    }
}
