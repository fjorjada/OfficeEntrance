using EntOff.Api.Models.DTOs.Roles;
using EntOff.Api.Models.Exceptions.Roles;
using Microsoft.AspNetCore.Identity;

namespace EntOff.Api.Services.Foundations.Roles
{
    public partial class RoleService
    {
        public static void ValidateRoleOnCreate(RoleDto role)
        {
            if (role == null)
            {
                throw new NullRoleException();
            }

            if (string.IsNullOrWhiteSpace(role.Name))
            {
                throw new InvalidRoleException(nameof(role.Name), role.Name);
            }
        }

        private static void ThrowExceptionIfAnyError(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var invalidUserException = new InvalidRoleException();

                foreach (var error in result.Errors)
                {
                    invalidUserException.UpsertDataList(
                        key: error.Code,
                        value: error.Description);
                }

                invalidUserException.ThrowIfContainsErrors();
            }

        }
    }
}
