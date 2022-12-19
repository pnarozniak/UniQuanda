namespace UniQuanda.Core.Domain.Utils
{
    public class AppRole
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string Premium = "premium";
        public const string TitledUser = "titledUser";
        public const string EduUser = "eduUser";

        private string currentRole = "";
        public string Value { get {
                return currentRole;
            } set
            {
                if (value == Admin || value == User || value == Premium || value == TitledUser || value == EduUser)
                {
                    currentRole = value;
                }
                else
                {
                    throw new ArgumentException("Invalid role");
                }
            }
        }
    }
}
