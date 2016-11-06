using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Messaging;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

namespace DBImporterClient
{
    public partial class ClientForm : Form
    {
        private Thread QueueTread;

        public ClientForm()
        {
            InitializeComponent();
        }



        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV files | *.csv";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                string path = dialog.FileName;
                try
                {
                    string result = CSVReader.ReadCSV(path);
                    loadedDataTextBox.Text = result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {

        }

        private void serverPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (serverPathTextBox.Text.Length > 0)
                connectBtn.Enabled = true;
            else
                connectBtn.Enabled = false;
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageQueue.Exists(serverPathTextBox.Text))
                    ServerConnectionHandler.SendLocalQueuePathToServerQueue(serverPathTextBox.Text);
                else
                    MessageBox.Show("Указан неверный путь к очереди сервера, либо очередь не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
