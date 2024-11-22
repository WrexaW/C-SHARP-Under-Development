using System.ComponentModel.DataAnnotations;

namespace WebApplication_C14.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
