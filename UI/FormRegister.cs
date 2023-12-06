﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.sun.org.apache.xml.@internal.dtm.@ref;
using com.sun.xml.@internal.bind.v2.model.core;
using java.security;
using Newtonsoft.Json;
using org.apache.log4j;
using RFIDentify.DAO;
using RFIDentify.Models;
using RFIDentify.Models.Dto;
using Sunny.UI;

namespace RFIDentify.UI
{
    public partial class FormRegister : UIPage
    {
        #region 定义变量
        private UserDao userDao = new();
        private string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase!;
        // 修改数据
        private User? user;
        private BindingList<string> userCSVPaths = new BindingList<string>();
        // 暂存数据，待提交
        private UserDisplayDto? stashUser;
        private Dictionary<string, string> stashFiles = new Dictionary<string, string>();
        private Image? stashPicture;
        
        private int trainDataSum = 0;
        private bool addOrEdit = false;        
        private int collectionSum;
        public int CollectionSum
        {
            get => collectionSum;
            set => collectionSum = value; 
        }
        #endregion
        public FormRegister()
        {
            InitializeComponent();
            var id = userDao.GetUserNum().Result + 1;
            user = new User() { Id = id };
            stashUser = new UserDisplayDto() { Id = id};
            this.listBox.DataSource = userCSVPaths;
            UpdateText();
        }       
        private string demo()
        {
            string str = "P1 & (今天 | 明天) & @综测";
            ExpressionParser parser = new();
            ExpressionNode node = parser.Parse(str);
            return str;
        }

        public FormRegister(int userId) 
        {
            InitializeComponent();
            UpdateByUserId(userId);
            this.listBox.DataSource = userCSVPaths;
        }
        #region 初始化或更新UI
        public void UpdateByUserId(int userId)
        {
            user = userDao.GetUserById(userId).Result.FirstOrDefault();
            stashPicture = user!.Picture;
            stashUser = JsonConvert.DeserializeObject<UserDisplayDto>(JsonConvert.SerializeObject(UserDisplayDto.GetFromUser(user))!);
            this.addOrEdit = true;
            UpdateText();
            InitListBox();
        }
        private void InitListBox()
        {
            userCSVPaths.Clear();
            string path = Path.Combine(basePath, "User", $"{user!.Id}");
            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    Console.WriteLine(file);
                    userCSVPaths.Add(file.Replace(basePath, ""));
                }
            }
            else
            {
                Console.WriteLine("文件夹不存在.");
                Directory.CreateDirectory(path);
            }
            this.listBox.Refresh();
        }

        private void UpdateText()
        {
            txt_Id.Text = user!.Id.ToString();
            txt_Age.Text = user!.Age.ToString();
            txt_Name.Text = user.Name ?? string.Empty;
            txt_Telephone.Text = user!.Telephone ?? string.Empty;
            txt_Description.Text = user!.Description ?? string.Empty;
        }
        #endregion

        #region UI事件
        private void btn_Save_Click(object sender, EventArgs e)
        {
            this.txt_Description.Text = demo();
            //this.stashUser!.Name = txt_Name.Text;
            //this.stashUser!.Age = int.Parse(txt_Age.Text);
            //this.stashUser!.Telephone = txt_Telephone.Text;
            //this.stashUser!.Description = txt_Description.Text;
            //stashPicture = user!.Picture;
            //CopyStashFiles();
            ////GenerateUserTrainCSV();            ;
            //this.roundProcess.Value = CalculateCompletion();
            MessageBox.Show("保存成功！");
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
                        userCSVPaths.Add(destinationFilePath.Replace(basePath, ""));
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
            FormRegisterFromEquipment formRegisterEQ = new(this);
            formRegisterEQ.ShowDialog();
            this.listBox.Refresh();
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
                        MessageBox.Show("上传成功！");
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
        #endregion
        
        private int CalculateCompletion()
        {
            int completion = 0;
            int completionUnit = 10;
            if (string.IsNullOrEmpty(stashUser!.Name))
            {
                completion += completionUnit;
            }
            if (stashUser!.Age != 0)
            {
                completion += completionUnit;
            }
            if (string.IsNullOrEmpty(stashUser!.Telephone))
            {
                completion += completionUnit;
            }
            if (string.IsNullOrEmpty(stashUser!.Description))
            {
                completion += completionUnit;
            }
            if (stashPicture == null)
            {
                completion += completionUnit;
            }
            completion += trainDataSum;
            return completion;
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

        private void GenerateUserTrainCSV()
        {
            // 调用接口，destinationFilePath的数组为参数
            // 返回是否成功和找到的峰值个数, find_peak 的结果放在destinationFilePath的同级目录的Train文件夹中
            // 即User/2/...csv => User/2/Train/...csv
            //trainDataSum = ...;
        }
    }

    public class ExpressionNode
    {
        public string Value { get; set; }
        public List<ExpressionNode> Children { get; set; }

        public ExpressionNode()
        {
            Children = new List<ExpressionNode>();
        }
    }

    public class ExpressionParser
    {
        private static readonly Dictionary<string, int> OperatorPrecedence = new Dictionary<string, int>
        {
            { "(", 0 },
            { ")", 0 },
            { "&", 1 },
            { "|", 2 }
        };

        public ExpressionNode Parse(string expression)
        {
            var expressionStack = new Stack<ExpressionNode>();
            var operatorStack = new Stack<string>();

            foreach (var token in Tokenize(expression))
            {
                if (IsOperand(token))
                {
                    expressionStack.Push(new ExpressionNode { Value = token });
                }
                else if (IsOperator(token))
                {
                    while (operatorStack.Count > 0 && OperatorPrecedence[operatorStack.Peek()] >= OperatorPrecedence[token] && operatorStack.Peek() != "(")
                    {
                        PopOperator(expressionStack, operatorStack);
                    }

                    operatorStack.Push(token);
                }
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        PopOperator(expressionStack, operatorStack);
                    }

                    if (operatorStack.Count == 0 || operatorStack.Peek() != "(")
                    {
                        throw new ArgumentException("Invalid expression: unbalanced parentheses");
                    }

                    operatorStack.Pop();
                }
            }

            while (operatorStack.Count > 0)
            {
                PopOperator(expressionStack, operatorStack);
            }

            if (expressionStack.Count != 1 || operatorStack.Count != 0)
            {
                throw new ArgumentException("Invalid expression");
            }

            return expressionStack.Pop();
        }

        private IEnumerable<string> Tokenize(string expression)
        {
            var tokens = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                yield return token;
            }
        }

        private bool IsOperand(string token)
        {
            return !IsOperator(token) && token != "(" && token != ")";
        }

        private bool IsOperator(string token)
        {
            return OperatorPrecedence.ContainsKey(token);
        }

        private void PopOperator(Stack<ExpressionNode> expressionStack, Stack<string> operatorStack)
        {
            var expressionNode = new ExpressionNode { Value = operatorStack.Pop() };

            for (int i = expressionNode.Value == "&" ? 2 : 1; i > 0; i--)
            {
                if (expressionStack.Count == 0)
                {
                    throw new ArgumentException("Invalid expression");
                }

                expressionNode.Children.Insert(0, expressionStack.Pop());
            }

            expressionStack.Push(expressionNode);
        }
    }
}
