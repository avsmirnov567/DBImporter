namespace Server
{
    partial class ServerForm
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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(13, 12);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.ReadOnly = true;
            this.tbInput.Size = new System.Drawing.Size(527, 255);
            this.tbInput.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(371, 283);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(167, 27);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Очистить базу данных";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 322);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tbInput);
            this.Name = "ServerForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button btnClear;
    }
}

