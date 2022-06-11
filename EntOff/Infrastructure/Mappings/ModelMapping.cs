using EntOff.Api.Models.DTOs.Register;
using EntOff.Api.Models.DTOs.Roles;
using EntOff.Api.Models.DTOs.Tags;
using EntOff.Api.Models.DTOs.Users;
using EntOff.Api.Models.Entities.Roles;
using EntOff.Api.Models.Entities.Tags;
using EntOff.Api.Models.Entities.Users;
using EntOff.Models.DTOs.History;
using EntOff.Models.Entities.History;

namespace EntOff.Api.Infrastructure.Mappings
{
    public static class ModelMapping
    {
        public static User ToEntity(this UserDto userDto)
        {
            return new User
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                InOffice = userDto.InOffice
            };
        }
        
        public static User ToEntity(this RegisterDto registerDto)
        {
            return new User
            {
                UserName = registerDto.UserName.ToLower(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
            };
        }

        public static Role ToEntity(this RoleDto roleDto)
        {
            return new Role
            {
                Name = roleDto.Name
            };
        }

        public static Tag ToEntity(this TagDto tagDto)
        {
            return new Tag
            {
                Code = tagDto.Code,
                ExpiresAt = tagDto.ExpiresAt,
                IsAuthorized = tagDto.IsAuthorized,
                Status = tagDto.Status,
                UserId = tagDto.UserId
            };
        }

        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InOffice = user.InOffice
            };
        }
        public static HistoryDto HisDto(this Histo history)
        {
            return new HistoryDto
            {
                InOut = history.InOut,
                Date = history.Date,
                UserId = history.UserId
               
            };
        }

        public static UserDto ToDto(this User user, string token)
        {
            return new UserDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                InOffice = user.InOffice,
                Token = token
            };
        }

        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto
            {
                Name = role.Name
            };
        }

        public static TagDto ToDto(this Tag tag)
        {
            return new TagDto
            {
                Code = tag.Code,
                ExpiresAt = tag.ExpiresAt,
                IsAuthorized = tag.IsAuthorized,
                Status = tag.Status,
                UserId = tag.UserId
            };
        }
    }
}
