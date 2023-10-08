using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;

namespace ExplorerWpfApp
{
    /*
     1. Познакомиться с WPF (+)
     2. Сделать простой аналог проводника с выводом списка файлов. (+)
     3. Сделать вывод папок и файлов с возможностью перехода по папкам. (+)
     4. Сделать выбор пути изначального открытия. (+)
    FileWay = textBlock
    ListFiles = ListBox
     */

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Window();
        }

        // Вывод файлов и папок в listBox
        private void Window()
        {
            ListFiles.Items.Clear(); // чистим экран
            DirectoryInfo dir = new DirectoryInfo(FileWay.Text);

            DirectoryInfo[] dirs = dir.GetDirectories(); // получим все папки 
            foreach (DirectoryInfo i in dirs)
            {
                ListFiles.Items.Add(i);
            }
            FileInfo[] files = dir.GetFiles();// получим все файлы 
            foreach (FileInfo file in files)
            {
                ListFiles.Items.Add(file);
            }
        }

        // двойной клик по элементам и переход по папкам
        private void ListBox1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Path.GetExtension(Path.Combine(FileWay.Text, ListFiles.SelectedItem.ToString())) == "")
            {
                FileWay.Text = Path.Combine(FileWay.Text, ListFiles.SelectedItem.ToString());
                Window(); // если папка - запускаем открытие папок
            }
            else// если файл - открываем
            {
                Process.Start(Path.Combine(FileWay.Text, ListFiles.SelectedItem.ToString()));
            }
        }

        // стрелка назад
        private void ButtonBack_Click(object sender, EventArgs e)
        {

            if (FileWay.Text[FileWay.Text.Length - 1] == '\\')
            {
                FileWay.Text = FileWay.Text.Remove(FileWay.Text.Length - 1, 1);
                while (FileWay.Text[FileWay.Text.Length - 1] != '\\')
                {
                    FileWay.Text = FileWay.Text.Remove(FileWay.Text.Length - 1, 1);
                }
            }
            else if (FileWay.Text[FileWay.Text.Length - 1] != '\\')
            {
                while (FileWay.Text[FileWay.Text.Length - 1] != '\\')
                {
                    FileWay.Text = FileWay.Text.Remove(FileWay.Text.Length - 1, 1);
                }
            }
            Window();
        }

        // кнопка Enter в поиске
        private void FileWay_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(FileWay.Text);
            Window();
        }
    }
}
