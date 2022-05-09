using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public abstract class Model
    {
        public Guid Id { get; private init; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; private init; }
        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; private set; }

        public Model()
        {
            Id = Guid.NewGuid();
            CreatedAt = GetCurrentUtcTime();
            UpdatedAt = GetCurrentUtcTime();
        }

        protected void UpdateModificationDate()
        {
            UpdatedAt = GetCurrentUtcTime();
        }

        private DateTime GetCurrentUtcTime()
        {
            TimeSpan offset = DateTimeOffset.Now.Offset;
            return DateTime.SpecifyKind(DateTime.Now - offset, DateTimeKind.Utc);
        }
    }
}
