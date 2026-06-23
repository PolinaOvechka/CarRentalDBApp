namespace CarRentalDBForm
{
    partial class LoginForm
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
            labelLogin = new Label();
            labelPassword = new Label();
            textBoxLogin = new TextBox();
            textBoxPassword = new TextBox();
            panel1 = new Panel();
            labelTitle = new Label();
            buttonLogin = new Button();
            buttonExit = new Button();
            labelError = new Label();
            panelError = new Panel();
            panel1.SuspendLayout();
            panelError.SuspendLayout();
            SuspendLayout();
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Font = new Font("Inter Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelLogin.ForeColor = Color.Gray;
            labelLogin.Location = new Point(25, 96);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(54, 16);
            labelLogin.TabIndex = 1;
            labelLogin.Text = "ЛОГИН";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Inter Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelPassword.ForeColor = Color.Gray;
            labelPassword.Location = new Point(25, 165);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(65, 16);
            labelPassword.TabIndex = 2;
            labelPassword.Text = "ПАРОЛЬ";
            // 
            // textBoxLogin
            // 
            textBoxLogin.BackColor = Color.White;
            textBoxLogin.Cursor = Cursors.IBeam;
            textBoxLogin.Font = new Font("Inter", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxLogin.ForeColor = Color.DarkGray;
            textBoxLogin.Location = new Point(28, 118);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.Size = new Size(283, 27);
            textBoxLogin.TabIndex = 3;
            textBoxLogin.Text = "Введите логин";
            textBoxLogin.Enter += textBoxLogin_Enter;
            textBoxLogin.Leave += textBoxLogin_Leave;
            // 
            // textBoxPassword
            // 
            textBoxPassword.BackColor = Color.White;
            textBoxPassword.Cursor = Cursors.IBeam;
            textBoxPassword.Font = new Font("Inter", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxPassword.ForeColor = Color.DarkGray;
            textBoxPassword.Location = new Point(28, 188);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(283, 27);
            textBoxPassword.TabIndex = 4;
            textBoxPassword.Text = "Введите пароль";
            textBoxPassword.Enter += textBoxPassword_Enter;
            textBoxPassword.Leave += textBoxPassword_Leave;
            // 
            // panel1
            // 
            panel1.BackColor = Color.RoyalBlue;
            panel1.Controls.Add(labelTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(339, 73);
            panel1.TabIndex = 5;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Inter V Black", 16F, FontStyle.Bold);
            labelTitle.ForeColor = Color.WhiteSmoke;
            labelTitle.ImageAlign = ContentAlignment.MiddleRight;
            labelTitle.Location = new Point(24, 24);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(296, 26);
            labelTitle.TabIndex = 1;
            labelTitle.Text = "АРЕНДА АВТОМОБИЛЕЙ";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.RoyalBlue;
            buttonLogin.FlatAppearance.BorderSize = 0;
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("Inter Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonLogin.ForeColor = Color.WhiteSmoke;
            buttonLogin.Location = new Point(28, 274);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(283, 40);
            buttonLogin.TabIndex = 6;
            buttonLogin.Text = "Войти";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.Transparent;
            buttonExit.FlatAppearance.BorderColor = Color.RoyalBlue;
            buttonExit.FlatAppearance.BorderSize = 2;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Inter Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonExit.ForeColor = Color.RoyalBlue;
            buttonExit.Location = new Point(28, 320);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(283, 40);
            buttonExit.TabIndex = 7;
            buttonExit.Text = "Выход";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // labelError
            // 
            labelError.AutoSize = true;
            labelError.BackColor = Color.Transparent;
            labelError.Font = new Font("Inter Semi Bold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelError.ForeColor = Color.WhiteSmoke;
            labelError.Location = new Point(22, 6);
            labelError.Name = "labelError";
            labelError.Size = new Size(240, 19);
            labelError.TabIndex = 8;
            labelError.Text = "Неверный логин или пароль!";
            labelError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelError
            // 
            panelError.BackColor = Color.Crimson;
            panelError.Controls.Add(labelError);
            panelError.Location = new Point(28, 230);
            panelError.Name = "panelError";
            panelError.Size = new Size(283, 30);
            panelError.TabIndex = 9;
            panelError.Visible = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(339, 381);
            Controls.Add(panelError);
            Controls.Add(buttonExit);
            Controls.Add(buttonLogin);
            Controls.Add(panel1);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxLogin);
            Controls.Add(labelPassword);
            Controls.Add(labelLogin);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход в систему";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelError.ResumeLayout(false);
            panelError.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelLogin;
        private Label labelPassword;
        private TextBox textBoxLogin;
        private TextBox textBoxPassword;
        private Panel panel1;
        private Label labelTitle;
        private Button buttonLogin;
        private Button buttonExit;
        private Label labelError;
        private Panel panelError;
    }
}