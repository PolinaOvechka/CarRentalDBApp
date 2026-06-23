namespace CarRentalDBForm
{
    partial class UserManagementForm
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
            dataGridViewUsers = new DataGridView();
            labelUsers = new Label();
            label1 = new Label();
            textBoxUsername = new TextBox();
            textBoxPassword = new TextBox();
            label2 = new Label();
            label3 = new Label();
            comboBoxRole = new ComboBox();
            buttonEdit = new Button();
            buttonDeleteUser = new Button();
            buttonApply = new Button();
            buttonCancel = new Button();
            buttonAddUser = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewUsers
            // 
            dataGridViewUsers.BackgroundColor = Color.White;
            dataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsers.Location = new Point(12, 12);
            dataGridViewUsers.Name = "dataGridViewUsers";
            dataGridViewUsers.ReadOnly = true;
            dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewUsers.Size = new Size(360, 142);
            dataGridViewUsers.TabIndex = 0;
            dataGridViewUsers.CellClick += dataGridViewUsers_CellClick;
            // 
            // labelUsers
            // 
            labelUsers.AutoSize = true;
            labelUsers.Font = new Font("Inter Black", 14F, FontStyle.Bold);
            labelUsers.ForeColor = Color.RoyalBlue;
            labelUsers.Location = new Point(9, 173);
            labelUsers.Name = "labelUsers";
            labelUsers.Size = new Size(267, 23);
            labelUsers.TabIndex = 1;
            labelUsers.Text = "ДАННЫЕ ПОЛЬЗОВАТЕЛЯ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Inter Black", 9.75F, FontStyle.Bold);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(9, 211);
            label1.Name = "label1";
            label1.Size = new Size(54, 16);
            label1.TabIndex = 2;
            label1.Text = "ЛОГИН";
            // 
            // textBoxUsername
            // 
            textBoxUsername.BackColor = Color.Gainsboro;
            textBoxUsername.Font = new Font("Inter", 9.75F);
            textBoxUsername.ForeColor = Color.Black;
            textBoxUsername.Location = new Point(12, 232);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.ReadOnly = true;
            textBoxUsername.Size = new Size(360, 23);
            textBoxUsername.TabIndex = 3;
            // 
            // textBoxPassword
            // 
            textBoxPassword.BackColor = Color.Gainsboro;
            textBoxPassword.Font = new Font("Inter", 9.75F);
            textBoxPassword.ForeColor = Color.Black;
            textBoxPassword.Location = new Point(12, 288);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.ReadOnly = true;
            textBoxPassword.Size = new Size(360, 23);
            textBoxPassword.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Inter Black", 9.75F, FontStyle.Bold);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(9, 267);
            label2.Name = "label2";
            label2.Size = new Size(65, 16);
            label2.TabIndex = 4;
            label2.Text = "ПАРОЛЬ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Inter Black", 9.75F, FontStyle.Bold);
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(9, 325);
            label3.Name = "label3";
            label3.Size = new Size(45, 16);
            label3.TabIndex = 6;
            label3.Text = "РОЛЬ";
            // 
            // comboBoxRole
            // 
            comboBoxRole.BackColor = Color.White;
            comboBoxRole.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRole.Enabled = false;
            comboBoxRole.Font = new Font("Inter", 9.75F);
            comboBoxRole.ForeColor = Color.Black;
            comboBoxRole.FormattingEnabled = true;
            comboBoxRole.Location = new Point(12, 346);
            comboBoxRole.Name = "comboBoxRole";
            comboBoxRole.Size = new Size(360, 24);
            comboBoxRole.TabIndex = 8;
            // 
            // buttonEdit
            // 
            buttonEdit.BackColor = Color.MidnightBlue;
            buttonEdit.Cursor = Cursors.Hand;
            buttonEdit.FlatAppearance.BorderSize = 0;
            buttonEdit.FlatStyle = FlatStyle.Flat;
            buttonEdit.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonEdit.ForeColor = Color.WhiteSmoke;
            buttonEdit.Location = new Point(12, 459);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(360, 40);
            buttonEdit.TabIndex = 9;
            buttonEdit.Text = "Редактировать";
            buttonEdit.UseVisualStyleBackColor = false;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDeleteUser
            // 
            buttonDeleteUser.BackColor = Color.Transparent;
            buttonDeleteUser.Cursor = Cursors.Hand;
            buttonDeleteUser.FlatAppearance.BorderColor = Color.Crimson;
            buttonDeleteUser.FlatAppearance.BorderSize = 2;
            buttonDeleteUser.FlatStyle = FlatStyle.Flat;
            buttonDeleteUser.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonDeleteUser.ForeColor = Color.Crimson;
            buttonDeleteUser.Location = new Point(12, 505);
            buttonDeleteUser.Name = "buttonDeleteUser";
            buttonDeleteUser.Size = new Size(360, 40);
            buttonDeleteUser.TabIndex = 10;
            buttonDeleteUser.Text = "Удалить";
            buttonDeleteUser.UseVisualStyleBackColor = false;
            buttonDeleteUser.Click += buttonDeleteUser_Click;
            // 
            // buttonApply
            // 
            buttonApply.BackColor = Color.MediumSeaGreen;
            buttonApply.Cursor = Cursors.Hand;
            buttonApply.FlatAppearance.BorderSize = 0;
            buttonApply.FlatStyle = FlatStyle.Flat;
            buttonApply.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonApply.ForeColor = Color.WhiteSmoke;
            buttonApply.Location = new Point(12, 459);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(360, 40);
            buttonApply.TabIndex = 11;
            buttonApply.Text = "Применить";
            buttonApply.UseVisualStyleBackColor = false;
            buttonApply.Visible = false;
            buttonApply.Click += buttonApply_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.DarkGreen;
            buttonCancel.Cursor = Cursors.Hand;
            buttonCancel.FlatAppearance.BorderSize = 0;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.WhiteSmoke;
            buttonCancel.Location = new Point(12, 505);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(360, 40);
            buttonCancel.TabIndex = 12;
            buttonCancel.Text = "Отмена";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Visible = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonAddUser
            // 
            buttonAddUser.BackColor = Color.RoyalBlue;
            buttonAddUser.Cursor = Cursors.Hand;
            buttonAddUser.FlatAppearance.BorderSize = 0;
            buttonAddUser.FlatStyle = FlatStyle.Flat;
            buttonAddUser.Font = new Font("Inter", 9.75F, FontStyle.Bold);
            buttonAddUser.ForeColor = Color.WhiteSmoke;
            buttonAddUser.Location = new Point(12, 413);
            buttonAddUser.Name = "buttonAddUser";
            buttonAddUser.Size = new Size(360, 40);
            buttonAddUser.TabIndex = 13;
            buttonAddUser.Text = "+ Добавить пользователя";
            buttonAddUser.UseVisualStyleBackColor = false;
            buttonAddUser.Click += buttonAddUser_Click;
            // 
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(384, 561);
            Controls.Add(buttonAddUser);
            Controls.Add(buttonCancel);
            Controls.Add(buttonApply);
            Controls.Add(buttonDeleteUser);
            Controls.Add(buttonEdit);
            Controls.Add(comboBoxRole);
            Controls.Add(label3);
            Controls.Add(textBoxPassword);
            Controls.Add(label2);
            Controls.Add(textBoxUsername);
            Controls.Add(label1);
            Controls.Add(labelUsers);
            Controls.Add(dataGridViewUsers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "UserManagementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Управление пользователями";
            Load += UserManagementForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewUsers;
        private Label labelUsers;
        private Label label1;
        private TextBox textBoxUsername;
        private TextBox textBoxPassword;
        private Label label2;
        private Label label3;
        private ComboBox comboBoxRole;
        private Button buttonEdit;
        private Button buttonDeleteUser;
        private Button buttonApply;
        private Button buttonCancel;
        private Button buttonAddUser;
    }
}