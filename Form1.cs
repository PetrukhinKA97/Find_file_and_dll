using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Find_file_and_dll
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Path_find(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = FBD.SelectedPath;
            }
        }

        private async Task Find_buttonAsync(object sender, EventArgs e)
        {
            treeView1.Nodes.Add(textBox1.Text);
            treeView1.Nodes[0].Nodes.Add("Второй");
            treeView1.Nodes[0].Nodes[0].Nodes.Add("3");
            //вызываем рекурсивный метод
            await Find(new DirectoryInfo(textBox1.Text));
        }

        private void Pause_button(object sender, EventArgs e)
        {

        }
        // сохраняем
        private void Save_treeView(object sender, EventArgs e)
        {/*
            //массив временый который и будем сериализовать
            TreeNode[] tempNodes = new TreeNode[treeView1.Nodes.Count];
            // заполняем его
            for (int i = 0; i < treeView1.Nodes.Count; i++)
                tempNodes[i] = treeView1.Nodes[i];
            // сама сериализация
            FileStream fs = new FileStream("TreeSave.xml", FileMode.Create);
            SoapFormatter sf = new SoapFormatter();
            sf.Serialize(fs, tempNodes);
            fs.Close();
            treeView1.Nodes.Clear(); // очищаем для наглядности*/
        }
        // а здесь загружаем
        private void Load_treeView(object sender, EventArgs e)
        {/*
            TreeNode[] tempNodes;
            FileStream fs = new FileStream("TreeSave.xml", FileMode.Open);
            SoapFormatter sf = new SoapFormatter();
            tempNodes = (TreeNode[])sf.Deserialize(fs);
            treeView1.Nodes.AddRange(tempNodes);
            fs.Close();*/
        }

        async static Task Find(DirectoryInfo root)
        {
            FileInfo[] File = null;
            DirectoryInfo[] Dir = null;
            // Получаем все файлы в текущем каталоге
            try
            {
                File = root.GetFiles("*.*");
            }
            finally
            {

            }
            if (File != null)
            {
                //выводим имена файлов в консоль
                foreach (FileInfo fi in File)
                {
                    Console.WriteLine(fi.FullName);
                }
                //получаем все подкаталоги
                Dir = root.GetDirectories();
                //проходим по каждому подкаталогу
                foreach (DirectoryInfo dirInfo in Dir)
                {
                    //РЕКУРСИЯ
                    Find(dirInfo);
                }
            }
        }
    }
}
