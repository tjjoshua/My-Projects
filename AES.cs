using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


// This application takes any string then encrypts/decrypts using AES Encryption
namespace AES
{
    public partial class Form1 : Form
    {
        int buttonOffset;
        int rstBtnOffset;
        public Form1()
        {
            InitializeComponent();
            buttonOffset = this.Width - textBox1.Width - msgBtn.Width - pictureBox1.Width;
            rstBtnOffset = this.Width - pictureBox1.Width;
        }
        String cipherdata;
        byte[] cipherbyte;
        byte[] plaintext;
        byte[] plainbyte;
        byte[] key;
        SymmetricAlgorithm symmetricAlgorithm = Rijndael.Create();

        // Encryption
        private void encBtn_Click(object sender, EventArgs e)
        {
            try
            {
                cipherdata = textBox1.Text;
                plaintext = Encoding.ASCII.GetBytes(cipherdata);        // Gets an encoding for ASCII
                key = Encoding.ASCII.GetBytes("0123456789abcdef");      // characters to encode
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.Mode = CipherMode.CBC;
                symmetricAlgorithm.Padding = PaddingMode.PKCS7;
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(plaintext, 0, plaintext.Length);
                cryptoStream.Close();
                cipherbyte = memoryStream.ToArray();        // writes the stream content to a byte array
                memoryStream.Close();
                textBox2.Text = Encoding.ASCII.GetString(cipherbyte);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message is: {0}", ex.Message);
            }
        }

        // Decryption
        private void decBtn_Click(object sender, EventArgs e)
        {
            try
            {   // create memorystream and cryptostream then read the cipherbyte
                MemoryStream memoryStream = new MemoryStream(cipherbyte);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);
                cryptoStream.Read(cipherbyte, 0, cipherbyte.Length);

                // writes the stream content to a byte array
                plainbyte = memoryStream.ToArray();

                // close both streams
                cryptoStream.Close();
                memoryStream.Close();

                // Decodes a sequece of bytes (plainbytes array) into a string
                String ab = Encoding.ASCII.GetString(plainbyte, 0, plaintext.Length);
                textBox3.Text = ab;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message is: {0}", ex.Message);
            }
        }

        // Reset button Resets all fields
        private void rstBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            MessageBox.Show("Your fields have been reset successfully. Click OK to continue....");
        }


        // Textbox
        private void Form1_Resize(object sender, EventArgs e)
        {            
            textBox1.Width = this.Width - buttonOffset - msgBtn.Width - pictureBox1.Width;
            textBox2.Width = this.Width - buttonOffset - msgBtn.Width - pictureBox1.Width;
            textBox3.Width = this.Width - buttonOffset - msgBtn.Width - pictureBox1.Width;
            rstBtn.Width = this.Width - pictureBox1.Width - buttonOffset;
        }
    }
}
