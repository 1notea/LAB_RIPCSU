namespace Zakharov.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public string StudentFullName { get; set; }
        public string SubjectName { get; set; }
        public string Comment { get; set; }
        public int Grade { get; set; }
        public DateTime Date { get; set; }
    }

}
