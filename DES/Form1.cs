using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
namespace DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void Encrypt(string source, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            if (comboBox1.Text == "ECB") desCryptoProvider.Mode = CipherMode.ECB;
            else if (comboBox1.Text == "CBC") desCryptoProvider.Mode = CipherMode.CBC;
            else if (comboBox1.Text == "CFB") desCryptoProvider.Mode = CipherMode.CFB;
            else { MessageBox.Show("Pasirinkite Modą"); }
            byteBuff = Encoding.UTF8.GetBytes(source);

            string encoded = Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            richTextBox1.Invoke((MethodInvoker)(() => richTextBox1.Text = (encoded)));
            using (var writer = File.AppendText("ENCRYPTED.txt"))
            {

                writer.WriteLine(encoded);

            }
            
        }
        public void Decrypt(string encodedText, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            if(comboBox1.Text == "ECB") desCryptoProvider.Mode = CipherMode.ECB;
                else if (comboBox1.Text == "CBC") desCryptoProvider.Mode = CipherMode.CBC;
                    else if(comboBox1.Text == "CFB") desCryptoProvider.Mode = CipherMode.CFB;
                        else { MessageBox.Show("Pasirinkite Modą");  }
            byteBuff = Convert.FromBase64String(encodedText);

            string plaintext = Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            richTextBox2.Invoke((MethodInvoker)(() => richTextBox2.Text = (plaintext)));
            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")comboBox1.Text = "ECB";
            Encrypt(textBox1.Text, textBox3.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") comboBox1.Text = "ECB";
            Decrypt(textBox2.Text, textBox4.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Lukas\\source\\repos\\DES\\DES\\bin\\Debug\\ENCRYPTED.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                richTextBox3.Invoke((MethodInvoker)(() => richTextBox3.AppendText(lines[i])));
                richTextBox3.Invoke((MethodInvoker)(() => richTextBox3.AppendText("\n")));
            }
        }
    }
}
