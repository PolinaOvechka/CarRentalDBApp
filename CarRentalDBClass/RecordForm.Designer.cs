namespace CarRentalDBForm
{
    partial class RecordForm
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
            buttonSave = new Button();
            panelFields = new Panel();
            panelHeader.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.RoyalBlue;
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(384, 55);
            panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Inter Extra Bold", 16F, FontStyle.Bold);
            labelTitle.ForeColor = Color.WhiteSmoke;
            labelTitle.Location = new Point(12, 13);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(164, 27);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Новая запись";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(buttonCancel);
            panelButtons.Controls.Add(buttonSave);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(0, 496);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(384, 65);
            panelButtons.TabIndex = 1;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.Transparent;
            buttonCancel.Cursor = Cursors.Hand;
            buttonCancel.FlatAppearance.BorderColor = Color.RoyalBlue;
            buttonCancel.FlatAppearance.BorderSize = 2;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.RoyalBlue;
            buttonCancel.Location = new Point(197, 13);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(175, 40);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.RoyalBlue;
            buttonSave.Cursor = Cursors.Hand;
            buttonSave.FlatAppearance.BorderSize = 0;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonSave.ForeColor = Color.WhiteSmoke;
            buttonSave.Location = new Point(12, 13);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(175, 40);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // panelFields
            // 
            panelFields.AutoScroll = true;
            panelFields.BackColor = Color.Transparent;
            panelFields.Dock = DockStyle.Fill;
            panelFields.Location = new Point(0, 55);
            panelFields.Name = "panelFields";
            panelFields.Size = new Size(384, 441);
            panelFields.TabIndex = 2;
            // 
            // RecordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(384, 561);
            Controls.Add(panelFields);
            Controls.Add(panelButtons);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RecordForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RecordForm";
            Load += RecordForm_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label labelTitle;
        private Panel panelButtons;
        private Button buttonSave;
        private Button buttonCancel;
        private Panel panelFields;
    }
}