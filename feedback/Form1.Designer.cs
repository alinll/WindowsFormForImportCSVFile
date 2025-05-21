namespace feedback
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataGridViewFeedbacks = new System.Windows.Forms.DataGridView();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnImportCsv = new System.Windows.Forms.Button();
            this.DataGriedViewProjects = new System.Windows.Forms.DataGridView();
            this.BtnDeleteNegativeFeedbacks = new System.Windows.Forms.Button();
            this.NameOfProject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AverageRating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewFeedbacks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriedViewProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridViewFeedbacks
            // 
            this.DataGridViewFeedbacks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewFeedbacks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProjectName,
            this.EmployeeName,
            this.Rating,
            this.Comment});
            this.DataGridViewFeedbacks.Location = new System.Drawing.Point(0, 0);
            this.DataGridViewFeedbacks.Name = "DataGridViewFeedbacks";
            this.DataGridViewFeedbacks.RowHeadersWidth = 51;
            this.DataGridViewFeedbacks.RowTemplate.Height = 24;
            this.DataGridViewFeedbacks.Size = new System.Drawing.Size(735, 230);
            this.DataGridViewFeedbacks.TabIndex = 0;
            // 
            // ProjectName
            // 
            this.ProjectName.HeaderText = "Назва проєкту";
            this.ProjectName.MinimumWidth = 6;
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.Width = 125;
            // 
            // EmployeeName
            // 
            this.EmployeeName.HeaderText = "Ім\'я працівника";
            this.EmployeeName.MinimumWidth = 6;
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.Width = 125;
            // 
            // Rating
            // 
            this.Rating.HeaderText = "Оцінка";
            this.Rating.MinimumWidth = 6;
            this.Rating.Name = "Rating";
            this.Rating.Width = 125;
            // 
            // Comment
            // 
            this.Comment.HeaderText = "Коментар";
            this.Comment.MinimumWidth = 6;
            this.Comment.Name = "Comment";
            this.Comment.Width = 125;
            // 
            // BtnImportCsv
            // 
            this.BtnImportCsv.Location = new System.Drawing.Point(1106, 104);
            this.BtnImportCsv.Name = "BtnImportCsv";
            this.BtnImportCsv.Size = new System.Drawing.Size(101, 56);
            this.BtnImportCsv.TabIndex = 1;
            this.BtnImportCsv.Text = "Імпорт CSV";
            this.BtnImportCsv.UseVisualStyleBackColor = true;
            this.BtnImportCsv.Click += new System.EventHandler(this.BtnImportCsv_Click);
            // 
            // DataGriedViewProjects
            // 
            this.DataGriedViewProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGriedViewProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameOfProject,
            this.AverageRating});
            this.DataGriedViewProjects.Location = new System.Drawing.Point(12, 250);
            this.DataGriedViewProjects.Name = "DataGriedViewProjects";
            this.DataGriedViewProjects.RowHeadersWidth = 51;
            this.DataGriedViewProjects.RowTemplate.Height = 24;
            this.DataGriedViewProjects.Size = new System.Drawing.Size(723, 188);
            this.DataGriedViewProjects.TabIndex = 2;
            // 
            // BtnDeleteNegativeFeedbacks
            // 
            this.BtnDeleteNegativeFeedbacks.Location = new System.Drawing.Point(1106, 309);
            this.BtnDeleteNegativeFeedbacks.Name = "BtnDeleteNegativeFeedbacks";
            this.BtnDeleteNegativeFeedbacks.Size = new System.Drawing.Size(101, 56);
            this.BtnDeleteNegativeFeedbacks.TabIndex = 3;
            this.BtnDeleteNegativeFeedbacks.Text = "Видалити негативні відгуки";
            this.BtnDeleteNegativeFeedbacks.UseVisualStyleBackColor = true;
            this.BtnDeleteNegativeFeedbacks.Click += new System.EventHandler(this.BtnDeleteNegativeFeedbacks_Click);
            // 
            // NameOfProject
            // 
            this.NameOfProject.HeaderText = "Назва проєкту";
            this.NameOfProject.MinimumWidth = 6;
            this.NameOfProject.Name = "NameOfProject";
            this.NameOfProject.Width = 125;
            // 
            // AverageRating
            // 
            this.AverageRating.HeaderText = "Середня оцінка проєкту";
            this.AverageRating.MinimumWidth = 6;
            this.AverageRating.Name = "AverageRating";
            this.AverageRating.Width = 125;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 450);
            this.Controls.Add(this.BtnDeleteNegativeFeedbacks);
            this.Controls.Add(this.DataGriedViewProjects);
            this.Controls.Add(this.BtnImportCsv);
            this.Controls.Add(this.DataGridViewFeedbacks);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewFeedbacks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGriedViewProjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridViewFeedbacks;
        private System.Windows.Forms.Button BtnImportCsv;
        private System.Windows.Forms.DataGridView DataGriedViewProjects;
        private System.Windows.Forms.Button BtnDeleteNegativeFeedbacks;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameOfProject;
        private System.Windows.Forms.DataGridViewTextBoxColumn AverageRating;
    }
}

