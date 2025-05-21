namespace feedback.Models
{
    public class InternProjectFeedback
    {
        public int FeedbackId { get; set; }

        public int ProjectId { get; set; }

        public string EmployeeName { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public InternProjectFeedback(int id, int projectId, string name, int rating, string comment)
        {
            FeedbackId = id;
            ProjectId = projectId;
            EmployeeName = name;
            Rating = rating;
            Comment = comment;
        }
    }
}
