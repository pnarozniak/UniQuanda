namespace UniQuanda.Core.Domain.Utils
{
    public class AuthRole
    {
        public const string OAuthAccount = "oauth_account";
        public const string UniquandaAccount = "uniquanda_account";

        private string currentRole = "";
        public string Value
        {
            get
            {
                return currentRole;
            }
            set
            {
                if (value == OAuthAccount || value == UniquandaAccount)
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
