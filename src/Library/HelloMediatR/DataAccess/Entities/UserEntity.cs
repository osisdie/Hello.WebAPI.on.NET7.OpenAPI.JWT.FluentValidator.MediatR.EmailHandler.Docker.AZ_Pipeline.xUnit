using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hello.MediatR.Domain.DataAccess.Entities
{
    public class UserEntity
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string Salt { get; set; }

        [JsonIgnore]
        public string HashedPassword { get; set; }
    }
}
