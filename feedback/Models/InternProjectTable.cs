namespace feedback.Models
{
    public class InternProjectTable
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public double AverageRating { get; set; }

        public InternProjectTable(int id, string name)
        {
            ProjectId = id;
            Name = name;
            AverageRating = 0;
        }
    }
}
