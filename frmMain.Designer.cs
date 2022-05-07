namespace Compiler_Design {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.textEditor = new System.Windows.Forms.TextBox();
            this.DGVVE = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelVE = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageConsole = new System.Windows.Forms.TabPage();
            this.tabPageSemanticAnalyzer = new System.Windows.Forms.TabPage();
            this.txtError = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEditor = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGVVE)).BeginInit();
            this.panelVE.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageConsole.SuspendLayout();
            this.tabPageSemanticAnalyzer.SuspendLayout();
            this.panelEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(110)))), ((int)(((byte)(114)))));
            this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtConsole.ForeColor = System.Drawing.Color.White;
            this.txtConsole.Location = new System.Drawing.Point(3, 3);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.Size = new System.Drawing.Size(338, 368);
            this.txtConsole.TabIndex = 5;
            // 
            // btnRun
            // 
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Location = new System.Drawing.Point(9, 31);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(832, 37);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // textEditor
            // 
            this.textEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(25)))), ((int)(((byte)(38)))));
            this.textEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textEditor.ForeColor = System.Drawing.Color.White;
            this.textEditor.Location = new System.Drawing.Point(9, 73);
            this.textEditor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textEditor.Multiline = true;
            this.textEditor.Name = "textEditor";
            this.textEditor.Size = new System.Drawing.Size(832, 622);
            this.textEditor.TabIndex = 3;
            this.textEditor.UseWaitCursor = true;
            // 
            // DGVVE
            // 
            this.DGVVE.AllowUserToAddRows = false;
            this.DGVVE.AllowUserToDeleteRows = false;
            this.DGVVE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVVE.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(190)))), ((int)(((byte)(195)))));
            this.DGVVE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVVE.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.type,
            this.value});
            this.DGVVE.Location = new System.Drawing.Point(848, 23);
            this.DGVVE.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DGVVE.Name = "DGVVE";
            this.DGVVE.ReadOnly = true;
            this.DGVVE.RowHeadersWidth = 51;
            this.DGVVE.RowTemplate.Height = 24;
            this.DGVVE.Size = new System.Drawing.Size(351, 271);
            this.DGVVE.TabIndex = 7;
            // 
            // name
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.name.DefaultCellStyle = dataGridViewCellStyle1;
            this.name.HeaderText = "Name";
            this.name.MinimumWidth = 6;
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 125;
            // 
            // type
            // 
            this.type.HeaderText = "Type";
            this.type.MinimumWidth = 6;
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 125;
            // 
            // value
            // 
            this.value.HeaderText = "Value";
            this.value.MinimumWidth = 6;
            this.value.Name = "value";
            this.value.ReadOnly = true;
            this.value.Width = 125;
            // 
            // panelVE
            // 
            this.panelVE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelVE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.panelVE.Controls.Add(this.label2);
            this.panelVE.Location = new System.Drawing.Point(848, 0);
            this.panelVE.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelVE.Name = "panelVE";
            this.panelVE.Size = new System.Drawing.Size(349, 25);
            this.panelVE.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(2, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Variable Explorer";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageConsole);
            this.tabControl1.Controls.Add(this.tabPageSemanticAnalyzer);
            this.tabControl1.Location = new System.Drawing.Point(848, 300);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(352, 402);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPageConsole
            // 
            this.tabPageConsole.Controls.Add(this.txtConsole);
            this.tabPageConsole.Location = new System.Drawing.Point(4, 24);
            this.tabPageConsole.Name = "tabPageConsole";
            this.tabPageConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsole.Size = new System.Drawing.Size(344, 374);
            this.tabPageConsole.TabIndex = 0;
            this.tabPageConsole.Text = "Console";
            this.tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // tabPageSemanticAnalyzer
            // 
            this.tabPageSemanticAnalyzer.Controls.Add(this.txtError);
            this.tabPageSemanticAnalyzer.Location = new System.Drawing.Point(4, 24);
            this.tabPageSemanticAnalyzer.Name = "tabPageSemanticAnalyzer";
            this.tabPageSemanticAnalyzer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSemanticAnalyzer.Size = new System.Drawing.Size(344, 374);
            this.tabPageSemanticAnalyzer.TabIndex = 1;
            this.tabPageSemanticAnalyzer.Text = "Semantic Analyzer";
            this.tabPageSemanticAnalyzer.UseVisualStyleBackColor = true;
            // 
            // txtError
            // 
            this.txtError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(110)))), ((int)(((byte)(114)))));
            this.txtError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtError.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtError.ForeColor = System.Drawing.Color.White;
            this.txtError.Location = new System.Drawing.Point(3, 3);
            this.txtError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(338, 368);
            this.txtError.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(11, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Editor";
            // 
            // panelEditor
            // 
            this.panelEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.panelEditor.Controls.Add(this.label1);
            this.panelEditor.Location = new System.Drawing.Point(0, 0);
            this.panelEditor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(848, 25);
            this.panelEditor.TabIndex = 8;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 698);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.panelVE);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.DGVVE);
            this.Controls.Add(this.textEditor);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compiler Design";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.DGVVE)).EndInit();
            this.panelVE.ResumeLayout(false);
            this.panelVE.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageConsole.ResumeLayout(false);
            this.tabPageConsole.PerformLayout();
            this.tabPageSemanticAnalyzer.ResumeLayout(false);
            this.tabPageSemanticAnalyzer.PerformLayout();
            this.panelEditor.ResumeLayout(false);
            this.panelEditor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox textEditor;
        private System.Windows.Forms.DataGridView DGVVE;
        private System.Windows.Forms.Panel panelVE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private TabControl tabControl1;
        private TabPage tabPageConsole;
        private TabPage tabPageSemanticAnalyzer;
        private TextBox txtError;
        private Label label1;
        private Panel panelEditor;
    }
}

