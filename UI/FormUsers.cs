using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using RFIDentify.Models;
using RFIDentify.DAO;
using RFIDentify.Models.Dto;

namespace RFIDentify.UI
{
    public partial class FormUsers : UIPage
    {
        private List<User> users = new();
        private UserDao userDao = new();
        public FormUsers()
        {
            InitializeComponent();
            users = userDao.GetAllUser().Result.ToList();
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            List<UserDisplayDto> us = users.Select(user => UserDisplayDto.GetFromUser(user)).ToList();
            this.DGV_Users.DataSource = us;
        }
    }
}
