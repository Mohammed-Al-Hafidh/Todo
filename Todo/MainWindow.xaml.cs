using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Todo
{
    public partial class MainWindow : Window
    {
        public string fileName = "..\\..\\mydata.txt";
        List<Task> tasks = new List<Task>();
        public MainWindow()
        {
            InitializeComponent();
            loadFile();
        }
        public void loadFile()
        {

            StreamReader sr = new StreamReader(fileName);
            ;
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] myData = line.Split(';');
                int difficulty;
                if(! int.TryParse(myData[2],out difficulty))
                {
                    MessageBox.Show("Input string error. Go to next line...","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    continue;
                }
                Task task = new Task(myData[0], myData[1], difficulty, myData[3]);
                tasks.Add(task);
                line = sr.ReadLine();
                

            }
            sr.Close();
            lvList.ItemsSource = tasks;
        }
        public void saveFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Task myTaskk in tasks)
                {
                    writer.WriteLine(myTaskk.ToDataString());
                }
            }
        }

        private void lvList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;

            var selectedItem = lvList.SelectedItem;
            if(selectedItem is Task)
            {
                Task task = (Task)lvList.SelectedItem;
                txtTask.Text = task.TaskName;
                slDifficulty.Value = task.Difficulty;                
                dpDueDate.Text = task.DueDate;
                comStatus.Text = task.Status;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((txtTask.Text == ""))
            {
                MessageBox.Show("Input string error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task task = new Task(txtTask.Text, dpDueDate.Text,int.Parse(slDifficulty.Value.ToString()), comStatus.Text);
            tasks.Add(task);
            ResetValue();
        }

        private void ResetValue()
        {
            lvList.Items.Refresh();
            txtTask.Clear();           
            dpDueDate.Text = "";
            slDifficulty.Value = 1;
            comStatus.Text = "";
            lvList.SelectedIndex = -1;
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvList.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select one item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task taskToBeDeleted = (Task)lvList.SelectedItem;
            tasks.Remove(taskToBeDeleted);
            ResetValue();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveFile();
        }

        private void btnExportToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text file (*.txt)|*.txt";
            if (dialog.ShowDialog() == true)
            {
                string allData="";
                foreach (Task myTaskk in tasks)
                {
                    allData += myTaskk.ToString()+"\n";                                       
                }
                File.WriteAllText(dialog.FileName, allData);
            }
            else
            {
                MessageBox.Show("File error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvList.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select one item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if ((txtTask.Text == ""))
            {
                MessageBox.Show("Input string error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task tasktoBeUpdated = (Task)lvList.SelectedItem;
            tasktoBeUpdated.TaskName = txtTask.Text;
            tasktoBeUpdated.Difficulty = int.Parse(slDifficulty.Value.ToString());
            tasktoBeUpdated.DueDate = dpDueDate.Text;
            tasktoBeUpdated.Status = comStatus.Text;
            ResetValue();
        }
    }
}
