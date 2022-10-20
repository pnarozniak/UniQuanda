namespace UniQuanda.Core.Domain.Enums
{
    public class ImageFolder : AbstractAdvancedEnum<string>
    {
        private ImageFolder(string val) : base(val)
        {
        }
        public static ImageFolder Root { get { return new ImageFolder(""); } }
        public static ImageFolder Profile { get { return new ImageFolder("Profile"); } }
        public static ImageFolder Question { get { return new ImageFolder("Question"); } }
        public static ImageFolder Message { get { return new ImageFolder("Message"); } }
    }
}
