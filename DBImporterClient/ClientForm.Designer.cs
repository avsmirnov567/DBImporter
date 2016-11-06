namespace DBImporterClient
{
    partial class ClientForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadBtn = new System.Windows.Forms.Button();
            this.loadedDataTextBox = new System.Windows.Forms.TextBox();
            this.sendGroup = new System.Windows.Forms.Panel();
            this.sendBtn = new System.Windows.Forms.Button();
            this.radioBtnSockets = new System.Windows.Forms.RadioButton();
            this.radioBtnMSMQ = new System.Windows.Forms.RadioButton();
            this.connectBtn = new System.Windows.Forms.Button();
            this.serverPathTextBox = new System.Windows.Forms.TextBox();
            this.sendGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadBtn
            // 
            this.loadBtn.Enabled = false;
            this.loadBtn.Location = new System.Drawing.Point(12, 194);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(199, 29);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "Загрузить из CSV";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // loadedDataTextBox
            // 
            this.loadedDataTextBox.Location = new System.Drawing.Point(12, 45);
            this.loadedDataTextBox.Multiline = true;
            this.loadedDataTextBox.Name = "loadedDataTextBox";
            this.loadedDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.loadedDataTextBox.Size = new System.Drawing.Size(685, 142);
            this.loadedDataTextBox.TabIndex = 1;
            // 
            // sendGroup
            // 
            this.sendGroup.Controls.Add(this.sendBtn);
            this.sendGroup.Controls.Add(this.radioBtnSockets);
            this.sendGroup.Controls.Add(this.radioBtnMSMQ);
            this.sendGroup.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sendGroup.Location = new System.Drawing.Point(369, 193);
            this.sendGroup.Name = "sendGroup";
            this.sendGroup.Size = new System.Drawing.Size(329, 62);
            this.sendGroup.TabIndex = 2;
            // 
            // sendBtn
            // 
            this.sendBtn.Enabled = false;
            this.sendBtn.Location = new System.Drawing.Point(164, 13);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(147, 31);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "Отправить";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // radioBtnSockets
            // 
            this.radioBtnSockets.AutoSize = true;
            this.radioBtnSockets.Location = new System.Drawing.Point(12, 36);
            this.radioBtnSockets.Name = "radioBtnSockets";
            this.radioBtnSockets.Size = new System.Drawing.Size(79, 21);
            this.radioBtnSockets.TabIndex = 1;
            this.radioBtnSockets.TabStop = true;
            this.radioBtnSockets.Text = "Sockets";
            this.radioBtnSockets.UseVisualStyleBackColor = true;
            // 
            // radioBtnMSMQ
            // 
            this.radioBtnMSMQ.AutoSize = true;
            this.radioBtnMSMQ.Location = new System.Drawing.Point(12, 9);
            this.radioBtnMSMQ.Name = "radioBtnMSMQ";
            this.radioBtnMSMQ.Size = new System.Drawing.Size(71, 21);
            this.radioBtnMSMQ.TabIndex = 0;
            this.radioBtnMSMQ.TabStop = true;
            this.radioBtnMSMQ.Text = "MSMQ";
            this.radioBtnMSMQ.UseVisualStyleBackColor = true;
            // 
            // connectBtn
            // 
            this.connectBtn.Enabled = false;
            this.connectBtn.Location = new System.Drawing.Point(500, 7);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(198, 27);
            this.connectBtn.TabIndex = 3;
            this.connectBtn.Text = "Подключиться к серверу";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // serverPathTextBox
            // 
            this.serverPathTextBox.Location = new System.Drawing.Point(12, 12);
            this.serverPathTextBox.Name = "serverPathTextBox";
            this.serverPathTextBox.Size = new System.Drawing.Size(482, 22);
            this.serverPathTextBox.TabIndex = 4;
            this.serverPathTextBox.TextChanged += new System.EventHandler(this.serverPathTextBox_TextChanged);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(710, 267);
            this.Controls.Add(this.serverPathTextBox);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.sendGroup);
            this.Controls.Add(this.loadedDataTextBox);
            this.Controls.Add(this.loadBtn);
            this.Name = "ClientForm";
            this.Text = "DBImporter - Client";
            this.sendGroup.ResumeLayout(false);
            this.sendGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.TextBox loadedDataTextBox;
        private System.Windows.Forms.Panel sendGroup;
        private System.Windows.Forms.RadioButton radioBtnSockets;
        private System.Windows.Forms.RadioButton radioBtnMSMQ;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TextBox serverPathTextBox;
    }
}

