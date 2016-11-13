namespace Client
{
    partial class ClientConnectForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbQueue = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Для обмена ключами, введите адрес серверной очереди:";
            // 
            // tbQueue
            // 
            this.tbQueue.Location = new System.Drawing.Point(16, 34);
            this.tbQueue.Name = "tbQueue";
            this.tbQueue.Size = new System.Drawing.Size(457, 22);
            this.tbQueue.TabIndex = 1;
            this.tbQueue.TextChanged += new System.EventHandler(this.tbQueue_TextChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(118, 75);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(255, 36);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // ClientConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 141);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbQueue);
            this.Controls.Add(this.label1);
            this.Name = "ClientConnectForm";
            this.Text = "ClientConnectForm";
            this.Load += new System.EventHandler(this.ClientConnectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbQueue;
        private System.Windows.Forms.Button btnConnect;
    }
}