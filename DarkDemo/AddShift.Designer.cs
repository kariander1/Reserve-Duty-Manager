namespace DarkDemo
{
    partial class AddShift
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddShift));
            this.panel_Top = new System.Windows.Forms.Panel();
            this.label_Add = new System.Windows.Forms.Label();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.comboBox_team = new System.Windows.Forms.ComboBox();
            this.label_Team = new System.Windows.Forms.Label();
            this.listBox_Log = new System.Windows.Forms.ListBox();
            this.textBox_ShiftName = new System.Windows.Forms.TextBox();
            this.label_ShiftName = new System.Windows.Forms.Label();
            this.label_SoldierName = new System.Windows.Forms.Label();
            this.numericUpDown_validity = new System.Windows.Forms.NumericUpDown();
            this.checkBox_AlwaysValid = new System.Windows.Forms.CheckBox();
            this.button_add = new System.Windows.Forms.Button();
            this.button_quit = new System.Windows.Forms.Button();
            this.panel_Top.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_validity)).BeginInit();
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
            this.label_Add.Size = new System.Drawing.Size(88, 30);
            this.label_Add.TabIndex = 1;
            this.label_Add.Text = "משמרת";
            this.label_Add.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Top_MouseDown);
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Controls.Add(this.checkBox_AlwaysValid);
            this.panel_Bottom.Controls.Add(this.numericUpDown_validity);
            this.panel_Bottom.Controls.Add(this.comboBox_team);
            this.panel_Bottom.Controls.Add(this.label_Team);
            this.panel_Bottom.Controls.Add(this.button_add);
            this.panel_Bottom.Controls.Add(this.listBox_Log);
            this.panel_Bottom.Controls.Add(this.textBox_ShiftName);
            this.panel_Bottom.Controls.Add(this.label_ShiftName);
            this.panel_Bottom.Controls.Add(this.label_SoldierName);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 33);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(354, 321);
            this.panel_Bottom.TabIndex = 1;
            // 
            // comboBox_team
            // 
            this.comboBox_team.FormattingEnabled = true;
            this.comboBox_team.Location = new System.Drawing.Point(19, 90);
            this.comboBox_team.Name = "comboBox_team";
            this.comboBox_team.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBox_team.Size = new System.Drawing.Size(174, 29);
            this.comboBox_team.TabIndex = 25;
            // 
            // label_Team
            // 
            this.label_Team.AutoSize = true;
            this.label_Team.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Team.Location = new System.Drawing.Point(287, 89);
            this.label_Team.Name = "label_Team";
            this.label_Team.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_Team.Size = new System.Drawing.Size(54, 30);
            this.label_Team.TabIndex = 24;
            this.label_Team.Text = "צוות";
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
            this.listBox_Log.Location = new System.Drawing.Point(0, 249);
            this.listBox_Log.Name = "listBox_Log";
            this.listBox_Log.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listBox_Log.Size = new System.Drawing.Size(354, 72);
            this.listBox_Log.TabIndex = 19;
            this.listBox_Log.Visible = false;
            // 
            // textBox_ShiftName
            // 
            this.textBox_ShiftName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.textBox_ShiftName.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox_ShiftName.Location = new System.Drawing.Point(19, 14);
            this.textBox_ShiftName.Name = "textBox_ShiftName";
            this.textBox_ShiftName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox_ShiftName.Size = new System.Drawing.Size(174, 27);
            this.textBox_ShiftName.TabIndex = 11;
            // 
            // label_ShiftName
            // 
            this.label_ShiftName.AutoSize = true;
            this.label_ShiftName.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ShiftName.Location = new System.Drawing.Point(215, 9);
            this.label_ShiftName.Name = "label_ShiftName";
            this.label_ShiftName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_ShiftName.Size = new System.Drawing.Size(126, 30);
            this.label_ShiftName.TabIndex = 9;
            this.label_ShiftName.Text = "שם משמרת";
            // 
            // label_SoldierName
            // 
            this.label_SoldierName.AutoSize = true;
            this.label_SoldierName.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SoldierName.Location = new System.Drawing.Point(204, 52);
            this.label_SoldierName.Name = "label_SoldierName";
            this.label_SoldierName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_SoldierName.Size = new System.Drawing.Size(138, 30);
            this.label_SoldierName.TabIndex = 8;
            this.label_SoldierName.Text = "תוקף כשירות";
            // 
            // numericUpDown_validity
            // 
            this.numericUpDown_validity.Location = new System.Drawing.Point(94, 52);
            this.numericUpDown_validity.Name = "numericUpDown_validity";
            this.numericUpDown_validity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numericUpDown_validity.Size = new System.Drawing.Size(99, 27);
            this.numericUpDown_validity.TabIndex = 27;
            this.numericUpDown_validity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown_validity.ThousandsSeparator = true;
            this.numericUpDown_validity.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // checkBox_AlwaysValid
            // 
            this.checkBox_AlwaysValid.AutoSize = true;
            this.checkBox_AlwaysValid.Location = new System.Drawing.Point(19, 54);
            this.checkBox_AlwaysValid.Name = "checkBox_AlwaysValid";
            this.checkBox_AlwaysValid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_AlwaysValid.Size = new System.Drawing.Size(64, 25);
            this.checkBox_AlwaysValid.TabIndex = 28;
            this.checkBox_AlwaysValid.Text = "תמיד";
            this.checkBox_AlwaysValid.UseVisualStyleBackColor = true;
            this.checkBox_AlwaysValid.CheckedChanged += new System.EventHandler(this.checkBox_AlwaysValid_CheckedChanged);
            // 
            // button_add
            // 
            this.button_add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_add.BackgroundImage")));
            this.button_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_add.FlatAppearance.BorderSize = 0;
            this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_add.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_add.ForeColor = System.Drawing.Color.White;
            this.button_add.Location = new System.Drawing.Point(148, 142);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(56, 101);
            this.button_add.TabIndex = 20;
            this.button_add.Text = "הוסף";
            this.button_add.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
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
            // AddShift
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(354, 354);
            this.Controls.Add(this.panel_Bottom);
            this.Controls.Add(this.panel_Top);
            this.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddShift";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlarmForm";
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_validity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Button button_quit;
        private System.Windows.Forms.Label label_Add;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Label label_ShiftName;
        private System.Windows.Forms.Label label_SoldierName;
        private System.Windows.Forms.TextBox textBox_ShiftName;
        private System.Windows.Forms.ListBox listBox_Log;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.ComboBox comboBox_team;
        private System.Windows.Forms.Label label_Team;
        private System.Windows.Forms.CheckBox checkBox_AlwaysValid;
        private System.Windows.Forms.NumericUpDown numericUpDown_validity;
    }
}