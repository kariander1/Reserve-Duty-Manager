namespace DarkDemo
{
    partial class AlarmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlarmForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel_Top = new System.Windows.Forms.Panel();
            this.button_quit = new System.Windows.Forms.Button();
            this.label_alarmList = new System.Windows.Forms.Label();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.dataGridView_Logs = new System.Windows.Forms.DataGridView();
            this.Column_Datetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_Top.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Logs)).BeginInit();
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
            // label_alarmList
            // 
            this.label_alarmList.AutoSize = true;
            this.label_alarmList.Location = new System.Drawing.Point(226, 3);
            this.label_alarmList.Name = "label_alarmList";
            this.label_alarmList.Size = new System.Drawing.Size(84, 21);
            this.label_alarmList.TabIndex = 1;
            this.label_alarmList.Text = "Alarm List";
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Controls.Add(this.dataGridView_Logs);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 35);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(544, 447);
            this.panel_Bottom.TabIndex = 1;
            // 
            // dataGridView_Logs
            // 
            this.dataGridView_Logs.AllowUserToAddRows = false;
            this.dataGridView_Logs.AllowUserToDeleteRows = false;
            this.dataGridView_Logs.AllowUserToResizeColumns = false;
            this.dataGridView_Logs.AllowUserToResizeRows = false;
            this.dataGridView_Logs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_Logs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(47)))));
            this.dataGridView_Logs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Logs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Logs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Datetime,
            this.Column_Message});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Logs.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Logs.EnableHeadersVisualStyles = false;
            this.dataGridView_Logs.GridColor = System.Drawing.Color.White;
            this.dataGridView_Logs.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Logs.MultiSelect = false;
            this.dataGridView_Logs.Name = "dataGridView_Logs";
            this.dataGridView_Logs.ReadOnly = true;
            this.dataGridView_Logs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Logs.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_Logs.RowHeadersVisible = false;
            this.dataGridView_Logs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Logs.Size = new System.Drawing.Size(544, 447);
            this.dataGridView_Logs.TabIndex = 9;
            // 
            // Column_Datetime
            // 
            this.Column_Datetime.HeaderText = "שעה";
            this.Column_Datetime.Name = "Column_Datetime";
            this.Column_Datetime.ReadOnly = true;
            this.Column_Datetime.Width = 67;
            // 
            // Column_Message
            // 
            this.Column_Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_Message.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column_Message.HeaderText = "הודעה";
            this.Column_Message.Name = "Column_Message";
            this.Column_Message.ReadOnly = true;
            this.Column_Message.Width = 77;
            // 
            // AlarmForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(544, 482);
            this.Controls.Add(this.panel_Bottom);
            this.Controls.Add(this.panel_Top);
            this.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AlarmForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlarmForm";
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            this.panel_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Logs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Button button_quit;
        private System.Windows.Forms.Label label_alarmList;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.DataGridView dataGridView_Logs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Datetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Message;
    }
}