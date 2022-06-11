using EntOff.Api.Models.Entities.Tags;
using EntOff.Api.Models.Exceptions.Tags;

namespace EntOff.Api.Services.Foundations.Tags
{
    public partial class TagService
    {
        private static void ValidateTagOnCreate(Tag tag)
        {
            ValidateTagIsNull(tag);

            Validate(
                (Rule: IsInvalid(tag.UserId, nameof(Tag.UserId)), Parameter: nameof(Tag.UserId)),
                (Rule: IsInvalid(tag.Code, nameof(Tag.Code)), Parameter: nameof(Tag.Code)),
                (Rule: IsInvalid(tag.Status.ToString(), nameof(Tag.Status)), Parameter: nameof(Tag.Status)),
                (Rule: IsInvalid(tag.ExpiresAt, nameof(Tag.ExpiresAt)), Parameter: nameof(Tag.ExpiresAt))
                );
        }

        private static void ValidateTagOnModify(Tag tag)
        {
            ValidateTagIsNull(tag);

            Validate(
                (Rule: IsInvalid(tag.Id, nameof(Tag.Id)), Parameter: nameof(Tag.Id)),
                (Rule: IsInvalid(tag.UserId, nameof(Tag.UserId)), Parameter: nameof(Tag.UserId)),
                (Rule: IsInvalid(tag.Code, nameof(Tag.Code)), Parameter: nameof(Tag.Code)),
                (Rule: IsInvalid(tag.Status.ToString(), nameof(Tag.Status)), Parameter: nameof(Tag.Status)),
                (Rule: IsInvalid(tag.ExpiresAt, nameof(Tag.ExpiresAt)), Parameter: nameof(Tag.ExpiresAt))
                );
        }

        private static void ValidateTagIsNull(Tag tag)
        {
            if (tag is null)
            {
                throw new NullTagException();
            }
        }

        private static void ValidateStorageTag(Tag storageTag, Guid id)
        {
            if (storageTag == null)
            {
                throw new NotFoundTagException(id);
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTagException = new InvalidTagException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTagException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidTagException.ThrowIfContainsErrors();
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

        private static dynamic IsInvalid(DateTimeOffset value, string fieldName) => new
        {
            Condition = value == default,
            Message = $"{fieldName} is required"
        };

    }
}
