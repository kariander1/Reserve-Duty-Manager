namespace DarkDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panel_Top = new System.Windows.Forms.Panel();
            this.label_alarmList = new System.Windows.Forms.Label();
            this.button_quit = new System.Windows.Forms.Button();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.pictureBox_image = new System.Windows.Forms.PictureBox();
            this.button_Yes = new System.Windows.Forms.Button();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.label_Username = new System.Windows.Forms.Label();
            this.loadingCircle_Loading = new MRG.Controls.UI.LoadingCircle();
            this.panel_Top.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.panel_Top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Top.Controls.Add(this.label_alarmList);
            this.panel_Top.Controls.Add(this.button_quit);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(544, 33);
            this.panel_Top.TabIndex = 0;
            this.panel_Top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Top_MouseDown);
            // 
            // label_alarmList
            // 
            this.label_alarmList.AutoSize = true;
            this.label_alarmList.Location = new System.Drawing.Point(251, 3);
            this.label_alarmList.Name = "label_alarmList";
            this.label_alarmList.Size = new System.Drawing.Size(51, 21);
            this.label_alarmList.TabIndex = 1;
            this.label_alarmList.Text = "Login";
            this.label_alarmList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Top_MouseDown);
            // 
            // button_quit
            // 
            this.button_quit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_quit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_quit.BackgroundImage")));
            this.button_quit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_quit.FlatAppearance.BorderSize = 0;
            this.button_quit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_quit.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_quit.ForeColor = System.Drawing.Color.White;
            this.button_quit.Location = new System.Drawing.Point(507, 0);
            this.button_quit.Name = "button_quit";
            this.button_quit.Size = new System.Drawing.Size(32, 29);
            this.button_quit.TabIndex = 7;
            this.button_quit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_quit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_quit.UseVisualStyleBackColor = true;
            this.button_quit.Click += new System.EventHandler(this.button_quit_Click);
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Controls.Add(this.loadingCircle_Loading);
            this.panel_Bottom.Controls.Add(this.pictureBox_image);
            this.panel_Bottom.Controls.Add(this.button_Yes);
            this.panel_Bottom.Controls.Add(this.textBox_pass);
            this.panel_Bottom.Controls.Add(this.textBox_user);
            this.panel_Bottom.Controls.Add(this.label_password);
            this.panel_Bottom.Controls.Add(this.label_Username);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 33);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(544, 183);
            this.panel_Bottom.TabIndex = 1;
            // 
            // pictureBox_image
            // 
            this.pictureBox_image.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_image.BackgroundImage")));
            this.pictureBox_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_image.Location = new System.Drawing.Point(415, 23);
            this.pictureBox_image.Name = "pictureBox_image";
            this.pictureBox_image.Size = new System.Drawing.Size(117, 125);
            this.pictureBox_image.TabIndex = 11;
            this.pictureBox_image.TabStop = false;
            // 
            // button_Yes
            // 
            this.button_Yes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Yes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_Yes.BackgroundImage")));
            this.button_Yes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Yes.FlatAppearance.BorderSize = 0;
            this.button_Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Yes.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Yes.ForeColor = System.Drawing.Color.White;
            this.button_Yes.Location = new System.Drawing.Point(253, 121);
            this.button_Yes.Name = "button_Yes";
            this.button_Yes.Size = new System.Drawing.Size(50, 50);
            this.button_Yes.TabIndex = 10;
            this.button_Yes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_Yes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_Yes.UseVisualStyleBackColor = true;
            this.button_Yes.Visible = false;
            this.button_Yes.Click += new System.EventHandler(this.button_Yes_Click);
            // 
            // textBox_pass
            // 
            this.textBox_pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.textBox_pass.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox_pass.Location = new System.Drawing.Point(231, 69);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.PasswordChar = '*';
            this.textBox_pass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_pass.Size = new System.Drawing.Size(160, 27);
            this.textBox_pass.TabIndex = 3;
            this.textBox_pass.UseSystemPasswordChar = true;
            // 
            // textBox_user
            // 
            this.textBox_user.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_user.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.textBox_user.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox_user.Location = new System.Drawing.Point(231, 23);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_user.Size = new System.Drawing.Size(160, 27);
            this.textBox_user.TabIndex = 2;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(55, 75);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(82, 21);
            this.label_password.TabIndex = 1;
            this.label_password.Text = "Password";
            // 
            // label_Username
            // 
            this.label_Username.AutoSize = true;
            this.label_Username.Location = new System.Drawing.Point(55, 23);
            this.label_Username.Name = "label_Username";
            this.label_Username.Size = new System.Drawing.Size(58, 21);
            this.label_Username.TabIndex = 0;
            this.label_Username.Text = "Name";
            // 
            // loadingCircle_Loading
            // 
            this.loadingCircle_Loading.Active = false;
            this.loadingCircle_Loading.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle_Loading.InnerCircleRadius = 8;
            this.loadingCircle_Loading.Location = new System.Drawing.Point(242, 138);
            this.loadingCircle_Loading.Name = "loadingCircle_Loading";
            this.loadingCircle_Loading.NumberSpoke = 10;
            this.loadingCircle_Loading.OuterCircleRadius = 10;
            this.loadingCircle_Loading.RotationSpeed = 100;
            this.loadingCircle_Loading.Size = new System.Drawing.Size(75, 23);
            this.loadingCircle_Loading.SpokeThickness = 4;
            this.loadingCircle_Loading.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle_Loading.TabIndex = 12;
            this.loadingCircle_Loading.Text = "loadingCircle1";
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(544, 216);
            this.Controls.Add(this.panel_Bottom);
            this.Controls.Add(this.panel_Top);
            this.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlarmForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Button button_quit;
        private System.Windows.Forms.Label label_alarmList;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.TextBox textBox_pass;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.Label label_Username;
        private System.Windows.Forms.Button button_Yes;
        private System.Windows.Forms.PictureBox pictureBox_image;
        private MRG.Controls.UI.LoadingCircle loadingCircle_Loading;
    }
}