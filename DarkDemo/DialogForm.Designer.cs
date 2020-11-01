namespace DarkDemo
{
    partial class DialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogForm));
            this.panel_Top = new System.Windows.Forms.Panel();
            this.label_alarmList = new System.Windows.Forms.Label();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.richTextBox_Text = new System.Windows.Forms.RichTextBox();
            this.button_Yes = new System.Windows.Forms.Button();
            this.button_No = new System.Windows.Forms.Button();
            this.panel_Top.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Top
            // 
            this.panel_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(73)))));
            this.panel_Top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Top.Controls.Add(this.label_alarmList);
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
            this.label_alarmList.Location = new System.Drawing.Point(226, 3);
            this.label_alarmList.Name = "label_alarmList";
            this.label_alarmList.Size = new System.Drawing.Size(0, 21);
            this.label_alarmList.TabIndex = 1;
            this.label_alarmList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Top_MouseDown);
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Bottom.Controls.Add(this.richTextBox_Text);
            this.panel_Bottom.Controls.Add(this.button_Yes);
            this.panel_Bottom.Controls.Add(this.button_No);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 33);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(544, 160);
            this.panel_Bottom.TabIndex = 1;
            // 
            // richTextBox_Text
            // 
            this.richTextBox_Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.richTextBox_Text.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_Text.ForeColor = System.Drawing.SystemColors.Info;
            this.richTextBox_Text.Location = new System.Drawing.Point(12, 6);
            this.richTextBox_Text.Name = "richTextBox_Text";
            this.richTextBox_Text.ReadOnly = true;
            this.richTextBox_Text.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.richTextBox_Text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.richTextBox_Text.Size = new System.Drawing.Size(520, 86);
            this.richTextBox_Text.TabIndex = 10;
            this.richTextBox_Text.Text = "";
            // 
            // button_Yes
            // 
            this.button_Yes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Yes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_Yes.BackgroundImage")));
            this.button_Yes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button_Yes.FlatAppearance.BorderSize = 0;
            this.button_Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Yes.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Yes.ForeColor = System.Drawing.Color.White;
            this.button_Yes.Location = new System.Drawing.Point(198, 98);
            this.button_Yes.Name = "button_Yes";
            this.button_Yes.Size = new System.Drawing.Size(50, 50);
            this.button_Yes.TabIndex = 9;
            this.button_Yes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_Yes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_Yes.UseVisualStyleBackColor = true;
            // 
            // button_No
            // 
            this.button_No.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_No.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_No.BackgroundImage")));
            this.button_No.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_No.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button_No.FlatAppearance.BorderSize = 0;
            this.button_No.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_No.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_No.ForeColor = System.Drawing.Color.White;
            this.button_No.Location = new System.Drawing.Point(312, 98);
            this.button_No.Name = "button_No";
            this.button_No.Size = new System.Drawing.Size(50, 50);
            this.button_No.TabIndex = 8;
            this.button_No.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_No.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button_No.UseVisualStyleBackColor = true;
            // 
            // DialogForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(544, 193);
            this.Controls.Add(this.panel_Bottom);
            this.Controls.Add(this.panel_Top);
            this.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DialogForm";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlarmForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogForm_FormClosing);
            this.Load += new System.EventHandler(this.DialogForm_Load);
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            this.panel_Bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Label label_alarmList;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Button button_No;
        private System.Windows.Forms.Button button_Yes;
        private System.Windows.Forms.RichTextBox richTextBox_Text;
    }
}