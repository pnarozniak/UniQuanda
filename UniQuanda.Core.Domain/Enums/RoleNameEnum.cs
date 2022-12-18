namespace UniQuanda.Core.Domain.Enums
{
    public enum RoleNameEnum
    {
        Admin,
        User,
        Premium,
        TitledUser,
        EduUser
    }

    public static class RoleNameEnumExtensions
    {
        public static string GetName(this RoleNameEnum rolename)
        {
            switch(rolename)
            {
                case RoleNameEnum.Admin:
                    return "admin";
                case RoleNameEnum.User:
                    return "user";
                case RoleNameEnum.Premium:
                    return "premium";
                case RoleNameEnum.TitledUser:
                    return "titledUser";
                case RoleNameEnum.EduUser:
                    return "eduUser";
            }
            return "";
        }
    }
}
