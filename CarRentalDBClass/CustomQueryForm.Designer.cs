namespace CarRentalDBForm
{
    partial class CustomQueryForm
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
            panelHeader = new Panel();
            labelTitle = new Label();
            panelButtons = new Panel();
            buttonCancel = new Button();
            buttonExecute = new Button();
            panelSql = new Panel();
            textBoxQuery = new TextBox();
            panelHeader.SuspendLayout();
            panelButtons.SuspendLayout();
            panelSql.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.RoyalBlue;
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(522, 60);
            panelHeader.TabIndex = 1;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Inter Medium", 14.25F, FontStyle.Bold);
            labelTitle.ForeColor = Color.WhiteSmoke;
            labelTitle.Location = new Point(22, 17);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(228, 23);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Создать свой запрос";
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(buttonCancel);
            panelButtons.Controls.Add(buttonExecute);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(0, 396);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(522, 65);
            panelButtons.TabIndex = 3;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.Transparent;
            buttonCancel.Cursor = Cursors.Hand;
            buttonCancel.FlatAppearance.BorderColor = Color.Crimson;
            buttonCancel.FlatAppearance.BorderSize = 2;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.Crimson;
            buttonCancel.Location = new Point(265, 13);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(233, 40);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonExecute
            // 
            buttonExecute.BackColor = Color.RoyalBlue;
            buttonExecute.Cursor = Cursors.Hand;
            buttonExecute.FlatAppearance.BorderSize = 0;
            buttonExecute.FlatStyle = FlatStyle.Flat;
            buttonExecute.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonExecute.ForeColor = Color.WhiteSmoke;
            buttonExecute.Location = new Point(22, 13);
            buttonExecute.Name = "buttonExecute";
            buttonExecute.Size = new Size(233, 40);
            buttonExecute.TabIndex = 1;
            buttonExecute.Text = "Выполнить";
            buttonExecute.UseVisualStyleBackColor = false;
            buttonExecute.Click += buttonExecute_Click;
            // 
            // panelSql
            // 
            panelSql.Controls.Add(textBoxQuery);
            panelSql.Dock = DockStyle.Fill;
            panelSql.Location = new Point(0, 60);
            panelSql.Name = "panelSql";
            panelSql.Size = new Size(522, 336);
            panelSql.TabIndex = 4;
            // 
            // textBoxQuery
            // 
            textBoxQuery.BorderStyle = BorderStyle.FixedSingle;
            textBoxQuery.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxQuery.Location = new Point(22, 22);
            textBoxQuery.Multiline = true;
            textBoxQuery.Name = "textBoxQuery";
            textBoxQuery.Size = new Size(476, 293);
            textBoxQuery.TabIndex = 0;
            // 
            // CustomQuerryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(522, 461);
            Controls.Add(panelSql);
            Controls.Add(panelButtons);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomQuerryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "CustomQuerryForm";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelButtons.ResumeLayout(false);
            panelSql.ResumeLayout(false);
            panelSql.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label labelTitle;
        private Panel panelButtons;
        private Button buttonCancel;
        private Button buttonExecute;
        private Panel panelSql;
        private TextBox textBoxQuery;
    }
}