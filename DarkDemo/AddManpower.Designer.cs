namespace DarkDemo
{
    partial class AddManpower
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddManpower));
            this.panel_Top = new System.Windows.Forms.Panel();
            this.label_Add = new System.Windows.Forms.Label();
            this.button_quit = new System.Windows.Forms.Button();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.pictureBox_Rank = new System.Windows.Forms.PictureBox();
            this.checkBox_InManPower = new System.Windows.Forms.CheckBox();
            this.comboBox_rank = new System.Windows.Forms.ComboBox();
            this.label_Rank = new System.Windows.Forms.Label();
            this.textBox_LastName = new System.Windows.Forms.TextBox();
            this.label_LastName = new System.Windows.Forms.Label();
            this.textBox_FirstName = new System.Windows.Forms.TextBox();
            this.button_add = new System.Windows.Forms.Button();
            this.listBox_Log = new System.Windows.Forms.ListBox();
            this.button_Validate = new System.Windows.Forms.Button();
            this.checkedListBox_Tasks = new System.Windows.Forms.CheckedListBox();
            this.label_Tasks = new System.Windows.Forms.Label();
            this.dateTimePicker_ResignDate = new System.Windows.Forms.DateTimePicker();
            this.label_ResignDate = new System.Windows.Forms.Label();
            this.textBox_PersonalNumber = new System.Windows.Forms.TextBox();
            this.label_PersonalNumber = new System.Windows.Forms.Label();
            this.label_SoldierName = new System.Windows.Forms.Label();
            this.panel_Top.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Rank)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.panel_Top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Top.Controls.Add(this.label_Add);
            this.panel_Top.Controls.Add(this.button_quit);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(354, 33);
            this.panel_Top.TabIndex = 0;
            this.panel_Top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Top_MouseDown);
            // 
            // label_Add
            // 
            this.label_Add.AutoSize = true;
            this.label_Add.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Add.Location = new System.Drawing.Point(142, 2);
            this.label_Add.Name = "label_Add";
            this.label_Add.Size = new System.Drawing.Size(85, 30);
            this.label_Add.TabIndex = 1;
            this.label_Add.Text = "כח אדם";
            this.label_Add.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Top_MouseDown);
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
            this.button_quit.Location = new System.Drawing.Point(317, 0);
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
            this.panel_Bottom.Controls.Add(this.pictureBox_Rank);
            this.panel_Bottom.Controls.Add(this.checkBox_InManPower);
            this.panel_Bottom.Controls.Add(this.comboBox_rank);
            this.panel_Bottom.Controls.Add(this.label_Rank);
            this.panel_Bottom.Controls.Add(this.textBox_LastName);
            this.panel_Bottom.Controls.Add(this.label_LastName);
            this.panel_Bottom.Controls.Add(this.textBox_FirstName);
            this.panel_Bottom.Controls.Add(this.button_add);
            this.panel_Bottom.Controls.Add(this.listBox_Log);
            this.panel_Bottom.Controls.Add(this.button_Validate);
            this.panel_Bottom.Controls.Add(this.checkedListBox_Tasks);
            this.panel_Bottom.Controls.Add(this.label_Tasks);
            this.panel_Bottom.Controls.Add(this.dateTimePicker_ResignDate);
            this.panel_Bottom.Controls.Add(this.label_ResignDate);
            this.panel_Bottom.Controls.Add(this.textBox_PersonalNumber);
            this.panel_Bottom.Controls.Add(this.label_PersonalNumber);
            this.panel_Bottom.Controls.Add(this.label_SoldierName);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 35);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(354, 447);
            this.panel_Bottom.TabIndex = 1;
            // 
            // pictureBox_Rank
            // 
            this.pictureBox_Rank.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_Rank.Location = new System.Drawing.Point(206, 160);
            this.pictureBox_Rank.Name = "pictureBox_Rank";
            this.pictureBox_Rank.Size = new System.Drawing.Size(69, 29);
            this.pictureBox_Rank.TabIndex = 27;
            this.pictureBox_Rank.TabStop = false;
            // 
            // checkBox_InManPower
            // 
            this.checkBox_InManPower.AutoSize = true;
            this.checkBox_InManPower.Checked = true;
            this.checkBox_InManPower.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_InManPower.Location = new System.Drawing.Point(22, 195);
            this.checkBox_InManPower.Name = "checkBox_InManPower";
            this.checkBox_InManPower.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_InManPower.Size = new System.Drawing.Size(86, 25);
            this.checkBox_InManPower.TabIndex = 26;
            this.checkBox_InManPower.Text = "בשבצ\"ק";
            this.checkBox_InManPower.UseVisualStyleBackColor = true;
            // 
            // comboBox_rank
            // 
            this.comboBox_rank.FormattingEnabled = true;
            this.comboBox_rank.Location = new System.Drawing.Point(22, 160);
            this.comboBox_rank.Name = "comboBox_rank";
            this.comboBox_rank.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBox_rank.Size = new System.Drawing.Size(174, 29);
            this.comboBox_rank.TabIndex = 25;
            this.comboBox_rank.SelectedIndexChanged += new System.EventHandler(this.comboBox_rank_SelectedIndexChanged);
            // 
            // label_Rank
            // 
            this.label_Rank.AutoSize = true;
            this.label_Rank.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Rank.Location = new System.Drawing.Point(281, 159);
            this.label_Rank.Name = "label_Rank";
            this.label_Rank.Size = new System.Drawing.Size(61, 30);
            this.label_Rank.TabIndex = 24;
            this.label_Rank.Text = "דרגה";
            // 
            // textBox_LastName
            // 
            this.textBox_LastName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.textBox_LastName.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox_LastName.Location = new System.Drawing.Point(22, 87);
            this.textBox_LastName.Name = "textBox_LastName";
            this.textBox_LastName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_LastName.Size = new System.Drawing.Size(174, 27);
            this.textBox_LastName.TabIndex = 23;
            // 
            // label_LastName
            // 
            this.label_LastName.AutoSize = true;
            this.label_LastName.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_LastName.Location = new System.Drawing.Point(215, 82);
            this.label_LastName.Name = "label_LastName";
            this.label_LastName.Size = new System.Drawing.Size(127, 30);
            this.label_LastName.TabIndex = 22;
            this.label_LastName.Text = "שם משפחה";
            // 
            // textBox_FirstName
            // 
            this.textBox_FirstName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.textBox_FirstName.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox_FirstName.Location = new System.Drawing.Point(22, 52);
            this.textBox_FirstName.Name = "textBox_FirstName";
            this.textBox_FirstName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_FirstName.Size = new System.Drawing.Size(174, 27);
            this.textBox_FirstName.TabIndex = 21;
            // 
            // button_add
            // 
            this.button_add.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_add.BackgroundImage")));
            this.button_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_add.FlatAppearance.BorderSize = 0;
            this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_add.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_add.ForeColor = System.Drawing.Color.White;
            this.button_add.Location = new System.Drawing.Point(294, 264);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(54, 99);
            this.button_add.TabIndex = 20;
            this.button_add.Text = "הוסף";
            this.button_add.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // listBox_Log
            // 
            this.listBox_Log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.listBox_Log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBox_Log.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Log.ForeColor = System.Drawing.SystemColors.Info;
            this.listBox_Log.FormattingEnabled = true;
            this.listBox_Log.HorizontalScrollbar = true;
            this.listBox_Log.ItemHeight = 17;
            this.listBox_Log.Location = new System.Drawing.Point(0, 375);
            this.listBox_Log.Name = "listBox_Log";
            this.listBox_Log.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listBox_Log.Size = new System.Drawing.Size(354, 72);
            this.listBox_Log.TabIndex = 19;
            this.listBox_Log.Visible = false;
            // 
            // button_Validate
            // 
            this.button_Validate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Validate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_Validate.BackgroundImage")));
            this.button_Validate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Validate.FlatAppearance.BorderSize = 0;
            this.button_Validate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Validate.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Validate.ForeColor = System.Drawing.Color.White;
            this.button_Validate.Location = new System.Drawing.Point(229, 264);
            this.button_Validate.Name = "button_Validate";
            this.button_Validate.Size = new System.Drawing.Size(54, 99);
            this.button_Validate.TabIndex = 18;
            this.button_Validate.Text = "בדוק";
            this.button_Validate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_Validate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_Validate.UseVisualStyleBackColor = true;
            this.button_Validate.Click += new System.EventHandler(this.button_Validate_Click);
            // 
            // checkedListBox_Tasks
            // 
            this.checkedListBox_Tasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.checkedListBox_Tasks.CheckOnClick = true;
            this.checkedListBox_Tasks.ForeColor = System.Drawing.SystemColors.Info;
            this.checkedListBox_Tasks.FormattingEnabled = true;
            this.checkedListBox_Tasks.Location = new System.Drawing.Point(22, 227);
            this.checkedListBox_Tasks.Name = "checkedListBox_Tasks";
            this.checkedListBox_Tasks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkedListBox_Tasks.Size = new System.Drawing.Size(174, 136);
            this.checkedListBox_Tasks.TabIndex = 17;
            // 
            // label_Tasks
            // 
            this.label_Tasks.AutoSize = true;
            this.label_Tasks.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Tasks.Location = new System.Drawing.Point(250, 227);
            this.label_Tasks.Name = "label_Tasks";
            this.label_Tasks.Size = new System.Drawing.Size(92, 30);
            this.label_Tasks.TabIndex = 16;
            this.label_Tasks.Text = "הסמכות";
            // 
            // dateTimePicker_ResignDate
            // 
            this.dateTimePicker_ResignDate.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.dateTimePicker_ResignDate.CalendarTitleForeColor = System.Drawing.SystemColors.Info;
            this.dateTimePicker_ResignDate.CustomFormat = "";
            this.dateTimePicker_ResignDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_ResignDate.Location = new System.Drawing.Point(22, 126);
            this.dateTimePicker_ResignDate.Name = "dateTimePicker_ResignDate";
            this.dateTimePicker_ResignDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dateTimePicker_ResignDate.RightToLeftLayout = true;
            this.dateTimePicker_ResignDate.ShowUpDown = true;
            this.dateTimePicker_ResignDate.Size = new System.Drawing.Size(174, 27);
            this.dateTimePicker_ResignDate.TabIndex = 13;
            // 
            // label_ResignDate
            // 
            this.label_ResignDate.AutoSize = true;
            this.label_ResignDate.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ResignDate.Location = new System.Drawing.Point(201, 123);
            this.label_ResignDate.Name = "label_ResignDate";
            this.label_ResignDate.Size = new System.Drawing.Size(141, 30);
            this.label_ResignDate.TabIndex = 12;
            this.label_ResignDate.Text = "תאריך שחרור";
            // 
            // textBox_PersonalNumber
            // 
            this.textBox_PersonalNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.textBox_PersonalNumber.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox_PersonalNumber.Location = new System.Drawing.Point(22, 14);
            this.textBox_PersonalNumber.Name = "textBox_PersonalNumber";
            this.textBox_PersonalNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_PersonalNumber.Size = new System.Drawing.Size(174, 27);
            this.textBox_PersonalNumber.TabIndex = 11;
            // 
            // label_PersonalNumber
            // 
            this.label_PersonalNumber.AutoSize = true;
            this.label_PersonalNumber.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_PersonalNumber.Location = new System.Drawing.Point(224, 14);
            this.label_PersonalNumber.Name = "label_PersonalNumber";
            this.label_PersonalNumber.Size = new System.Drawing.Size(118, 30);
            this.label_PersonalNumber.TabIndex = 9;
            this.label_PersonalNumber.Text = "מספר אישי";
            // 
            // label_SoldierName
            // 
            this.label_SoldierName.AutoSize = true;
            this.label_SoldierName.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SoldierName.Location = new System.Drawing.Point(244, 47);
            this.label_SoldierName.Name = "label_SoldierName";
            this.label_SoldierName.Size = new System.Drawing.Size(98, 30);
            this.label_SoldierName.TabIndex = 8;
            this.label_SoldierName.Text = "שם פרטי";
            // 
            // AddManpower
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(354, 482);
            this.Controls.Add(this.panel_Bottom);
            this.Controls.Add(this.panel_Top);
            this.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddManpower";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlarmForm";
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Rank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Button button_quit;
        private System.Windows.Forms.Label label_Add;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Label label_PersonalNumber;
        private System.Windows.Forms.Label label_SoldierName;
        private System.Windows.Forms.CheckedListBox checkedListBox_Tasks;
        private System.Windows.Forms.Label label_Tasks;
        private System.Windows.Forms.DateTimePicker dateTimePicker_ResignDate;
        private System.Windows.Forms.Label label_ResignDate;
        private System.Windows.Forms.TextBox textBox_PersonalNumber;
        private System.Windows.Forms.Button button_Validate;
        private System.Windows.Forms.ListBox listBox_Log;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.TextBox textBox_FirstName;
        private System.Windows.Forms.CheckBox checkBox_InManPower;
        private System.Windows.Forms.ComboBox comboBox_rank;
        private System.Windows.Forms.Label label_Rank;
        private System.Windows.Forms.TextBox textBox_LastName;
        private System.Windows.Forms.Label label_LastName;
        private System.Windows.Forms.PictureBox pictureBox_Rank;
    }
}