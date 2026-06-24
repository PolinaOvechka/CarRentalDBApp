namespace CarRentalDBForm
{
    partial class QueryForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panelHeader = new Panel();
            buttonCustomQuery = new Button();
            labelTitle = new Label();
            panelQuerySelector = new Panel();
            comboBoxQueries = new ComboBox();
            label1 = new Label();
            panelButtons = new Panel();
            buttonExit = new Button();
            buttonPrint = new Button();
            buttonExecute = new Button();
            dataGridViewResults = new DataGridView();
            panelHeader.SuspendLayout();
            panelQuerySelector.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.RoyalBlue;
            panelHeader.Controls.Add(buttonCustomQuery);
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1353, 76);
            panelHeader.TabIndex = 0;
            // 
            // buttonCustomQuery
            // 
            buttonCustomQuery.BackColor = Color.Transparent;
            buttonCustomQuery.Cursor = Cursors.Hand;
            buttonCustomQuery.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            buttonCustomQuery.FlatAppearance.BorderSize = 2;
            buttonCustomQuery.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonCustomQuery.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonCustomQuery.FlatStyle = FlatStyle.Flat;
            buttonCustomQuery.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonCustomQuery.ForeColor = Color.Cyan;
            buttonCustomQuery.Location = new Point(1118, 13);
            buttonCustomQuery.Margin = new Padding(3, 4, 3, 4);
            buttonCustomQuery.Name = "buttonCustomQuery";
            buttonCustomQuery.Size = new Size(222, 51);
            buttonCustomQuery.TabIndex = 11;
            buttonCustomQuery.Text = "Создать свой запрос";
            buttonCustomQuery.UseVisualStyleBackColor = false;
            buttonCustomQuery.Click += buttonCustomQuery_Click;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            labelTitle.Location = new Point(25, 22);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(108, 26);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Запросы";
            // 
            // panelQuerySelector
            // 
            panelQuerySelector.Controls.Add(comboBoxQueries);
            panelQuerySelector.Controls.Add(label1);
            panelQuerySelector.Dock = DockStyle.Top;
            panelQuerySelector.Location = new Point(0, 76);
            panelQuerySelector.Margin = new Padding(3, 4, 3, 4);
            panelQuerySelector.Name = "panelQuerySelector";
            panelQuerySelector.Size = new Size(1353, 108);
            panelQuerySelector.TabIndex = 1;
            // 
            // comboBoxQueries
            // 
            comboBoxQueries.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxQueries.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboBoxQueries.FormattingEnabled = true;
            comboBoxQueries.Location = new Point(25, 48);
            comboBoxQueries.Margin = new Padding(3, 4, 3, 4);
            comboBoxQueries.Name = "comboBoxQueries";
            comboBoxQueries.Size = new Size(1314, 26);
            comboBoxQueries.TabIndex = 1;
            comboBoxQueries.SelectedIndexChanged += comboBoxQueries_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(204, 18);
            label1.TabIndex = 0;
            label1.Text = "ДОСТУПНЫЕ ЗАПРОСЫ";
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(buttonExit);
            panelButtons.Controls.Add(buttonPrint);
            panelButtons.Controls.Add(buttonExecute);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(0, 755);
            panelButtons.Margin = new Padding(3, 4, 3, 4);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1353, 82);
            panelButtons.TabIndex = 2;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.Transparent;
            buttonExit.Cursor = Cursors.Hand;
            buttonExit.FlatAppearance.BorderColor = Color.Crimson;
            buttonExit.FlatAppearance.BorderSize = 2;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            buttonExit.ForeColor = Color.Crimson;
            buttonExit.Location = new Point(1073, 18);
            buttonExit.Margin = new Padding(3, 4, 3, 4);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(266, 51);
            buttonExit.TabIndex = 3;
            buttonExit.Text = "Отмена";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // buttonPrint
            // 
            buttonPrint.BackColor = Color.Transparent;
            buttonPrint.Cursor = Cursors.Hand;
            buttonPrint.FlatAppearance.BorderColor = Color.RoyalBlue;
            buttonPrint.FlatAppearance.BorderSize = 2;
            buttonPrint.FlatStyle = FlatStyle.Flat;
            buttonPrint.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            buttonPrint.ForeColor = Color.RoyalBlue;
            buttonPrint.Location = new Point(298, 18);
            buttonPrint.Margin = new Padding(3, 4, 3, 4);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(266, 51);
            buttonPrint.TabIndex = 2;
            buttonPrint.Text = "🖨️ Печатать";
            buttonPrint.UseVisualStyleBackColor = false;
            buttonPrint.Click += buttonPrint_Click;
            // 
            // buttonExecute
            // 
            buttonExecute.BackColor = Color.RoyalBlue;
            buttonExecute.Cursor = Cursors.Hand;
            buttonExecute.FlatAppearance.BorderSize = 0;
            buttonExecute.FlatStyle = FlatStyle.Flat;
            buttonExecute.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            buttonExecute.ForeColor = Color.WhiteSmoke;
            buttonExecute.Location = new Point(25, 18);
            buttonExecute.Margin = new Padding(3, 4, 3, 4);
            buttonExecute.Name = "buttonExecute";
            buttonExecute.Size = new Size(266, 51);
            buttonExecute.TabIndex = 1;
            buttonExecute.Text = "Выполнить";
            buttonExecute.UseVisualStyleBackColor = false;
            buttonExecute.Click += buttonExecute_Click;
            // 
            // dataGridViewResults
            // 
            dataGridViewResults.AllowUserToAddRows = false;
            dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewResults.BackgroundColor = Color.White;
            dataGridViewResults.BorderStyle = BorderStyle.None;
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResults.Dock = DockStyle.Fill;
            dataGridViewResults.Location = new Point(0, 184);
            dataGridViewResults.Margin = new Padding(3, 4, 3, 4);
            dataGridViewResults.Name = "dataGridViewResults";
            dataGridViewResults.ReadOnly = true;
            dataGridViewResults.RowHeadersWidth = 47;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewResults.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResults.Size = new Size(1353, 571);
            dataGridViewResults.TabIndex = 3;
            // 
            // QueryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1353, 837);
            Controls.Add(dataGridViewResults);
            Controls.Add(panelButtons);
            Controls.Add(panelQuerySelector);
            Controls.Add(panelHeader);
            ForeColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "QueryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Запросы";
            Load += QueryForm_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelQuerySelector.ResumeLayout(false);
            panelQuerySelector.PerformLayout();
            panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label labelTitle;
        private Panel panelQuerySelector;
        private Label label1;
        private ComboBox comboBoxQueries;
        private Panel panelButtons;
        private Button buttonPrint;
        private Button buttonExecute;
        private DataGridView dataGridViewResults;
        private Button buttonCustomQuery;
        private Button buttonExit;
    }
}