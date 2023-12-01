using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Models.Dto
{
    [Serializable]
    public class UserDisplayDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }

        public static UserDisplayDto GetFromUser(User user) => new UserDisplayDto()
        {
            Id = user.Id,
            Name = user.Name,
            Telephone = user.Telephone,
            Age = user.Age,
            Description = user.Description,
        };
    }
}
