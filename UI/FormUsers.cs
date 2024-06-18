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
        private FormMain _parent;
        public FormUsers(FormMain parent)
        {
            InitializeComponent();
            users = userDao.GetAllUser().Result.ToList();
            _parent = parent;
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            List<UserDisplayDto> us = users.Select(user => UserDisplayDto.GetFromUser(user)).ToList();
            this.DGV_Users.DataSource = us;
        }

        private void DGV_Users_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(this.DGV_Users.Rows[e.RowIndex].Cells[0].Value.ToString()!);
            _parent.formRegister.UpdateByUserId(id);
            _parent.SelectPage(3000);
        }

        private void DGV_Users_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = int.Parse(this.DGV_Users.Rows[e.RowIndex].Cells[0].Value.ToString()!);
            _parent.formRegister.UpdateByUserId(id);
            _parent.SelectPage(3000);
        }
    }
}
