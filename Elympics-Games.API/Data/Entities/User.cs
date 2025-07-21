using System.ComponentModel.DataAnnotations;

namespace Elympics_Games.API.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [RegularExpression(@"^\+?[0-9\s\-()]{7,}$")]
        public string? Phone { get; set; }

        public string? Tin { get; set; }

        public required string Password { get; set; }

        public string? Validator { get; set; } = null;

        public byte[] ProfileImage { get; set; } = Array.Empty<byte>();

        public DateTime? ConfirmedAt { get; set; } = null;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedAt { get; set; } = null;

        public string Role { get; set; } = "User";
    }
}
