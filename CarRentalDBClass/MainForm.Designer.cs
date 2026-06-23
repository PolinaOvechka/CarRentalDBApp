namespace CarRentalDBForm
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
            panelTop = new Panel();
            buttonUserManagement = new Button();
            labelUserInfo = new Label();
            buttonExit = new Button();
            panelMenu = new Panel();
            buttonReset = new Button();
            buttonSearch = new Button();
            comboBoxSearchField = new ComboBox();
            buttonExport = new Button();
            textBoxSearch = new TextBox();
            labelSearch = new Label();
            buttonDelete = new Button();
            buttonEdit = new Button();
            buttonAdd = new Button();
            label1 = new Label();
            labelTable = new Label();
            comboBoxTables = new ComboBox();
            dataGridViewMain = new DataGridView();
            labelTablename = new Label();
            panelTop.SuspendLayout();
            panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.RoyalBlue;
            panelTop.Controls.Add(buttonUserManagement);
            panelTop.Controls.Add(labelUserInfo);
            panelTop.Controls.Add(buttonExit);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1184, 60);
            panelTop.TabIndex = 0;
            // 
            // buttonUserManagement
            // 
            buttonUserManagement.BackColor = Color.Transparent;
            buttonUserManagement.Cursor = Cursors.Hand;
            buttonUserManagement.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            buttonUserManagement.FlatAppearance.BorderSize = 2;
            buttonUserManagement.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonUserManagement.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonUserManagement.FlatStyle = FlatStyle.Flat;
            buttonUserManagement.Font = new Font("Inter", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonUserManagement.ForeColor = Color.Cyan;
            buttonUserManagement.Location = new Point(736, 10);
            buttonUserManagement.Name = "buttonUserManagement";
            buttonUserManagement.Size = new Size(248, 40);
            buttonUserManagement.TabIndex = 10;
            buttonUserManagement.Text = "Управление пользователями";
            buttonUserManagement.UseVisualStyleBackColor = false;
            buttonUserManagement.Click += buttonUserManagement_Click;
            // 
            // labelUserInfo
            // 
            labelUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelUserInfo.AutoSize = true;
            labelUserInfo.BackColor = Color.Transparent;
            labelUserInfo.FlatStyle = FlatStyle.Flat;
            labelUserInfo.Font = new Font("Inter Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelUserInfo.ForeColor = Color.WhiteSmoke;
            labelUserInfo.Location = new Point(22, 17);
            labelUserInfo.Name = "labelUserInfo";
            labelUserInfo.Size = new Size(145, 23);
            labelUserInfo.TabIndex = 1;
            labelUserInfo.Text = "[Имя], [Роль]";
            labelUserInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.FromArgb(128, 255, 255);
            buttonExit.Cursor = Cursors.Hand;
            buttonExit.FlatAppearance.BorderSize = 0;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Inter", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonExit.ForeColor = Color.RoyalBlue;
            buttonExit.Location = new Point(991, 10);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(170, 40);
            buttonExit.TabIndex = 9;
            buttonExit.Text = "Выйти из аккаунта";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.White;
            panelMenu.Controls.Add(buttonReset);
            panelMenu.Controls.Add(buttonSearch);
            panelMenu.Controls.Add(comboBoxSearchField);
            panelMenu.Controls.Add(buttonExport);
            panelMenu.Controls.Add(textBoxSearch);
            panelMenu.Controls.Add(labelSearch);
            panelMenu.Controls.Add(buttonDelete);
            panelMenu.Controls.Add(buttonEdit);
            panelMenu.Controls.Add(buttonAdd);
            panelMenu.Controls.Add(label1);
            panelMenu.Controls.Add(labelTable);
            panelMenu.Controls.Add(comboBoxTables);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 60);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(240, 601);
            panelMenu.TabIndex = 1;
            // 
            // buttonReset
            // 
            buttonReset.BackColor = Color.Transparent;
            buttonReset.Cursor = Cursors.Hand;
            buttonReset.FlatAppearance.BorderColor = Color.Gray;
            buttonReset.FlatAppearance.BorderSize = 2;
            buttonReset.FlatStyle = FlatStyle.Flat;
            buttonReset.Font = new Font("Inter", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonReset.ForeColor = Color.Gray;
            buttonReset.Location = new Point(20, 248);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(194, 40);
            buttonReset.TabIndex = 13;
            buttonReset.Text = "↻ Сбросить";
            buttonReset.UseVisualStyleBackColor = false;
            buttonReset.Click += buttonReset_Click;
            // 
            // buttonSearch
            // 
            buttonSearch.BackColor = Color.Transparent;
            buttonSearch.Cursor = Cursors.Hand;
            buttonSearch.FlatAppearance.BorderColor = Color.Gray;
            buttonSearch.FlatAppearance.BorderSize = 2;
            buttonSearch.FlatStyle = FlatStyle.Flat;
            buttonSearch.Font = new Font("Inter", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonSearch.ForeColor = Color.Gray;
            buttonSearch.Location = new Point(20, 203);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(194, 40);
            buttonSearch.TabIndex = 12;
            buttonSearch.Text = "🔎︎ Найти";
            buttonSearch.UseVisualStyleBackColor = false;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // comboBoxSearchField
            // 
            comboBoxSearchField.BackColor = Color.Gainsboro;
            comboBoxSearchField.Cursor = Cursors.Hand;
            comboBoxSearchField.Font = new Font("Inter", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboBoxSearchField.ForeColor = Color.Gray;
            comboBoxSearchField.FormattingEnabled = true;
            comboBoxSearchField.Location = new Point(21, 132);
            comboBoxSearchField.Name = "comboBoxSearchField";
            comboBoxSearchField.Size = new Size(194, 24);
            comboBoxSearchField.TabIndex = 11;
            comboBoxSearchField.Text = "Поле";
            // 
            // buttonExport
            // 
            buttonExport.BackColor = Color.Transparent;
            buttonExport.Cursor = Cursors.Hand;
            buttonExport.FlatAppearance.BorderColor = Color.DodgerBlue;
            buttonExport.FlatAppearance.BorderSize = 2;
            buttonExport.FlatStyle = FlatStyle.Flat;
            buttonExport.Font = new Font("Inter", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonExport.ForeColor = Color.DodgerBlue;
            buttonExport.Location = new Point(21, 534);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(194, 40);
            buttonExport.TabIndex = 10;
            buttonExport.Text = "Запросы";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.BackColor = Color.Gainsboro;
            textBoxSearch.Cursor = Cursors.IBeam;
            textBoxSearch.Font = new Font("Inter", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxSearch.ForeColor = Color.Gray;
            textBoxSearch.Location = new Point(21, 162);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.ReadOnly = true;
            textBoxSearch.Size = new Size(194, 23);
            textBoxSearch.TabIndex = 8;
            textBoxSearch.Text = "🔎︎ Введите для поиска...";
            // 
            // labelSearch
            // 
            labelSearch.AutoSize = true;
            labelSearch.BackColor = Color.Transparent;
            labelSearch.Font = new Font("Inter Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelSearch.ForeColor = Color.Gray;
            labelSearch.Location = new Point(18, 106);
            labelSearch.Name = "labelSearch";
            labelSearch.Size = new Size(56, 16);
            labelSearch.TabIndex = 7;
            labelSearch.Text = "ПОИСК";
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.Crimson;
            buttonDelete.Cursor = Cursors.Hand;
            buttonDelete.FlatAppearance.BorderColor = Color.Crimson;
            buttonDelete.FlatAppearance.BorderSize = 0;
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.Font = new Font("Inter", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonDelete.ForeColor = Color.White;
            buttonDelete.Location = new Point(21, 444);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(194, 40);
            buttonDelete.TabIndex = 6;
            buttonDelete.Text = "× Удалить";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.BackColor = Color.RoyalBlue;
            buttonEdit.Cursor = Cursors.Hand;
            buttonEdit.FlatAppearance.BorderSize = 0;
            buttonEdit.FlatStyle = FlatStyle.Flat;
            buttonEdit.Font = new Font("Inter", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonEdit.ForeColor = Color.White;
            buttonEdit.Location = new Point(21, 398);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(194, 40);
            buttonEdit.TabIndex = 5;
            buttonEdit.Text = "Редактировать";
            buttonEdit.UseVisualStyleBackColor = false;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.BackColor = Color.MediumSeaGreen;
            buttonAdd.Cursor = Cursors.Hand;
            buttonAdd.FlatAppearance.BorderSize = 0;
            buttonAdd.FlatStyle = FlatStyle.Flat;
            buttonAdd.Font = new Font("Inter", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonAdd.ForeColor = Color.White;
            buttonAdd.Location = new Point(21, 352);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(194, 40);
            buttonAdd.TabIndex = 4;
            buttonAdd.Text = "+ Добавить";
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Inter Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(18, 326);
            label1.Name = "label1";
            label1.Size = new Size(99, 16);
            label1.TabIndex = 3;
            label1.Text = "УПРАВЛЕНИЕ";
            // 
            // labelTable
            // 
            labelTable.AutoSize = true;
            labelTable.BackColor = Color.Transparent;
            labelTable.Font = new Font("Inter Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelTable.ForeColor = Color.Gray;
            labelTable.Location = new Point(18, 25);
            labelTable.Name = "labelTable";
            labelTable.Size = new Size(74, 16);
            labelTable.TabIndex = 2;
            labelTable.Text = "ТАБЛИЦА";
            // 
            // comboBoxTables
            // 
            comboBoxTables.BackColor = Color.White;
            comboBoxTables.Font = new Font("Inter", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            comboBoxTables.FormattingEnabled = true;
            comboBoxTables.Location = new Point(22, 50);
            comboBoxTables.Name = "comboBoxTables";
            comboBoxTables.Size = new Size(194, 24);
            comboBoxTables.TabIndex = 1;
            comboBoxTables.SelectedIndexChanged += comboBoxTables_SelectedIndexChanged;
            // 
            // dataGridViewMain
            // 
            dataGridViewMain.AllowUserToAddRows = false;
            dataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMain.BackgroundColor = Color.White;
            dataGridViewMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMain.Location = new Point(265, 129);
            dataGridViewMain.Name = "dataGridViewMain";
            dataGridViewMain.ReadOnly = true;
            dataGridViewMain.Size = new Size(896, 505);
            dataGridViewMain.TabIndex = 2;
            // 
            // labelTablename
            // 
            labelTablename.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelTablename.AutoSize = true;
            labelTablename.BackColor = Color.Transparent;
            labelTablename.FlatStyle = FlatStyle.Flat;
            labelTablename.Font = new Font("Inter Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelTablename.ForeColor = Color.Gray;
            labelTablename.Location = new Point(257, 85);
            labelTablename.Name = "labelTablename";
            labelTablename.Size = new Size(232, 25);
            labelTablename.TabIndex = 10;
            labelTablename.Text = "[Название таблицы]";
            labelTablename.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1184, 661);
            Controls.Add(labelTablename);
            Controls.Add(dataGridViewMain);
            Controls.Add(panelMenu);
            Controls.Add(panelTop);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Аренда автомобилей";
            Load += MainForm_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelTop;
        private Label labelUserInfo;
        private Panel panelMenu;
        private ComboBox comboBoxTables;
        private Label labelTable;
        private DataGridView dataGridViewMain;
        private Button buttonAdd;
        private Label label1;
        private Button buttonDelete;
        private Button buttonEdit;
        private TextBox textBoxSearch;
        private Label labelSearch;
        private Button buttonExit;
        private Button buttonExport;
        private ComboBox comboBoxSearchField;
        private Label labelTablename;
        private Button buttonReset;
        private Button buttonSearch;
        private Button buttonUserManagement;
    }
}