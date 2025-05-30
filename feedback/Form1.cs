using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace feedback
{
    public partial class MainForm : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]
            .ConnectionString;

        public MainForm()
        {
            InitializeComponent();
            UpdateGrids();
        }

        private void BtnImportCsv_Click(object sender, EventArgs e)
        {
           ChooseCSV();
        }

        private void BtnDeleteNegativeFeedbacks_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(inputNegativeRating.Text, out int negativeRating))
                {
                    DeleteNegativeFeedbacks(negativeRating);
                }
                else
                {
                    MessageBox.Show("Введіть правильне ціле число", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні негативних відгуків:\n{ex.Message}", "Помилка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ChooseCSV()
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "CSV files (*.csv)|*.csv";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        ImportCSVToDB(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при читанні файлу CSV:\n{ex.Message}", "Помилка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ImportCSVToDB(string filePath)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var reader = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
                        {
                            reader.ReadLine();

                            string line;

                            while ((line = reader.ReadLine()) != null)
                            {
                                var parts = line.Split(';');
                                if (parts.Length < 4) continue;

                                string projectName = parts[0].Trim();
                                string employeeName = parts[1].Trim();
                                if (!int.TryParse(parts[2], out int rating)) continue;
                                string comment = parts[3].Trim();

                                int projectId;
                                using (var cmd = new SqlCommand("SELECT ProjectId FROM InternProject WHERE Name = @Name", 
                                    connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Name", projectName);
                                    var result = cmd.ExecuteScalar();

                                    if (result != null)
                                    {
                                        projectId = (int)result;
                                    }
                                    else
                                    {
                                        var insertCmd = new SqlCommand("INSERT INTO InternProject (Name) VALUES (@Name); " +
                                            "SELECT SCOPE_IDENTITY();", connection, transaction);
                                        insertCmd.Parameters.AddWithValue("@Name", projectName);
                                        projectId = Convert.ToInt32(insertCmd.ExecuteScalar());
                                    }
                                }

                                using (var cmd = new SqlCommand("INSERT INTO InternProjectFeedback (ProjectId, " +
                                    "EmployeeName, Rating, Comment) VALUES (@ProjectId, @EmployeeName, @Rating, @Comment)", 
                                    connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                                    cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
                                    cmd.Parameters.AddWithValue("@Rating", rating);
                                    cmd.Parameters.AddWithValue("@Comment", comment);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            transaction.Commit();
                        }
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

        private void UpdateGrids()
        {
            ClearGrids();
            UpdateFeedbacksGrid();
            UpdateProjectsGrid();
        }

        private void ClearGrids()
        {
            ClearFeedbacksGrid();
            ClearProjectsGrid();
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

        private void UpdateFeedbacksGrid()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT f.EmployeeName, f.Rating, f.Comment, p.Name AS ProjectName
                    FROM InternProjectFeedback f
                    JOIN InternProject p ON f.ProjectId = p.ProjectId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectName = reader["ProjectName"].ToString();
                            string employeeName = reader["EmployeeName"].ToString();
                            int rating = Convert.ToInt32(reader["Rating"]);
                            string comment = reader["Comment"].ToString();

                            DataGridViewFeedbacks.Rows.Add(projectName, employeeName, rating, comment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні таблиці відгуків:\n{ex.Message}", "Помилка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateProjectsGrid()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT p.Name AS ProjectName,
                    ISNULL(AVG(CAST(f.Rating AS FLOAT)), 0) AS AverageRating
                    FROM InternProject p
                    LEFT JOIN InternProjectFeedback f ON p.ProjectId = f.ProjectId
                    GROUP BY p.Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string projectName = reader["ProjectName"].ToString();
                            double averageRating = Convert.ToDouble(reader["AverageRating"]);

                            DataGriedViewProjects.Rows.Add(projectName, averageRating.ToString("F2"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні таблиці проєктів:\n{ex.Message}", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteNegativeFeedbacks(int negativeRating)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM InternProjectFeedback WHERE Rating <= " +
                        "@NegativeRating", connection))
                    {
                        command.Parameters.AddWithValue("@NegativeRating", negativeRating);
                        command.ExecuteNonQuery();
                    }
                }

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
