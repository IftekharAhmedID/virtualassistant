using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;


namespace speech_recognition_1._5
{
    public partial class Form2 : Form
    {

        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

        SpeechSynthesizer reader;



        public Form2()
        {
            InitializeComponent();
        }


        SpeechRecognizer sRecognize = new SpeechRecognizer();

        private void Form2_Load(object sender, EventArgs e)
        {
            sRecognize.SpeechRecognized += sRecognize_SpeechRecognized;
            reader = new SpeechSynthesizer();   ////time reader
            reader.SelectVoiceByHints(VoiceGender.Female);




            Choices commands = new Choices();
            commands.Add(new string[] { "Open Virtual Assistant" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammer = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammer);
            recEngine.SetInputToDefaultAudioDevice();
        }

        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.AppendText(e.Result.Text.ToString());

        }

        private async void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Open Virtual Assistant":

                    
                    if(status.Text=="status")

                    {

                        Form1 f1 = new Form1();
                        f1.Show();
                        status.Text = "aaa";

                    }

                    else

                    {


                    }
                    


                  



                    break;



            }

        }




        private void timer1_Tick(object sender, EventArgs e)
        {


            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;


            this.Visible = false;
            timer1.Enabled = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();

            f1.Show();

            status.Text = "aaa";

            this.Visible = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
