using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Xml.Linq;
namespace ArchiveModel
{
    partial class CreateArchive
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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


        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rlArchiveButton = new Button();
            rlCancelButton = new Button();
            lastArchiveLabel = new Label();
            rlListArchives = new Button();
            rlArchiveDate = new Label();
            rlProjectTitle = new Label();
            SuspendLayout();
            // 
            // rlArchiveButton
            // 
            rlArchiveButton.Location = new System.Drawing.Point(44, 288);
            rlArchiveButton.Name = "rlArchiveButton";
            rlArchiveButton.Size = new Size(165, 46);
            rlArchiveButton.TabIndex = 0;
            rlArchiveButton.Text = "New Archive";
            rlArchiveButton.UseVisualStyleBackColor = true;
            rlArchiveButton.Click += rlArchiveButton_Click;
            // 
            // rlCancelButton
            // 
            rlCancelButton.Location = new System.Drawing.Point(559, 288);
            rlCancelButton.Name = "rlCancelButton";
            rlCancelButton.Size = new Size(150, 46);
            rlCancelButton.TabIndex = 1;
            rlCancelButton.Text = "Cancel";
            rlCancelButton.UseVisualStyleBackColor = true;
            rlCancelButton.Click += rlCancelButton_Click;
            // 
            // lastArchiveLabel
            // 
            lastArchiveLabel.AutoSize = true;
            lastArchiveLabel.Location = new System.Drawing.Point(44, 105);
            lastArchiveLabel.Name = "lastArchiveLabel";
            lastArchiveLabel.Size = new Size(264, 32);
            lastArchiveLabel.TabIndex = 0;
            lastArchiveLabel.Text = "Last archive completed:";
            // 
            // rlListArchives
            // 
            rlListArchives.Location = new System.Drawing.Point(239, 288);
            rlListArchives.Name = "rlListArchives";
            rlListArchives.Size = new Size(174, 46);
            rlListArchives.TabIndex = 3;
            rlListArchives.Text = "List Archives";
            rlListArchives.UseVisualStyleBackColor = true;
            rlListArchives.Click += rlListArchives_Click;
            // 
            // rlArchiveDate
            // 
            rlArchiveDate.AutoSize = true;
            rlArchiveDate.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rlArchiveDate.Location = new System.Drawing.Point(314, 105);
            rlArchiveDate.Name = "rlArchiveDate";
            rlArchiveDate.Size = new Size(0, 32);
            rlArchiveDate.TabIndex = 0;
            // 
            // rlProjectTitle
            // 
            rlProjectTitle.AutoSize = false;
            rlProjectTitle.Dock = DockStyle.Top;
            rlProjectTitle.TextAlign = ContentAlignment.MiddleCenter;
            rlProjectTitle.Height = 32;
            rlProjectTitle.Font = new Font("Segoe UI", 12F);
            rlProjectTitle.Name = "rlProjectTitle";
            rlProjectTitle.TabIndex = 4;
            rlProjectTitle.Text = "rlProjectTitle";
            // 
            // CreateArchive
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(757, 400);
            Controls.Add(rlProjectTitle);
            Controls.Add(rlArchiveDate);
            Controls.Add(rlListArchives);
            Controls.Add(lastArchiveLabel);
            Controls.Add(rlCancelButton);
            Controls.Add(rlArchiveButton);
            Name = "CreateArchive";
            Text = "Create Archive";
            Load += CreateArchive_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button rlArchiveButton;
        private Button rlCancelButton;
        private Label lastArchiveLabel;
        private Button rlListArchives;
        private Label rlArchiveDate;
        private Label rlProjectTitle;
        private string rlParentDirectory;
        private string rlSourceName;
    }
}