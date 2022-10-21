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

        /// <summary>
        ///     Gets ImageFolder by folder name
        /// </summary>
        /// <param name="Value">Folder name</param>
        /// <returns>ImageFolder instance if found</returns>
        /// <exception cref="ArgumentOutOfRangeException">When there is no folder with given name</exception>
        public static ImageFolder FindByValue(string Value)
        {
            switch(Value)
            {
                case "": return Root;
                case "Profile": return Profile;
                case "Question": return Question;
                case "Message": return Message;
                default: throw new ArgumentOutOfRangeException("Unknown ImageFolder value"); 
            }
        }
    }
}
