namespace WindowsFormsApp1
{
    partial class Reports
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
            this.lstTransactions = new System.Windows.Forms.ListBox();
            this.lstPopularRides = new System.Windows.Forms.ListBox();
            this.lstEmployeeReport = new System.Windows.Forms.ListBox();
            this.btngenReports = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstTransactions
            // 
            this.lstTransactions.FormattingEnabled = true;
            this.lstTransactions.Location = new System.Drawing.Point(12, 12);
            this.lstTransactions.Name = "lstTransactions";
            this.lstTransactions.Size = new System.Drawing.Size(253, 303);
            this.lstTransactions.TabIndex = 0;
            // 
            // lstPopularRides
            // 
            this.lstPopularRides.FormattingEnabled = true;
            this.lstPopularRides.Location = new System.Drawing.Point(271, 12);
            this.lstPopularRides.Name = "lstPopularRides";
            this.lstPopularRides.Size = new System.Drawing.Size(258, 303);
            this.lstPopularRides.TabIndex = 1;
            // 
            // lstEmployeeReport
            // 
            this.lstEmployeeReport.FormattingEnabled = true;
            this.lstEmployeeReport.Location = new System.Drawing.Point(535, 12);
            this.lstEmployeeReport.Name = "lstEmployeeReport";
            this.lstEmployeeReport.Size = new System.Drawing.Size(253, 303);
            this.lstEmployeeReport.TabIndex = 2;
            // 
            // btngenReports
            // 
            this.btngenReports.Location = new System.Drawing.Point(271, 338);
            this.btngenReports.Name = "btngenReports";
            this.btngenReports.Size = new System.Drawing.Size(131, 56);
            this.btngenReports.TabIndex = 3;
            this.btngenReports.Text = "Generate Reports";
            this.btngenReports.UseVisualStyleBackColor = true;
            this.btngenReports.Click += new System.EventHandler(this.btngenReports_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(657, 338);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(131, 56);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btngenReports);
            this.Controls.Add(this.lstEmployeeReport);
            this.Controls.Add(this.lstPopularRides);
            this.Controls.Add(this.lstTransactions);
            this.Name = "Reports";
            this.Text = "Reports";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Reports_FormClosed);
            this.Load += new System.EventHandler(this.Reports_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstTransactions;
        private System.Windows.Forms.ListBox lstPopularRides;
        private System.Windows.Forms.ListBox lstEmployeeReport;
        private System.Windows.Forms.Button btngenReports;
        private System.Windows.Forms.Button btnBack;
    }
}