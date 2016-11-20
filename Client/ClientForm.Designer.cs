namespace Client
{
    partial class ClientForm
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
            this.btnLoadCsv = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.rbtnSockets = new System.Windows.Forms.RadioButton();
            this.rbtnMSMQ = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadCsv
            // 
            this.btnLoadCsv.Enabled = false;
            this.btnLoadCsv.Location = new System.Drawing.Point(13, 13);
            this.btnLoadCsv.Name = "btnLoadCsv";
            this.btnLoadCsv.Size = new System.Drawing.Size(194, 27);
            this.btnLoadCsv.TabIndex = 0;
            this.btnLoadCsv.Text = "Загрузить из CSV";
            this.btnLoadCsv.UseVisualStyleBackColor = true;
            this.btnLoadCsv.Click += new System.EventHandler(this.btnLoadCsv_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 47);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(537, 185);
            this.textBox1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.rbtnSockets);
            this.groupBox1.Controls.Add(this.rbtnMSMQ);
            this.groupBox1.Location = new System.Drawing.Point(12, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 76);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите способ отправки и нажмите \"Отправить\"";
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(376, 22);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(155, 48);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rbtnSockets
            // 
            this.rbtnSockets.AutoSize = true;
            this.rbtnSockets.Location = new System.Drawing.Point(7, 49);
            this.rbtnSockets.Name = "rbtnSockets";
            this.rbtnSockets.Size = new System.Drawing.Size(79, 21);
            this.rbtnSockets.TabIndex = 1;
            this.rbtnSockets.Text = "Sockets";
            this.rbtnSockets.UseVisualStyleBackColor = true;
            // 
            // rbtnMSMQ
            // 
            this.rbtnMSMQ.AutoSize = true;
            this.rbtnMSMQ.Checked = true;
            this.rbtnMSMQ.Location = new System.Drawing.Point(7, 22);
            this.rbtnMSMQ.Name = "rbtnMSMQ";
            this.rbtnMSMQ.Size = new System.Drawing.Size(71, 21);
            this.rbtnMSMQ.TabIndex = 0;
            this.rbtnMSMQ.TabStop = true;
            this.rbtnMSMQ.Text = "MSMQ";
            this.rbtnMSMQ.UseVisualStyleBackColor = true;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 336);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnLoadCsv);
            this.Name = "ClientForm";
            this.Text = "ClientConnectForm";
            this.Load += new System.EventHandler(this.ClientConnectForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadCsv;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RadioButton rbtnSockets;
        private System.Windows.Forms.RadioButton rbtnMSMQ;
    }
}