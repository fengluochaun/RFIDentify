using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.sun.xml.@internal.bind.v2.model.core;
using java.security;
using Newtonsoft.Json;
using RFIDentify.DAO;
using RFIDentify.Models;
using RFIDentify.Models.Dto;
using Sunny.UI;

namespace RFIDentify.UI
{
    public partial class FormRegister : UIPage
    {
        private User? user;
        private UserDisplayDto? stashUser;
        private Dictionary<string, string> stashFiles = new Dictionary<string, string>();
        private Image? stashPicture;
        private UserDao userDao = new();
        private int id;
        private string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase!;
        private BindingList<string> userCSVPaths = new BindingList<string>();
        private List<string> stashUserCSVPaths = new List<string>();
        private int collectionSum;
        private bool addOrEdit = false;
        public int CollectionSum
        {
            get => collectionSum;
            set
            {
                collectionSum = value;
                //lbl_CollectionSum.Text = CollectionSum.ToString() + "/50";
            }
        }
        public FormRegister()
        {
            InitializeComponent();
            id = userDao.GetUserNum().Result + 1;
            user = new User() { Id = id };
            stashUser = new UserDisplayDto() { Id = id};
            this.listBox.DataSource = userCSVPaths;
            UpdateText();
        }

        public FormRegister(int userId) 
        {
            user = userDao.GetUserById(userId).Result.FirstOrDefault();
            stashPicture = user!.Picture;
            stashUser = JsonConvert.DeserializeObject<UserDisplayDto>(JsonConvert.SerializeObject(UserDisplayDto.GetFromUser(user))!);
            InitializeComponent();
            this.listBox.DataSource = userCSVPaths;
            this.addOrEdit = true;
            UpdateText();
        }

        private void UpdateText()
        {
            txt_Id.Text = user!.Id.ToString();
            txt_Age.Text = user!.Age.ToString();
            txt_Name.Text = user!.Name!.ToString();
            txt_Telephone.Text = user!.Telephone!.ToString();
            txt_Description.Text = user!.Description!.ToString();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            stashUser = JsonConvert.DeserializeObject<UserDisplayDto>(JsonConvert.SerializeObject(UserDisplayDto.GetFromUser(user))!);
            stashPicture = user!.Picture;
            CopyStashFiles();
        }

        private void btn_FromExplorer_Click(object sender, EventArgs e)
        {
            try
            {            
                using(OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    // 设置对话框的标题和过滤器
                    openFileDialog.Title = "选择用户文件";
                    openFileDialog.Filter = "CSV文件 (*.csv)|*.csv";
                    // 检查用户是否选择了文件
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        CollectionSum++;
                        string selectedFilePath = openFileDialog.FileName;
                        Console.WriteLine("选择的文件路径：" + selectedFilePath);
                        string destinationFilePath = Path.Combine(basePath, "User", $"{user!.Id}");
                        Directory.CreateDirectory(destinationFilePath);
                        destinationFilePath = Path.Combine(destinationFilePath, $"user{user!.Id}--{CollectionSum}.csv");
                        stashFiles.Add(selectedFilePath, destinationFilePath);
                        userCSVPaths.Add(selectedFilePath);
                        this.listBox.Refresh();
                    }
                    else
                    {
                        Console.WriteLine("未选择任何文件.");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btn_FromEquipment_Click(object sender, EventArgs e)
        {

        }

        private void btn_UploadPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    // 设置对话框的标题和过滤器
                    openFileDialog.Title = "选择图片文件";
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                    // 检查用户是否选择了文件
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        CollectionSum++;
                        string selectedFilePath = openFileDialog.FileName;
                        Console.WriteLine("选择的文件路径：" + selectedFilePath);
                        user!.Picture = Image.FromFile(selectedFilePath);
                    }
                    else
                    {
                        Console.WriteLine("未选择任何文件.");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void btn_Commit_Click(object sender, EventArgs e)
        {
            User user = User.GetFromDto<UserDisplayDto>(stashUser!);
            user.Picture = stashPicture;
            if (this.addOrEdit)
            {
                await userDao.UpdateUser(user);
            }
            else
            {
                await userDao.AddUser(user);  
            }
        }

        private void CopyStashFiles()
        {
            //Copy each item in the stashfiles from the source address to the destination address
            foreach (var item in stashFiles)
            {
                string sourceFilePath = item.Key;
                string destinationFilePath = item.Value;
                File.Copy(sourceFilePath, destinationFilePath);
            }
            stashFiles.Clear();
        }
    }
}
