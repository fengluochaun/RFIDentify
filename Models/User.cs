using Newtonsoft.Json;
using RFIDentify.Com.CustAttributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Models
{
    [Table("User")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }
        public Image? Picture { get; set; }

        // 从DTO模型中获取User模型
        public static User GetFromDto<T>(T dto)
        {
            User user = new();
            Type dtoType = dto!.GetType();
            Type userType = user.GetType();
            foreach (var dtoProp in dtoType.GetProperties())
            {
                var userProp = userType.GetProperty(dtoProp.Name);
                if (userProp != null)
                {
                    userProp.SetValue(user, dtoProp.GetValue(dto));
                }
            }
            return user;
        }
    }
}
