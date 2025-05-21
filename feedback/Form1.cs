using feedback.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace feedback
{
    public partial class MainForm : Form
    {
        private readonly List<InternProjectTable> projects = new List<InternProjectTable>();
        private List<InternProjectFeedback> feedbacks = new List<InternProjectFeedback>();
        private int feedbackCounter = 1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnImportCsv_Click(object sender, EventArgs e)
        {
           ReadCSV();
        }

        private void BtnDeleteNegativeFeedbacks_Click(object sender, EventArgs e)
        {
            DeleteNegativeFeedbacks();
        }

        private void ClearFeedbacksGrid()
        {
            try
            {
                DataGridViewFeedbacks.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при очищенні таблиці відгуків:\n{ex.Message}", "Помилка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearProjectsGrid()
        {
            try
            {
                DataGriedViewProjects.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при очищенні таблиці проєктів:\n{ex.Message}", "Помилка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearGrids()
        {
            ClearFeedbacksGrid();
            ClearProjectsGrid();
        }

        private void UpdateFeedbacksGrid()
        {
            try
            {
                foreach (var feedback in feedbacks)
                {
                    var project = projects.FirstOrDefault(p => p.ProjectId == feedback.ProjectId);

                    DataGridViewFeedbacks.Rows.Add(project != null ? project.Name : "Не знайдено назву проєкту",
                        feedback.EmployeeName, feedback.Rating, feedback.Comment);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні таблиці відгуків:\n{ex.Message}", "Помилка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private double CalculateAverageRatings(InternProjectTable project)
        {
            try
            {
                var projectFeedbacks = feedbacks.Where(f => f.ProjectId == project.ProjectId).ToList();

                if (projectFeedbacks.Count != 0)
                {
                    return project.AverageRating = projectFeedbacks.Average(f => f.Rating);
                }

                return project.AverageRating = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при обчисленні середньої оцінки проєкту:\n{ex.Message}", "Помилка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return project.AverageRating = 0;
            }
        }

        private void UpdateProjectsGrid()
        {
            try
            {
                foreach (var project in projects)
                {
                    DataGriedViewProjects.Rows.Add(project.Name, CalculateAverageRatings(project));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні таблиці проєктів:\n{ex.Message}", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateGrids()
        {
            ClearGrids();
            UpdateFeedbacksGrid();
            UpdateProjectsGrid();
        }

        private void ImportCSV(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
                {
                    reader.ReadLine(); // Пропускаємо заголовок

                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length < 4) continue; // Перевірка на повні рядки

                        string projectName = parts[0];
                        string employeeName = parts[1];
                        int rating = int.Parse(parts[2]);
                        string comment = parts[3];

                        var project = projects.FirstOrDefault(p => p.Name == projectName);
                        if (project == null)
                        {
                            project = new InternProjectTable(projects.Count + 1, projectName);
                            projects.Add(project);
                        }

                        var feedback = new InternProjectFeedback(feedbackCounter++, project.ProjectId, employeeName,
                            rating, comment);
                        feedbacks.Add(feedback);
                    }
                }

                UpdateGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при імпорті CSV:\n{ex.Message}", "Помилка", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void ReadCSV()
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "CSV files (*.csv)|*.csv";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        ImportCSV(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при читанні файлу CSV:\n{ex.Message}", "Помилка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DeleteNegativeFeedbacks()
        {
            try
            {
                feedbacks = feedbacks.Where(f => f.Rating >= 3).ToList();
                UpdateGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні негативних відгуків:\n{ex.Message}", "Помилка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
