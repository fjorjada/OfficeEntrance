using EntOff.Api.Models.Entities.Users;
using EntOff.Api.Models.Exceptions.Users;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace EntOff.Api.Services.Foundations.Users
{
    public partial class UserService
    {
        private static void ValidateUserOnCreate(User user, string password)
        {
            ValidateUserIsNull(user);

            Validate(
                (Rule: IsInvalid(user.UserName, nameof(User.UserName)), Parameter: nameof(User.UserName)),
                (Rule: IsInvalidPassword(password), Parameter: nameof(password)),
                (Rule: IsInvalid(user.FirstName, nameof(User.FirstName)), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName, nameof(User.LastName)), Parameter: nameof(User.LastName))
                );
        }

        private static void ValidateUserIsNull(User user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }

        private static void ThrowExceptionIfAnyError(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var invalidUserException = new InvalidUserException();

                foreach (var error in result.Errors)
                {
                    invalidUserException.UpsertDataList(
                        key: error.Code,
                        value: error.Description);
                }

                invalidUserException.ThrowIfContainsErrors();
            }

        }

        private static void ValidateStorageUser(User storageUser, string username)
        {
            if (storageUser == null)
            {
                throw new NotFoundUserException(username);
            }
        }

        private static void ValidateUserOnModify(User user)
        {
            ValidateUserIsNull(user);

            Validate(
                (Rule: IsInvalid(user.Id, nameof(User.Id)), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.UserName, nameof(User.UserName)), Parameter: nameof(User.UserName)),
                (Rule: IsInvalid(user.FirstName, nameof(User.FirstName)), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName, nameof(User.LastName)), Parameter: nameof(User.LastName))
                );
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidUserException = new InvalidUserException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidUserException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidUserException.ThrowIfContainsErrors();
        }

        private static dynamic IsInvalid(string value, string fieldName) => new
        {
            Condition = string.IsNullOrWhiteSpace(value),
            Message = $"{fieldName} is required"
        };

        private static dynamic IsInvalid(Guid value, string fieldName) => new
        {
            Condition = value == Guid.Empty,
            Message = $"{fieldName} is required"
        };

        private static dynamic IsInvalidPassword(string password) => new
        {
            Condition = InvalidPassword(password),
            Message =
                $"Invalid password! " +
                $"Password must be at least 8 characters long " +
                $"and contain at least one number, one lowercase, " +
                $"one uppercase and one special character."
        };

        private static bool InvalidPassword(string password)
        {
            Regex rgx = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");

            if (string.IsNullOrWhiteSpace(password) || !rgx.IsMatch(password))
            {
                return true;
            }

            return false;
        }

    }
}
