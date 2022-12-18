namespace UniQuanda.Core.Domain.Enums
{
    public class ImageFolder : AbstractAdvancedEnum<string>
    {
        private ImageFolder(string val) : base(val)
        {
        }
        public static ImageFolder Profile { get { return new ImageFolder("Profile"); } }
        public static ImageFolder Tags { get { return new ImageFolder("Tags"); } }
        public static ImageFolder Content { get { return new ImageFolder("Content"); } }
        public static ImageFolder University { get { return new ImageFolder("University"); } }
        public static ImageFolder TitleRequest { get { return new ImageFolder("TitleRequest"); } }

        /// <summary>
        ///     Gets ImageFolder by folder name
        /// </summary>
        /// <param name="Value">Folder name</param>
        /// <returns>ImageFolder instance if found</returns>
        /// <exception cref="ArgumentOutOfRangeException">When there is no folder with given name</exception>
        public static ImageFolder FindByValue(string Value)
        {
            switch (Value)
            {
                case "Profile": return Profile;
                case "Tags": return Tags;
                case "Content": return Content;
                case "University": return University;
                case "TitleRequest": return TitleRequest;
                default: throw new ArgumentOutOfRangeException("Unknown ImageFolder value");
            }
        }
    }
}
