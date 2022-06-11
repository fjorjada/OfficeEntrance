using EntOff.Api.Models.DTOs.Login;
using EntOff.Api.Models.DTOs.Register;
using EntOff.Api.Models.Entities.Tags;
using EntOff.Api.Models.Entities.Users;
using EntOff.Api.Models.Exceptions.Tags;
using EntOff.Api.Models.Exceptions.Users;
using System.Text.RegularExpressions;

namespace EntOff.Api.Services.Processings.Accounts
{
    public partial class AccountService
    {
        private static void ValidateExistsStorageUser(User storageUser, string username)
        {
            if (storageUser is not null)
            {
                throw new AlreadyExistsUsernameException(username);
            }
        }

        private static void ValidateStorageUserName(User storageUser, string username)
        {
            if (storageUser == null)
            {
                throw new NotFoundUsernameException(username);
            }
        }

        private static void ValidateStorageTag(Tag storageTag, Guid id)
        {
            if (storageTag is null)
            {
                throw new NotFoundTagException(id);
            }
        }

        private static void ValidateRegisterDto(RegisterDto registerDto)
        {
            if (registerDto is null)
            {
                throw new NullUserException();
            }

            Validate(
               (Rule: IsInvalid(registerDto.UserName, nameof(RegisterDto.UserName)), Parameter: nameof(RegisterDto.UserName)),
               (Rule: IsInvalidPassword(registerDto.Password), Parameter: nameof(RegisterDto.Password)),
               (Rule: IsInvalid(registerDto.FirstName, nameof(RegisterDto.FirstName)), Parameter: nameof(RegisterDto.FirstName)),
               (Rule: IsInvalid(registerDto.LastName, nameof(RegisterDto.LastName)), Parameter: nameof(RegisterDto.LastName))
               );
        }

        private static void ValidateLoginDto(LoginDto loginDto)
        {
            if (loginDto is null)
            {
                throw new NullUserException();
            }

            Validate(
               (Rule: IsInvalid(loginDto.UserName, nameof(LoginDto.UserName)), Parameter: nameof(LoginDto.UserName)),
               (Rule: IsInvalid(loginDto.Password, nameof(LoginDto.Password)), Parameter: nameof(LoginDto.Password))

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
