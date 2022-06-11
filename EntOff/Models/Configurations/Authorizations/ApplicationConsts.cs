namespace EntOff.Api.Models.Configurations.Authorizations
{
    /// <summary>
    /// constants used throughout the app
    /// </summary>
    public static class ApplicationConsts
    {
        public const string AdminRoleName = "Admin";
        public const string EmployeeRoleName = "Employee";
        public const string GuestRoleName = "Guest";
        public const string TagName = "Tag";
        public const string TagStatusName = "TagStatus";
        public const string TagExpirationName = "TagExpiration";
        public const string TagIsAuthorizedName = "IsAuthorizedTag";

        /// <summary>
        /// name of the policy for Tag authorization
        /// </summary>
        public const string AuthorizedTagPolicy = "AuthorizedTagPolicy";
    }
}
