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
        public string Name { get; set; }
        public string Telephone { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public Image Picture { get; set; }
    }
}
