namespace UniQuanda.Core.Domain.Enums
{
    public class BucketNames : AbstractAdvancedEnum<string>
    {
        private BucketNames(string val) : base(val)
        {
        }
        public static BucketNames Default { get { return new BucketNames("uniquanda-bucket"); } }
    }
}
