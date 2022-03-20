using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private void Find_buttonAsync(object sender, EventArgs e)
        {

            var root = new DirectoryInfo(textBox1.Text);
            treeView1.Nodes.Add(Find_file(root));

            /*
            if (radioButton1.Checked == true)
            {
                Find_dll(new DirectoryInfo(textBox1.Text));
            }
            else
            {
                Find_file(new DirectoryInfo(textBox1.Text));
            }
            */

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

        static void Find_dll(DirectoryInfo root)
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
                    Find_dll(dirInfo);
                }
            }
        }



        private static TreeNode Find_file(DirectoryInfo root)
        {
            var node=new TreeNode(root.Name);
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            
            // Получаем все файлы в текущем каталоге
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }


            try
            {
                subDirs = root.GetDirectories();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }


            if(subDirs != null)
            {
                foreach (var dirInfo in subDirs)
                {
                    node.Nodes.Add(Find_file(dirInfo));
                }
            }
            
            if(files != null)
            {
                foreach (var Fi in files)
                {
                    node.Nodes.Add(new TreeNode(Fi.Name));
                }
            }
            return node;
        }  
    }
}
