using System;
using System.IO;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace New_Project_Voice
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speech;
        public Form1()
        {
            InitializeComponent();
            Form_Location();
            btn_Close.Click += (s, e) => { Close(); };
        }
        void Form_Location()
        {
            int move = 0, moveX = 0, moveY = 0;
            pnl_Top.MouseDown += (s, e) => { move = 1; moveX = e.X; moveY = e.Y; };
            pnl_Top.MouseMove += (s, e) => { if (move == 1) SetDesktopLocation(MousePosition.X - moveX, MousePosition.Y - moveY); };
            pnl_Top.MouseUp += (s, e) => { move = 0; };
        }

        void Btn_start_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text)){

                speech = new SpeechSynthesizer();
                speech.Rate = trackBar1.Value;
                speech.SpeakAsync(textBox1.Text);
            }
            else
                MessageBox.Show("Напиши что-нибудь!", "Уведомление системы!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void Btn_pause_Click(object sender, EventArgs e)
        {
            speech.Pause();
        }

        void Btn_resume_Click(object sender, EventArgs e)
        {
            speech.Resume();
        }

        void Btn_openfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "text files|*.txt"
            };
            openFile.ShowDialog();
            string filename = openFile.FileName;
            StreamReader stream = new StreamReader(filename);
            textBox1.Text = stream.ReadToEnd();
            stream.Close();
        }

        void Btn_Record_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "Wave Files| *.wav"
            };
            saveFile.ShowDialog();
            string filename;
            filename = saveFile.FileName;
            speech = new SpeechSynthesizer();
            speech.Rate = trackBar1.Value;
            speech.SetOutputToWaveFile(filename);
            speech.Speak(textBox1.Text);
            speech.SetOutputToDefaultAudioDevice();
            MessageBox.Show("Аудиофайл успешно сохранен!");
        }
    }
}
