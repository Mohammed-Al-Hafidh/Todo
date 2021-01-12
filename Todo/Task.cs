namespace Todo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Task
    {
        public string TaskName { get; set; }
        public string DueDate { get; set; }
        public int Difficulty { get; set; }
        public string Status { get; set; }
        public Task(string taskName, string dueDate,int difficulty,string status)
        {
            this.TaskName = taskName;
            this.DueDate = dueDate;
            this.Difficulty = difficulty;
            this.Status = status;
        }
        public override string ToString()
        {
            return string.Format("{0} by {1} / {2}, {3}",this.TaskName,this.DueDate,this.Difficulty,this.Status);
        }
        public string ToDataString()
        {
            return string.Format("{0};{1};{2};{3}", this.TaskName, this.DueDate, this.Difficulty, this.Status);
        }

    }
}
