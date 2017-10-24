using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace DDSReaderSharpTest
{
    public partial class Form1 : Form
    {
        string[] Files = Directory.GetFiles(@"..\..\..\DDS File Examples\", "*.dds", SearchOption.AllDirectories);

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < Files.Length; i++)
                listBox1.Items.Add(Path.GetFileName(Files[i]));
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DdsFile = Files[listBox1.SelectedIndex];
            if (File.Exists(DdsFile))
                pictureBox1.Image = DDSReaderSharp.ToBitmap(File.ReadAllBytes(DdsFile));
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                e.Effect = DragDropEffects.Move;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && e.Effect == DragDropEffects.Move)
            {
                string[] Files = (string[])e.Data.GetData(DataFormats.FileDrop);
                pictureBox1.Image = DDSReaderSharp.ToBitmap(File.ReadAllBytes(Files[0]));
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void button_RandBG_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            pictureBox1.BackColor = Color.FromArgb(R.Next(256), R.Next(256), R.Next(256));
        }
    }
}
