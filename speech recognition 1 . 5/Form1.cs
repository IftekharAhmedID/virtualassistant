using AForge.Video;
using AForge.Video.DirectShow;
using Gmail;
using Microsoft.Win32;
using System;
using System.Data;
using System.Drawing;
using System.IO;         ////time reader
using System.Linq;
using System.Media;
using System.Net;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;





namespace speech_recognition_1._5
{
    public partial class Form1 : Form
    {

     


        RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);


        SpeechRecognizer sRecognize = new SpeechRecognizer();

        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        
        SpeechSynthesizer reader;

        PromptBuilder pb = new PromptBuilder();


        Choices commands;


        public Form1()
        {

            reg.SetValue("My app", Application.ExecutablePath.ToString());
            InitializeComponent();
           

        }

        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;

        string[] files, paths;


      

        


        private void Form1_Load(object sender, EventArgs e)
        {

            if (File.Exists("data.xml"))                         //When the application starts
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);
                msgurlTxt.Text = info.Data1;
                textBox17.Text = info.Data2;
                textBox18.Text = info.Data3;
                listBox1.Text = info.Data4;


            }

            else
            {

                
            }

            // 

          

            //

            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in webcam)
            {

                comboBox1.Items.Add(VideoCaptureDevice.Name);


            }
            comboBox1.SelectedIndex = 0;




            if (Directory.Exists("D:\\"))
            {

                picLocation.Text = "D:\\";


            }

            else if (Directory.Exists("E:\\"))
            {

                picLocation.Text = "E:\\";


            }

            else if (Directory.Exists("F:\\"))
            {

                picLocation.Text = "F:\\";


            }

            else if (Directory.Exists("G:\\"))
            {

                picLocation.Text = "G:\\";


            }

            else if (Directory.Exists("H:\\"))
            {

                picLocation.Text = "H:\\";


            }







            SoundPlayer snd1 = new SoundPlayer(speech_recognition_1._5.Resource1.sound_refresh);
            snd1.Play();


            timer2.Enabled = true;
            //////webBrowser1.Navigate("https://en.wikipedia.org/wiki/" + (richTextBox1.Text));

            label24.Text = dateTimePicker1.Text;

            sRecognize.SpeechRecognized += sRecognize_SpeechRecognized;
            reader = new SpeechSynthesizer();   ////time reader
            reader.SelectVoiceByHints(VoiceGender.Female);


            




        }

        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.AppendText(e.Result.Text.ToString());

        }



        private void btnEnable_Click(object sender, EventArgs e)
        {

          


           

        }

        private async void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)             // Speech recognigation Engine
        {
            switch (e.Result.Text.ToString())                                
            {
                case "learn mears":
                    System.Diagnostics.Process.Start("chrome", "http://mearssoft.wix.com/site");
                    break;

                case "close virtual assistant":                      //// The procedure for each command


                    reader.SpeakAsync("Bye sir,take care");

                    await Task.Delay(3000);

                    Application.Exit();

                    break;


                case "pause everything":

                    reader.Pause();
                    axWindowsMediaPlayer1.Ctlcontrols.pause();


                    break;

               

                case " play ":

                    reader.Resume();
                   

                    break;


                case "save all information":


                    Information info = new Information();
                    info.Data1 = msgurlTxt.Text;
                    info.Data2 = textBox17.Text;
                    info.Data3 = textBox18.Text;
                    info.Data4 = listBox1.Items.ToString();
                    SaveXml.SaveData(info, "data.xml");

                    reader.SpeakAsync(" all information saved");

                    break;


                case " pause music ":

                    axWindowsMediaPlayer1.Ctlcontrols.pause();

                    break;

                

                case "play music":

                    

                    try

                    {

                        if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                        {
                            tabControl1.SelectedTab = Music;
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                            reader.SpeakAsync("played song is : " + listBox1.Text);
                        }
                        else
                        {
                            tabControl1.SelectedTab = Music;
                            int selectedIndex = listBox1.SelectedIndex;
                            listBox1.SelectedIndex = selectedIndex + 1;
                            reader.SpeakAsync("played song is : " + listBox1.Text);
                        }


                    }

                        catch
                        {

                        reader.SpeakAsync(" Sir, you have to enter music before hearing . You should need help from others . ");
                        
                        }

                 


                    break;

                case "play previous music ":

                    try
                    {

                        tabControl1.SelectedTab = Music;
                        int selectedIndex = listBox1.SelectedIndex;
                        listBox1.SelectedIndex = selectedIndex - 1;
                        reader.SpeakAsync("played song is : " + listBox1.Text);
                   
 }

                    catch
                    {
                        tabControl1.SelectedTab = Music;
                        reader.SpeakAsync("Sir, before hearing music you have to select musics from your pc first . You should need help of others");

                    }

                    break;


                case "play past music":


                    try
                    {

                        tabControl1.SelectedTab = Music;
                        int selectedIndex = listBox1.SelectedIndex;
                        listBox1.SelectedIndex = selectedIndex - 1;
                        reader.SpeakAsync("played song is : " + listBox1.Text);

                    }

                    catch
                    {
                        tabControl1.SelectedTab = Music;
                        reader.SpeakAsync("Sir, before hearing music you have to select musics from your pc first . You should need help of others");

                    }

                    break;

                case "fast forward music":

                    if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    {
                        tabControl1.SelectedTab = Music;
                        axWindowsMediaPlayer1.Ctlcontrols.fastForward();
                       
                    }
                    else
                    {
                        
                        reader.SpeakAsync(" music isn't played");
                    }

                    break;



                case "play next music":
                    try
                    {
                        tabControl1.SelectedTab = Music;
                        int selectedIndex = listBox1.SelectedIndex;
                        listBox1.SelectedIndex = selectedIndex + 1;
                        reader.SpeakAsync("played song is : " + listBox1.Text);
                    }

                    catch
                    {
                        tabControl1.SelectedTab = Music;
                        reader.SpeakAsync("Sir, before hearing music you have to select musics from your pc first . You should need help of others");

                    }


                    break;

             


                case " how are you mears ":

                    reader.SpeakAsync("As i am a robot , I am always fine.");


                    break;


                case "Add to phone contact":


                    tabControl1.SelectedTab = Message;
                    textBox18.Text = "880" + textBox1.Text;

                    reader.SpeakAsync(textBox18.Text + " added to your phone contact . ");

                    


                    break;


                case "Add to email contact":



                    tabControl1.SelectedTab = Message;
                    textBox17.Text = richTextBox1.Text + "@gmail.com";

                    reader.SpeakAsync(textBox17.Text + " added to your email contact . ");

                   
                    break;



                case "read received message":


                    tabControl1.SelectedTab = Message;

                    reader.SpeakAsync(" your arrived message is : "+ msgtxtRead.Text);


                    break;


                case "I love u mears":

                    reader.SpeakAsync(" Hei.. I dont love you . I only love tonoy  ");

                    break;


               



                case "space":

                    richTextBox1.Text += " ";
                    reader.SpeakAsync("space added");

                    break;


                case "undo recent command":

                    richTextBox1.Undo();
                    textBox1.Undo();
                    textBox2.Undo();
                    textBox9.Undo();
                    reader.SpeakAsync("undo done");

                    break;






                case "news number 1":


                    tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header1.Text+ " . " + news1.Text);
                    newstab.SelectedTab = tabPage1;
                    
                    break;

                case "news number 2":


          tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header2.Text + " . " + news2.Text);
                    newstab.SelectedTab = tabPage2;

                    break;
                case "news number 3":


             tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header3.Text + " . " + news3.Text);
                    newstab.SelectedTab = tabPage3;

                    break;
                case "news number 4":


                   tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header4.Text + " . " + news4.Text);
                    newstab.SelectedTab = tabPage4;

                    break;
                case "news number 5":


                 tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header5.Text + " . " + news5.Text);
                    newstab.SelectedTab = tabPage5;

                    break;
                case "news number 6":


          //   tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header6.Text + " . " + news6.Text);
                    newstab.SelectedTab = tabPage6;

                    break;
                case "news number 7":


            //   tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header7.Text + " . " + news7.Text);
                    newstab.SelectedTab = tabPage7;

                    break;
                case "news number 8":


          //          tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header8.Text + " . " + news8.Text);
                    newstab.SelectedTab = tabPage8;

                    break;
                case "news number 9":


              //      tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header9.Text + " . " + news9.Text);
                    newstab.SelectedTab = tabPage9;

                    break;
                case "news number 10":


               //     tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header10.Text + " . " + news10.Text);
                    newstab.SelectedTab = tabPage10;

                    break;
                case "news number 11":


                //    tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header11.Text + " . " + news11.Text);
                    newstab.SelectedTab = tabPage11;

                    break;
                case "news number 12":


          //          tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();

                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header12.Text + " . " + news12.Text);
                    newstab.SelectedTab = tabPage12;

                    break;
                case "news number 13":


             //       tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header13.Text + " . " + news13.Text);
                    newstab.SelectedTab = tabPage13;

                    break;
                case "news number 14":


           //         tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header14.Text + " . " + news14.Text);
                    newstab.SelectedTab = tabPage14;

                    break;
                case "news number 15":


              //      tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header15.Text + " . " + news15.Text);
                    newstab.SelectedTab = tabPage15;

                    break;
                case "news number 16":


             //       tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header16.Text + " . " + news16.Text);
                    newstab.SelectedTab = tabPage16;

                    break;
                case "news number 17":


            //        tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header17.Text + " . " + news17.Text);
                    newstab.SelectedTab = tabPage17;

                    break;
                case "news number 18":


               //     tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header18.Text + " . " + news18.Text);
                    newstab.SelectedTab = tabPage18;

                    break;
                case "news number 19":


             //       tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header19.Text + " . " + news19.Text);
                    newstab.SelectedTab = tabPage19;

                    break;
                case "news number 20":


              //      tabControl1.SelectedTab = news;
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(header20.Text + " . " + news20.Text);
                    newstab.SelectedTab = tabPage20;

                    break;
             




                case "todays news update":




                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Wellcome at Mears News update.  I am telling summeries. for detail ask me with serial number. " + ". #1: " + header1.Text + ". #2: " + header2.Text + ". #3: " + header3.Text + ". #4: " + header4.Text + ". #5: " + header5.Text + ". #6: " + header6.Text + ". #7: " + header7.Text + ". #8: " + header8.Text + ". #9: " + header9.Text + ". #10: " + header10.Text + ". # 11: " + header11.Text + ". #12: " + header12.Text + ". #13: " + header13.Text + ". #14: " + header14.Text + ". #15: " + header15.Text + ". #16: " + header16.Text + ". #17: " + header17.Text + ". #18: " + header18.Text + ". #19: " + header19.Text + ". #20: " + header20.Text + " Thank you . ");


                    tabControl1.SelectedTab = news;


                    await Task.Delay(7942);
                    newstab.SelectedTab = tabPage1;
                    tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage2;
                    tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage3;
                    tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage4;
                    tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage5;
                    tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage6;
              //      tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage7;
            //        tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage8;
            //        tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage9;
          //          tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage10;
           //         tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage11;
        //            tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage12;
         //           tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage13;
         //           tabControl1.SelectedTab = news;
                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage14;
        //            tabControl1.SelectedTab = news;
                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage15;
         //           tabControl1.SelectedTab = news;
                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage16;
         //           tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage17;
         //           tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage18;
          //          tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage19;
         //           tabControl1.SelectedTab = news;

                    await Task.Delay(5000);
                    newstab.SelectedTab = tabPage20;
         //           tabControl1.SelectedTab = news;










                    break;



                case "thank you":

                    tabControl1.SelectedTab = Message;
                    textBox9.Text += " thank you .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("thank you");


                    break;

                case "thank you mears":


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(" Wellcome Sir. ask me anything you want. ");


                    break;



                case "restart":

                    Application.Restart();
                   

                    break;

                case "tell something about you":

                    reader.Dispose();    ////listeningggg for

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Of course. I am a fully voice operated multi tasking virtual assistant. Tonoy Ahmed, 16 years old programmer is my creator. Tonoy used C-charp programming for building me. Through me you can do everything you want. ");



                    break;




                case "how to use this":


                    reader.Dispose();    ////listeningggg for

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Wellcome at Mears. Though this software you can do everything without using your hands and eyes. If you want to know about the current time, say. What is the current time. for date , say. What is the current date. for weather, say. What is the current weather. For location, say. What is the current location. If you want to calculate any big sum. Hei... listen carefully. first say each digit of your number. then say. second veriable. and say each digit of your second number . then say . plus or minus or add into add or multiply to get the result . If you want to know about the meaning of any word then say each alphabet of the word and say. search. i will told everything what you want . ");






                    break;





                case "call me":
                    tabControl1.SelectedTab = Message;
                    textBox9.Text += " call me ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(" call me ");


                    break;

                case "Hello":
                    tabControl1.SelectedTab = Message;
                    textBox9.Text += " hello, ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("hello input");


                    break;



                case "come here":

                    tabControl1.SelectedTab = Message;
                    textBox9.Text += " come here. ";

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("come here input");


                    break;

                case "What are doing now?":

                    tabControl1.SelectedTab = Message;
                    textBox9.Text += " What are you doing? ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("What are you doing input");


                    break;


                case "stop":
                    reader.Dispose();


                    SoundPlayer snd312 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    snd312.Play();
                   
                    snd312.Stop();
                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Everything stoped");


                    alrmStmr.Enabled = false;

                    axWindowsMediaPlayer1.Ctlcontrols.pause();

                    break;


                case "What is the current location":


                    tabControl1.SelectedTab = Location;

                    SoundPlayer snd3 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    snd3.Play();

                    textBox4.Text = "Your current location ," + label32.Text + " ,and " + label34.Text + " ,and " + label37.Text + " ,and " + label38.Text + " ,and " + label35.Text + " ,and " + label36.Text + orgLbl.Text;

                    reader.Dispose();    ////listeningggg for

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(textBox4.Text);
                    



                    break;








                case "What are you doing":



                    tabControl1.SelectedTab = Message;
                    textBox9.Text += " What are you doing? ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("What are you doing? input");



                    break;
                case "Please call me":



                    tabControl1.SelectedTab = Message;


                    textBox9.Text += " Please call me!! ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(" Please call me!! input ");




                    break;
                case "mail your phone number":

                    tabControl1.SelectedTab = Message;


                    textBox9.Text += " mail your phone number . ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("mail your phone number .input");



                    break;

                case "What is the current date":

                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp.Play();




                    reader.Dispose();

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Todays Date is :" + label24.Text);


                    break;







                case "Hello Mears":


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Hi Sir. I am your assistant mears");


                    break;

                case "Its an urgent":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " Its an urgent .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Its an urgent. input");


                    break;

                case "today":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " today ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("today input");


                    break;

                case "i feel sick":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += "i feel sick .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Its an urgent. input");


                    break;
                case "yestarday":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " yestarday ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("yestarday input");


                    break;
                case "tomorrow":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " tomorrow ";


                   
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("tomorrow input");


                    break;


                case "i am in a meeting":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " i am in a meeting .";


                    
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("i am in a meeting input");


                    break;
                case "i am hungry":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " i am hungry .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("i am hungry input");


                    break;
                case "persel some food for me":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " persel some food for me .";


                  
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("persel some food for me input");


                    break;
                case "but":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " but ";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("but input");


                    break;
                case "give me your phone number":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " give me your phone number .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("give me your phone number input");


                    break;
                case "i lost your phone number":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " i lost your phone number .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("i lost your phone number input");


                    break;
                case "help me":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " help me .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("help me input");


                    break;
                case "i am in a trouble":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " i am in a trouble .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("i am in a trouble input");


                    break;
                case "call me now":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += "call me now";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("call me now input");


                    break;                              
                    break;

                case "Meet with you tomorrow":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " Meet with you tomorrow .";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Meet with you tomorrow .input");




                    break;




                case "add in gmail":



                    tabControl1.SelectedTab = Message;

                    textBox17.Text = richTextBox1.Text + "@gmail.com";

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(textBox17.Text+" added to your mail");


                    break;




                case "search":

                    textBox6.Text = richTextBox1.Text;

                    tabControl1.SelectedTab = Dictionary;




                    textBox4.Text = "Sir. You searched :" + richTextBox1.Text;
                    reader.Dispose();

                    SoundPlayer snd2 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    snd2.Play();

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(textBox4.Text);

                    webBrowser1.Navigate("https://en.wikipedia.org/wiki/" + (richTextBox1.Text));


                    dicTimer.Enabled = true;

                    timer15.Enabled = true;





                    break;

                case "Open voice dictionary":

                    textBox6.Text = richTextBox1.Text;

                    tabControl1.SelectedTab = Dictionary;




                    textBox4.Text = "Sir. You searched :" + richTextBox1.Text + "in mears dictionary.";
                    reader.Dispose();

                    SoundPlayer snd112 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    snd112.Play();

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(textBox4.Text);

                    webBrowser1.Navigate("https://en.wikipedia.org/wiki/" + (richTextBox1.Text));


                    dicTimer.Enabled = true;

                    timer15.Enabled = true;





                    break;

                case "what is the current time":

                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp3 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp3.Play();


                    reader.Dispose();

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Now it is, " + label1.Text);




                    break;
                case "second veriable":

                    try

                    {

                        textBox2.Text = "";

                        label2.Text = textBox1.Text;
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync(label2.Text + " you input");
                    }

                    catch
                    {
                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync("Sorry sir . You dont input any first veriable .If you dont know how to sum , then ask me that learn about sum.");

                    }


                    break;


                case "How are you":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += "How are you?";

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("How are you? input");



                    break;


                case "Refresh all":



                    richTextBox1.Text = "";

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox7.Text = "";
                    textBox6.Text = "";
                    textBox9.Text = "";


                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("Everything has refreshed.");




                    break;




                case "greeting for you":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " greeting for you. ";

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("greeting for you. input");


                    break;


                case "send sms":

                    tabControl1.SelectedTab = Message;

                    try
                    {

                       

                        WebClient client = new WebClient();
                        string to, msg;
                        to = textBox16.Text;
                        msg = textBox9.Text;
                        string baseurl = " http://api.clickatell.com/http/sendmsg?user=Tonoy001&password=KOXRRdRfKOZDOC&api_id=3626023&to='" + to + "'&text='" + msg + "'";
                        client.OpenRead(baseurl);

                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync("sms send to " + textBox16.Text);

                    }
                    catch 
                    {
                        



                    }



                    break;
                    
                case "send sms to current number":

                    SoundPlayer asp55 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp55.Play();

                    try
                    {
                        textBox9.Text = textBox19.Text;

                        tabControl1.SelectedTab = Message;

                        WebClient client = new WebClient();
                        string to, msg;
                        to = textBox18.Text;
                        msg = textBox9.Text;
                        string baseurl = " http://api.clickatell.com/http/sendmsg?user=Tonoy001&password=KOXRRdRfKOZDOC&api_id=3626023&to='" + to + "'&text='" + msg + "'";
                        client.OpenRead(baseurl);


                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync("sms send to " + textBox18.Text);

                    }
                    catch 
                    {




                    }


                    break;




                case "send sms to 4":

                    SoundPlayer asp23 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp23.Play();


                    try
                    {

                        tabControl1.SelectedTab = Message;

                        WebClient client = new WebClient();
                        string to, msg;
                        to = textBox18.Text;
                        msg = textBox9.Text;
                        string baseurl = " http://api.clickatell.com/http/sendmsg?user=Tonoy001&password=KOXRRdRfKOZDOC&api_id=3626023&to='" + to + "'&text='" + msg + "'";
                        client.OpenRead(baseurl);
                        MessageBox.Show("Successfully sms Send to" + textBox1.Text);
                    }
                    catch (Exception ex)
                    {


                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync(ex.Message);



                    }




                    break;

                case "plus":
                    try

                    {

                        tabControl1.SelectedTab = Calculator;


                        SoundPlayer asp54 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                        asp54.Play();




                        textBox3.Text = Convert.ToString(Convert.ToDouble(label2.Text) + Convert.ToDouble(textBox2.Text));



                        label14.Text = "+";
                        label15.Text = textBox2.Text;
                        label7.Text = textBox3.Text;
                        label6.Text = "=";






                        reader.Dispose();    ////listeningggg for

                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync(label2.Text + " plus " + label15.Text + label6.Text + label7.Text);

                    }

                    catch
                    {
                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync("Sorry sir. You dont input any first and second veriable. If you dont know how to sum , then ask me that. learn about sum.");

                    }


                    break;


                case "Percentage veriables":

                    try

                    {

                        tabControl1.SelectedTab = Calculator;


                        SoundPlayer asp54 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                        asp54.Play();




                        textBox3.Text = Convert.ToString(Convert.ToDouble(label2.Text) * Convert.ToDouble(textBox2.Text) / Convert.ToDouble("100"));



                        label14.Text = "%";
                        label15.Text = textBox2.Text;
                        label7.Text = textBox3.Text;
                        label6.Text = "=";






                        reader.Dispose();    ////listeningggg for

                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync(label2.Text + " and its " + label15.Text + " persent is " + label7.Text);

                    }

                    catch
                    {
                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync("Sorry sir. You dont input any first and second veriable. If you dont know how to sum , then ask me that. learn about sum.");

                    }

                    break;



                case "minus":
                    try

                    {
                        tabControl1.SelectedTab = Calculator;


                        SoundPlayer asp15 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                        asp15.Play();




                        textBox3.Text = Convert.ToString(Convert.ToDouble(label2.Text) - Convert.ToDouble(textBox2.Text));


                        label14.Text = "-";
                        label15.Text = textBox2.Text;
                        label7.Text = textBox3.Text;
                        label6.Text = "=";

                        reader.Dispose();

                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync(label2.Text + " minus " + label15.Text + label6.Text + label7.Text);


                    }

                    catch
                    {
                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync("Sorry sir. You dont input any first and second veriable. If you dont know how to sum , then ask me that. learn about sum.");

                    }

                    break;


                case "multiply":
                    try
                    {
                        tabControl1.SelectedTab = Calculator;


                        SoundPlayer asp93 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                        asp93.Play();

                        textBox3.Text = Convert.ToString(Convert.ToDouble(label2.Text) * Convert.ToDouble(textBox2.Text));


                        label14.Text = "X";
                        label15.Text = textBox2.Text;
                        label7.Text = textBox3.Text;
                        label6.Text = "=";


                        reader.Dispose();

                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync(label2.Text + " Into " + label15.Text + label6.Text + label7.Text);
                    }

                    catch
                    {
                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync("Sorry sir. You dont input any first and second veriable. If you dont know how to sum , then ask me that. learn about sum.");

                    }


                    break;

                case "Divition":
                    try

                    {
                        tabControl1.SelectedTab = Calculator;

                        SoundPlayer asp45 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                        asp45.Play();

                        textBox3.Text = Convert.ToString(Convert.ToDouble(label2.Text) / Convert.ToDouble(textBox2.Text));



                        label14.Text = "÷";
                        label15.Text = textBox2.Text;
                        label7.Text = textBox3.Text;
                        label6.Text = "=";


                        reader.Dispose();

                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync(label2.Text + " Divite " + label15.Text + label6.Text + label7.Text);
                    }

                    catch
                    {
                        reader = new SpeechSynthesizer();
                        reader.SelectVoiceByHints(VoiceGender.Female);
                        reader.SpeakAsync("Sorry sir. You dont input any first and second veriable. If you dont know how to sum , then ask me that. learn about sum.");

                    }

                    break;

                case "open chrome":



                    break;


                case "reading mode":

                    System.Diagnostics.Process.Start("chrome", "https://www.youtube.com/watch?v=ycBJywyqZM8");

                    break;

                case "three ":
                    reader.Dispose();
                    if (textBox3.Text != "")
                        reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(textBox3.Text);
                    break;

                case "wake me up in 30 second":
                
                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp52274 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp52274.Play();

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("alarm after 30 second set");

                    
                    tmr30sec.Enabled = true;
                    

                    break;
                case "wake me up in 10 second":


                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp5227422 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp5227422.Play();

                    reader = new SpeechSynthesizer();
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync("alarm after 10 second set");


                    check10sec.Checked = true;


                    tmr10sec.Enabled = true;



                    break;

                case "wake me up in 1 hour":


                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp5227411 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp5227411.Play();

              
                    reader.SpeakAsync("alarm after 1 hour set");


                    check1hr.Checked = true;


                    tmr1hr.Enabled = true;


                    break;



                case "wake me up in 3 hour":

                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp112 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp112.Play();

                    
                    reader.SpeakAsync("alarm after 3 hour set");


                    check3hr.Checked = true;


                    tmr3hr.Enabled = true;

                    


                    break;




                case "wake me up in 2 hour":


                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp52 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp52.Play();

                 
                    reader.SpeakAsync("alarm after 2 hour set");


                    check2hr.Checked = true;


                    tmr2hr.Enabled = true;

                    break;

                case "I am busy now":

                    tabControl1.SelectedTab = Message;

                    textBox9.Text += " I am busy now .";

                   
                    reader.SpeakAsync(" I am busy now .");
                    break;



                case "wake me up in 6 hour":


                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp666 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp666.Play();

                   
                    reader.SpeakAsync("alarm after 6 hour set");


                    check6hr.Checked = true;


                    tmr6hr.Enabled = true;


                    break;


                case "wake me up in 9 hour":



                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp5111 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp5111.Play();

                  
                    reader.SpeakAsync("alarm after 9 hour set");


                    check3hr.Checked = true;


                    tmr3hr.Enabled = true;


                    break;


                case "wake me up in 7 hour":



                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp522411 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp522411.Play();

                    tmr7hr.Enabled = true;

                    
                  
                    reader.SpeakAsync("alarm after 7 hour set");

                    check7hr.Checked = true;


                    break;

                case "wake me up in 4 hour":



                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp52241111 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp52241111.Play();

                    tmr4hr.Enabled = true;


                   
                    reader.SpeakAsync("alarm for 4 hour set");

                    check4hr.Checked = true;


                    break;

                case "wake me up in 5 hour":



                    tabControl1.SelectedTab = Clock;

                    SoundPlayer asp5224121 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp5224121.Play();

                    tmr5hr.Enabled = true;


                   
                    reader.SpeakAsync("alarm for 5 hour set");

                    check5hr.Checked = true;


                    break;
                case "wake me up in 8 hour":



                    tabControl1.SelectedTab = Clock;


                    SoundPlayer asp5478 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp5478.Play();

                    tmr8hr.Enabled = true;
                    
                   
                    reader.SpeakAsync("alarm after 8 hour set");

                    check8hr.Checked = true;


                    break;





                case "a":
                    richTextBox1.Text += "a";
                    reader.SpeakAsync("a input");


                    // reader = new SpeechSynthesizer();


                    break;

                case "b":
                    richTextBox1.Text += "b";
                    reader.SpeakAsync("b input");


                    break;
                case "c":
                    richTextBox1.Text += "c";
                    reader.SpeakAsync("c input");


                    break;

                case "d":
                    richTextBox1.Text += "d";
                    reader.SpeakAsync("d input");


                    break;


                case "e":
                    richTextBox1.Text += "e";
                    reader.SpeakAsync("e input");

                    break;


                case "f":
                    richTextBox1.Text += "f";
                    reader.SpeakAsync("f input");
                    break;


                case "g":

                    richTextBox1.Text += "g";
                    reader.SpeakAsync("g input");

                    break;


                case "h":
                    richTextBox1.Text += "h";
                    reader.SpeakAsync("h input");

                    break;

                case "i":
                    richTextBox1.Text += "i";
                    reader.SpeakAsync("i input");
                    break;

                case "j":
                    richTextBox1.Text += "j";
                    reader.SpeakAsync("j input");
                    break;
                case "k":
                    richTextBox1.Text += "k";
                    reader.SpeakAsync("k input");

                    break;
                case "l":
                    richTextBox1.Text += "l";
                    reader.SpeakAsync("l input");

                    break;
                case "m":
                    richTextBox1.Text += "m";
                    reader.SpeakAsync("m input");

                    break;
                case "n":
                    richTextBox1.Text += "n";
                    reader.SpeakAsync("n input");

                    break;
                case "o":
                    richTextBox1.Text += "o";
                    reader.SpeakAsync("o input");

                    break;

                case "p":
                    richTextBox1.Text += "p";
                    reader.SpeakAsync("p input");

                    break;
                case "q":
                    richTextBox1.Text += "q";
                    reader.SpeakAsync("q input");
                    break;
                case "r":
                    richTextBox1.Text += "r";
                    reader.SpeakAsync("r input");
                    break;
                case "s":
                    richTextBox1.Text += "s";
                    reader.SpeakAsync(" ");                    //////s changed as it works eco...

                    break;
                case "t":
                    richTextBox1.Text += "t";
                    reader.SpeakAsync("t input");
                    break;
                case "u":
                    richTextBox1.Text += "u";
                    reader.SpeakAsync("u input");

                    break;
                case "v":
                    richTextBox1.Text += "v";
                    reader.SpeakAsync("v input");


                    break;
                case "w":
                    richTextBox1.Text += "w";
                    reader.SpeakAsync("w input");

                    break;
                case "x":
                    richTextBox1.Text += "x";
                    reader.SpeakAsync("x input");

                    break;
                case "y":
                    richTextBox1.Text += "y";
                    reader.SpeakAsync("y input");

                    break;
                case "z":
                    richTextBox1.Text += "z";
                    reader.SpeakAsync("z input");


                    break;

                case "one":
                    tabControl1.SelectedTab = Calculator;


                    textBox1.Text += "1";
                    textBox2.Text += "1";
                    richTextBox1.Text += "1";
                    textBox9.Text += "1";
                    reader.SpeakAsync("1 input");



                    break;

                case "two":
                    tabControl1.SelectedTab = Calculator;


                    textBox1.Text += "2";
                    textBox2.Text += "2";
                    richTextBox1.Text += "2";
                    textBox9.Text += "2";


                    reader.SpeakAsync("2 input");


                    break;

                case "three":

                    tabControl1.SelectedTab = Calculator;
                    textBox1.Text += "3";
                    textBox2.Text += "3";
                    richTextBox1.Text += "3";
                    textBox9.Text += "3";

                    reader.SpeakAsync("3 input");
                    break;

                case "four":
                    tabControl1.SelectedTab = Calculator;

                    textBox1.Text += "4";
                    textBox2.Text += "4";
                    richTextBox1.Text += "4";
                    textBox9.Text += "4";


                    reader.SpeakAsync("4 input");
                    break;





                case "five":
                    tabControl1.SelectedTab = Calculator;

                    textBox1.Text += "5";
                    textBox2.Text += "5";
                    richTextBox1.Text += "5";
                    textBox9.Text += "5";


                    reader.SpeakAsync("5 input");

                    break;

                case "six":
                    tabControl1.SelectedTab = Calculator;

                    textBox1.Text += "6";
                    textBox2.Text += "6";
                    richTextBox1.Text += "6";
                    textBox9.Text += "6";


                    reader.SpeakAsync("6 input");

                    break;

                case "seven":
                    tabControl1.SelectedTab = Calculator;

                    textBox1.Text += "7";
                    textBox2.Text += "7";
                    richTextBox1.Text += "7";
                    textBox9.Text += "7";



                    reader.SpeakAsync("7 input");

                    break;

                case "eight":

                    tabControl1.SelectedTab = Calculator;

                    textBox1.Text += "8";
                    textBox2.Text += "8";
                    richTextBox1.Text += "8";
                    textBox9.Text += "8";


                    reader.SpeakAsync("8 input");

                    break;

                case "nine":
                    tabControl1.SelectedTab = Calculator;


                    textBox1.Text += "9";
                    textBox2.Text += "9";
                    richTextBox1.Text += "9";
                    textBox9.Text += "9";


                    reader.SpeakAsync("9 input");


                    break;


                case "zero":
                    tabControl1.SelectedTab = Calculator;

                    textBox1.Text += "0";
                    textBox2.Text += "0";
                    richTextBox1.Text += "0";
                    textBox9.Text += "0";


                    reader.SpeakAsync("0 input");

                    break;

                case "send this email to the address":

                    try

                    {

                        tabControl1.SelectedTab = Message;

                        reader.SpeakAsync("your message :" + textBox9.Text);   

                        SoundPlayer asp540 = new SoundPlayer(speech_recognition_1._5.Resource1.sound_refresh);
                        asp540.Play();


                        GmailClient client = new GmailClient();

                        string status = client.send("icreativebd@gmail.com", "Abanglalink1", textBox8.Text, "Redirected from MEARSSOFT", textBox9.Text, null);

                      
                        reader.SpeakAsync(" your email "+status + " to " + textBox8.Text );



                    }

                    catch
                    {
                       
                        reader.SpeakAsync("Sorry sir. Email has not been send. Please check your inernet connection");

                    }

                    break;



                case "send this message to 4":

                    try
                    {


                        GmailClient client = new GmailClient();

                        string status = client.send("icreativebd@gmail.com", "Abanglalink1", textBox17.Text, "Redirected from MEARSSOFT", textBox9.Text, null);
                        MessageBox.Show(status);



                    }

                    catch
                    {
                       
                        reader.SpeakAsync("Sorry sir. Message has not been sent . Please check your balance and typed number");

                    }


                    break;



                case "What is the current weather":

                    tabControl1.SelectedTab = Weather;

                    SoundPlayer asp546 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                    asp546.Play();

                    textBox4.Text = " Todays Current weather , " + "temperature: "+label10.Text + "and" + textBox15.Text + " and " + label4.Text + " and " + label5.Text + " and " + label31.Text + " and " + label8.Text + " and " +label9.Text;

                  

                   
                    reader.SelectVoiceByHints(VoiceGender.Female);
                    reader.SpeakAsync(textBox4.Text);


                    break;



            }
        }

       
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ///label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void timer1_Tick_2(object sender, EventArgs e)
        {
            label24.Text = dateTimePicker1.Text;
            
           


        }

        private void Lblctime_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {





            { }
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {

        }

        public EventHandler<SpeakCompletedEventArgs> reader_SpeakCompleted { get; set; }

        private void btnDisable_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
           
        }

        private void timer2_Tick(object sender, EventArgs e)


            
        {

            msgWeb.Navigate(msgurlTxt.Text);



            Choices commands = new Choices();        /// COMMAND DATABASE
            commands.Add(new string[] { "today", "I love u mears", "pause everything", "close virtual assistant", "play past music", "play previous music ", "fast forward music", " play ", " pause music ", " resume music ", "play music", "play next music", " how are you mears ", "Add to phone contact", "save all information", "Add to email contact", "read received message", "send sms to current number", "call me now", "undo recent command", "space", "i feel sick", "yestarday", "tomorrow", "i am in a meeting", "i am hungry", "persel some food for me", "but", "give me your phone number", "i lost your phone number", "help me", "i am in a trouble", "call me", "how to use this", "restart", "thank you mears", "wake me up in 4 hour", "wake me up in 5 hour", "news number 1", "news number 2", "news number 3", "news number 4", "news number 5", "news number 6", "news number 7", "news number 8", "news number 9", "news number 10", "news number 11", "news number 12", "news number 13", "news number 14", "news number 15", "news number 16", "news number 17", "news number 18", "news number 19", "news number 20", "undo command", "todays news update", "Percentage veriables", "undo calculator", "thank you", "Hello Mears", "Divition", "multiply", "tell something about you", "wake me up in 10 second", "call me", "send this email to the address", "send sms to current address", "come here", "What are doing now?", "What is the current date", "stop", "What is the current location", "What are you doing", "Please call me", "mail your phone number", "Hello", "Its an urgent", "Meet with you tomorrow", "add in gmail", "nine", "send sms", "send sms to 4", "send this message to 4", "Copy number to message box", "search", "Refresh all", "Open voice dictionary", "zero", "send this message to first person", "send this message to second  person", "send this message to Free online book", "How are you", "greeting for you", "I am busy now", "meet with you tomorrow", "wake me up in 9 hour", "okey", "wake me up in 5 hour", "wake me up in 4 hour", "wake me up in 3 hour", "wake me up in 2 hour", "wake me up in 30 second", "wake me up in 1 hour", "Help me", "What is the current weather", "second veriable", "pronounce text", "send this message to the address", "learn mears", "plus", "calculate", "minus", "one", "two", "three", "four", "five", "six", "seven", "eight", "Nine", "add into add", "multiply this", "open system browser", "open my shedule", "what is the current time", "open facebook ", "print my name", "open chrome", "wake me up in 6 hour", "wake me up in 8 hour", "search", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p" });
            Grammar grammer = new Grammar(new GrammarBuilder(commands));

            recEngine.RequestRecognizerUpdate();
            recEngine.LoadGrammar(grammer);
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);




            // EngineEnable.PerformClick();

            msgTimer.Enabled = true;


            tabControl1.SelectedTab = Home;

            webBrowser2.Document.ExecCommand("SelectAll", false, null);    ////weather brief sentance  er jonno
            webBrowser2.Document.ExecCommand("Copy", false, null);

            textBox22.Paste();
            textBox22.Lines = textBox22.Lines.Where(line => line.Contains("Wind")).ToArray();


            try
            {
                textBox15.Text = textBox22.Lines[1];
            }
            catch
            {
                textBox15.Text = textBox22.Text;
            }
            



            cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

         cam.Start();

            richTextBox2.Focus();


            try
            {


                



                weatherWeb.Document.ExecCommand("SelectAll", false, null);    ////weather Teller er jonno
                weatherWeb.Document.ExecCommand("Copy", false, null);


                textBox5.Paste();




                weatherWeb.Document.ExecCommand("SelectAll", false, null);    ////weather Teller er jonno
                weatherWeb.Document.ExecCommand("Copy", false, null);


                textBox5.Paste();




                label3.Text = textBox5.Lines[20];
               
                label10.Text = textBox5.Lines[13];
                label5.Text = textBox5.Lines[20];
                label8.Text = textBox5.Lines[27];
                label9.Text = textBox5.Lines[29];
                label4.Text = textBox5.Lines[31];
                label31.Text = textBox5.Lines[33];


                textBox20.Text = textBox5.Text;

                textBox4.Text = "Hi Friend, I am Mears. A Virtual Assistant .";
                reader.Dispose();    ////listeningggg for

                reader = new SpeechSynthesizer();
                reader.SelectVoiceByHints(VoiceGender.Female);
                reader.SpeakAsync(textBox4.Text);
               

               

                ////webBrowser1.Navigate("http://www.mearssoft.wix.com/site");


                //////weather
               
                
              

                
                timer2.Enabled = false;



                ////location

                locationWeb.Document.ExecCommand("SelectAll", false, null);    ////weather Teller er jonno
                locationWeb.Document.ExecCommand("Copy", false, null);


                textBox12.Paste();





                latTxt.Text = textBox12.Text;
                lonTxt.Text = textBox12.Text;
                cntryTxt.Text = textBox12.Text;
                ctyTxt.Text = textBox12.Text;
                rgnTxt.Text = textBox12.Text;
                orgTxt.Text = textBox12.Text;
                ipTxt.Text = textBox12.Text;


                latTxt.Lines = latTxt.Lines.Where(line => line.Contains("Latitude")).ToArray();

                latTxt.Lines = latTxt.Lines.Where(line => !line.Contains("?")).ToArray();

                lonTxt.Lines = lonTxt.Lines.Where(line => line.Contains("Longitude")).ToArray();

                lonTxt.Text = lonTxt.Text.Replace("Longitude ?", " ");

                cntryTxt.Lines = cntryTxt.Lines.Where(line => line.Contains("Country")).ToArray();
                ctyTxt.Lines = ctyTxt.Lines.Where(line => line.Contains("City")).ToArray();
                rgnTxt.Lines = rgnTxt.Lines.Where(line => line.Contains("Region")).ToArray();
                orgTxt.Lines = orgTxt.Lines.Where(line => line.Contains("Organization")).ToArray();
                ipTxt.Lines = ipTxt.Lines.Where(line => line.Contains("Public IP Address:")).ToArray();


                label32.Text = ipTxt.Text;
                label35.Text = latTxt.Text;
                label36.Text = lonTxt.Text;
                label34.Text = cntryTxt.Text;
                label37.Text = rgnTxt.Text;
                label38.Text = ctyTxt.Text;
                orgLbl.Text = orgTxt.Text;



                textBox21.Text = textBox12.Text;  ////offline er je alada textbox ase.. ta load er jonno .. 


                ///news feed



                newsWeb.Document.ExecCommand("SelectAll", false, null);    ////weather Teller er jonno
                newsWeb.Document.ExecCommand("Copy", false, null);


                newsBox.Paste();

                if (newsBox.Text.Contains("Enable news feed"))


               

                {
                    header1.Text = newsBox.Text;
                    header1.Lines = header1.Lines.Where(line => line.Contains("header1:")).ToArray();
                    news1.Text = newsBox.Text;
                    news1.Lines = news1.Lines.Where(line => line.Contains("news1:")).ToArray();
                    header1.Text = header1.Text.Replace("header1:", " ");
                    news1.Text = news1.Text.Replace("news1:", " ");



                    header2.Text = newsBox.Text;
                    news2.Text = newsBox.Text;
                    header2.Lines = header2.Lines.Where(line => line.Contains("header2:")).ToArray();

                    news2.Lines = news2.Lines.Where(line => line.Contains("news2:")).ToArray();
                    header2.Text = header2.Text.Replace("header2:", " ");
                    news2.Text = news2.Text.Replace("news2:", " ");


                    header3.Text = newsBox.Text;
                    news3.Text = newsBox.Text;
                    header3.Lines = header3.Lines.Where(line => line.Contains("header3:")).ToArray();

                    news3.Lines = news3.Lines.Where(line => line.Contains("news3:")).ToArray();
                    header3.Text = header3.Text.Replace("header1:", " ");
                    news3.Text = news3.Text.Replace("news3:", " ");



                    header4.Text = newsBox.Text;
                    news4.Text = newsBox.Text;
                    header4.Lines = header4.Lines.Where(line => line.Contains("header4:")).ToArray();
                    news4.Lines = news4.Lines.Where(line => line.Contains("news4:")).ToArray();
                    header4.Text = header4.Text.Replace("header4:", " ");
                    news4.Text = news4.Text.Replace("news4:", " ");




                    header5.Text = newsBox.Text;
                    news5.Text = newsBox.Text;
                    header5.Lines = header5.Lines.Where(line => line.Contains("header5:")).ToArray();
                    news5.Lines = news5.Lines.Where(line => line.Contains("news5:")).ToArray();
                    header5.Text = header5.Text.Replace("header5:", " ");
                    news5.Text = news5.Text.Replace("news5:", " ");





                    header6.Text = newsBox.Text;
                    news6.Text = newsBox.Text;
                    header6.Lines = header6.Lines.Where(line => line.Contains("header6:")).ToArray();
                    news6.Lines = news6.Lines.Where(line => line.Contains("news6:")).ToArray();
                    header6.Text = header6.Text.Replace("header6:", " ");
                    news6.Text = news6.Text.Replace("news6:", " ");





                    header7.Text = newsBox.Text;
                    news7.Text = newsBox.Text;
                    header7.Lines = header7.Lines.Where(line => line.Contains("header7:")).ToArray();
                    news7.Lines = news7.Lines.Where(line => line.Contains("news7:")).ToArray();
                    header7.Text = header7.Text.Replace("header7:", " ");
                    news7.Text = news7.Text.Replace("news7:", " ");





                    header8.Text = newsBox.Text;
                    news8.Text = newsBox.Text;
                    header8.Lines = header8.Lines.Where(line => line.Contains("header8:")).ToArray();
                    news8.Lines = news8.Lines.Where(line => line.Contains("news8:")).ToArray();
                    header8.Text = header8.Text.Replace("header8:", " ");
                    news8.Text = news8.Text.Replace("news8:", " ");





                    header9.Text = newsBox.Text;
                    news9.Text = newsBox.Text;
                    header9.Lines = header9.Lines.Where(line => line.Contains("header9:")).ToArray();
                    news9.Lines = news9.Lines.Where(line => line.Contains("news9:")).ToArray();
                    header9.Text = header9.Text.Replace("header9:", " ");
                    news9.Text = news9.Text.Replace("news9:", " ");




                    header10.Text = newsBox.Text;
                    news10.Text = newsBox.Text;
                    header10.Lines = header10.Lines.Where(line => line.Contains("header10:")).ToArray();
                    news10.Lines = news10.Lines.Where(line => line.Contains("news10:")).ToArray();
                    header10.Text = header10.Text.Replace("header10:", " ");
                    news10.Text = news10.Text.Replace("news10:", " ");





                    header11.Text = newsBox.Text;
                    news11.Text = newsBox.Text;
                    header11.Lines = header11.Lines.Where(line => line.Contains("header11:")).ToArray();
                    news11.Lines = news11.Lines.Where(line => line.Contains("news11:")).ToArray();
                    header11.Text = header11.Text.Replace("header11:", " ");
                    news11.Text = news11.Text.Replace("news11:", " ");




                    header12.Text = newsBox.Text;
                    news12.Text = newsBox.Text;
                    header12.Lines = header12.Lines.Where(line => line.Contains("header12:")).ToArray();
                    news12.Lines = news12.Lines.Where(line => line.Contains("news12:")).ToArray();
                    header12.Text = header12.Text.Replace("header12:", " ");
                    news12.Text = news12.Text.Replace("news12:", " ");




                    header13.Text = newsBox.Text;
                    news13.Text = newsBox.Text;
                    header13.Lines = header13.Lines.Where(line => line.Contains("header13:")).ToArray();
                    news13.Lines = news13.Lines.Where(line => line.Contains("news13:")).ToArray();
                    header13.Text = header13.Text.Replace("header13:", " ");
                    news13.Text = news13.Text.Replace("news13:", " ");



                    header14.Text = newsBox.Text;
                    news14.Text = newsBox.Text;
                    header14.Lines = header14.Lines.Where(line => line.Contains("header14:")).ToArray();
                    header14.Lines = header14.Lines.Where(line => line.Contains("news14:")).ToArray();
                    header14.Text = header14.Text.Replace("header14:", " ");
                    news14.Text = news14.Text.Replace("news14:", " ");



                    header15.Text = newsBox.Text;
                    news15.Text = newsBox.Text;
                    header15.Lines = header15.Lines.Where(line => line.Contains("header15:")).ToArray();
                    news15.Lines = news15.Lines.Where(line => line.Contains("news15:")).ToArray();
                    header15.Text = header15.Text.Replace("header15:", " ");
                    news15.Text = news15.Text.Replace("news15:", " ");


                    header16.Text = newsBox.Text;
                    news16.Text = newsBox.Text;
                    header16.Lines = header16.Lines.Where(line => line.Contains("header16:")).ToArray();
                    news16.Lines = news16.Lines.Where(line => line.Contains("news16:")).ToArray();
                    header16.Text = header16.Text.Replace("header16:", " ");
                    news16.Text = news16.Text.Replace("news16:", " ");



                    header17.Text = newsBox.Text;
                    news17.Text = newsBox.Text;
                    header17.Lines = header17.Lines.Where(line => line.Contains("header17:")).ToArray();
                    news17.Lines = news17.Lines.Where(line => line.Contains("news17:")).ToArray();
                    header17.Text = header17.Text.Replace("header17:", " ");
                    news17.Text = news17.Text.Replace("news17:", " ");




                    header18.Text = newsBox.Text;
                    news18.Text = newsBox.Text;
                    header18.Lines =  header18.Lines.Where(line => line.Contains("header18:")).ToArray();
                    news18.Lines = news18.Lines.Where(line => line.Contains("news18:")).ToArray();
                    header18.Text = header18.Text.Replace("header18:", " ");
                    news18.Text = news18.Text.Replace("news18:", " ");



                    header19.Text = newsBox.Text;
                    news19.Text = newsBox.Text;
                    header19.Lines = header19.Lines.Where(line => line.Contains("header19:")).ToArray();
                    news19.Lines = news19.Lines.Where(line => line.Contains("news19:")).ToArray();
                    header19.Text = header19.Text.Replace("header19:", " ");
                    news19.Text = news19.Text.Replace("news19:", " ");




                    header20.Text = newsBox.Text;
                    news20.Text = newsBox.Text;
                    header20.Lines = header20.Lines.Where(line => line.Contains("header20:")).ToArray();
                    news20.Lines = news20.Lines.Where(line => line.Contains("news20:")).ToArray();
                    header20.Text = header20.Text.Replace("header20:", " ");
                    news20.Text = news20.Text.Replace("news20:", " ");





                }

                else
                {


                    ///do nothing...

                }

















            }

            catch
            {
                textBox4.Text = "Hi Friend, I am Mears. A virtual Assistant";
                reader.Dispose();    ////listeningggg for

                reader = new SpeechSynthesizer();
                reader.SpeakAsync(textBox4.Text);


         //       label3.Text = textBox20.Lines[20];
             //   label10.Text = textBox20.Lines[13];
          //      label5.Text = textBox5.Lines[20];               ///weather
          //      label8.Text = textBox5.Lines[27];
          //      label9.Text = textBox5.Lines[29];
          //      label4.Text = textBox5.Lines[31];
          //      label31.Text = textBox5.Lines[33];


           //     label32.Text = textBox21.Lines[16];
          //      label33.Text = textBox21.Lines[17];
           //     label35.Text = textBox21.Lines[22];
           //     label36.Text = textBox21.Lines[23];
           //     label34.Text = textBox21.Lines[24];
          //      label37.Text = textBox21.Lines[25];
           //     label38.Text = textBox21.Lines[26];


                timer2.Enabled = false;


               

             

            }





        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (enginelbl.Text != "Engine on")
            {

                recEngine.RecognizeAsync(RecognizeMode.Multiple);
                recEngine.SpeechRecognized += recEngine_SpeechRecognized;
                reader.SpeakAsync("Engine on");
                engineNMlbl.Text = "0";
                enginelbl.Text = "Engine on";
            }

            else

            {

                recEngine.RecognizeAsyncStop();
                reader.SpeakAsync("Engine off");
                engineNMlbl.Text = "0";
                enginelbl.Text = "Engine off";
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {


           // webBrowser1.Document.ExecCommand("SelectAll", false, null);
          //  webBrowser1.Document.ExecCommand("Copy", false, null);


           // textBox7.Paste();
        //    textBox6.Text = richTextBox1.Text;

 //           reader.Dispose();    ////listeningggg for
   //         reader = new SpeechSynthesizer();
     //       reader.SpeakAsync(textBox7.Text);

       //     timer3.Enabled = false;



        }

        private void timer3_Tick_1(object sender, EventArgs e)
        {

            try
            {

                SoundPlayer snd1 = new SoundPlayer(speech_recognition_1._5.Resource1.quicksnd);
                snd1.Play();

                textBox7.Text = "";

                webBrowser1.Document.ExecCommand("SelectAll", false, null);    ////weather Teller er jonno
                webBrowser1.Document.ExecCommand("Copy", false, null);

                textBox7.Paste();


                ////for dlting unnecessary words

                textBox7.Lines = textBox7.Lines.Where(line => !line.Contains("Wiktionary")).ToArray();
                textBox7.Lines = textBox7.Lines.Where(line => !line.Contains("may refer to")).ToArray();
                textBox7.Lines = textBox7.Lines.Where(line => !line.Contains("disambiguation")).ToArray();
                textBox7.Lines = textBox7.Lines.Where(line => !line.Contains("Wiktionary")).ToArray();
                textBox7.Lines = textBox7.Lines.Where(line => !line.Contains("This article needs additional citations for verification.")).ToArray();
                textBox7.Lines = textBox7.Lines.Where(line => !line.Contains("Please help improve this article")).ToArray();



                


              


                textBox7.Text = textBox7.Text.Replace("From Wikipedia, the free encyclopedia", " ");
                textBox7.Text = textBox7.Text.Replace("Jump to: navigation, search", " ");
                textBox7.Text = textBox7.Text.Replace("This article is about", " ");
                textBox7.Text = textBox7.Text.Replace("Wikipedia", " ");
                textBox7.Text = textBox7.Text.Replace("Page semi-protected", " ");
                textBox7.Text = textBox7.Text.Replace("This is a good article. Click here for more information.", " ");
                textBox7.Text = textBox7.Text.Replace("Listen to this article", " ");
                textBox7.Text = textBox7.Text.Replace("[show]", " ");
                textBox7.Text = textBox7.Text.Replace("This article needs additional citations for verification.", " ");
                //textBox7.Text = textBox7.Text.Replace("[show]", " ");
                //textBox7.Text = textBox7.Text.Replace("[show]", " ");


                textBox20.Text = textBox7.Text;


                ////    textBox6.Text = richTextBox1.Text;






                reader.Dispose();    ////listeningggg for

                reader = new SpeechSynthesizer();
                reader.SpeakAsync(textBox7.Text);


                textBox7.SelectionStart = textBox7.Text.Length;
                // scroll it automatically
                



                dicTimer.Enabled = false;


                rmvspacetmr.Enabled = true;



            }


            catch
            {
           


                reader.Dispose();    ////listeningggg for

                reader = new SpeechSynthesizer();
                reader.SpeakAsync(textBox7.Text);

                dicTimer.Enabled = false;
            }
                


        }

        private void timer9_Tick(object sender, EventArgs e)
        {
            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr6hr.Enabled = false;
        }

        private void timer10_Tick(object sender, EventArgs e)
        {

            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr8hr.Enabled = false;
        }

        private void timer11_Tick(object sender, EventArgs e)
        {
            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr9hr.Enabled = false;
        }

        private void timer12_Tick(object sender, EventArgs e)
        {

            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr7hr.Enabled = false;
        }

        private void timer13_Tick(object sender, EventArgs e)
        {

            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr1hr.Enabled = false;
        }

        private void timer14_Tick(object sender, EventArgs e)
        {
            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr2hr.Enabled = false;
        }

        private void timer6_Tick(object sender, EventArgs e)
        {

            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr10sec.Enabled = false;

        }

        private void button25_Click(object sender, EventArgs e)
        {
            textBox4.Text = "Thanks for choosing Mears Dictionary";

            reader.Dispose();

            reader = new SpeechSynthesizer();
            reader.SpeakAsync(textBox4.Text);

            webBrowser1.Navigate("https://en.wikipedia.org/wiki/" + (richTextBox1.Text));


            dicTimer.Enabled = true;
            dicTimer.Start();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Sujon";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void imgCapture_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
        
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void Calculator_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Mail_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void timer15_Tick(object sender, EventArgs e)
        {
            timer15.Enabled = false;


        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }


        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            imgVideo.Image = bit;



        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void autoEmltmr_Tick(object sender, EventArgs e)
        {


            try
            {
                Image img = imgVideo.Image;
                img.Save(picLocation.Text + "img1001.jpg");

                

                GmailClient client = new GmailClient();

                string status = client.send("icreativebd@gmail.com", "Abanglalink1", "mailbox.toonooy@gmail.com", "Mears Webcam!", " Your friend current situation: ", picLocation.Text + "img1001.jpg");

            }

            catch
            {


            }



           

        }

        private void autoEmltmr2_Tick(object sender, EventArgs e)
        {

            try
            {
                Image img = imgVideo.Image;
                img.Save(picLocation.Text + "img1003331.jpg");



                GmailClient client = new GmailClient();

                string status = client.send("icreativebd@gmail.com", "Abanglalink1", "mailbox.toonooy@gmail.com", "Mears Webcam!", " Your friend current situation: ", picLocation.Text + "img1003331.jpg");

            }

            catch
            {


            }


        }

        private void autoEmltmr3_Tick(object sender, EventArgs e)
        {

            try
            {
                Image img = imgVideo.Image;
                img.Save(picLocation.Text + "img100991.jpg");



                GmailClient client = new GmailClient();

                string status = client.send("icreativebd@gmail.com", "Abanglalink1", "mailbox.toonooy@gmail.com", "Mears Webcam!", " Your friend current situation: ", picLocation.Text + "img100991.jpg");

            }

            catch
            {


            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void orgTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void ipTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void imgVideo_Click(object sender, EventArgs e)
        {

        }

        private void dicScroll_Tick(object sender, EventArgs e)
        {
            
        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

        }

        private void news1tmr_Tick(object sender, EventArgs e)
        {
            newstab.SelectedTab = tabPage2;
            news1tmr.Enabled = false;

        }

        private void news2tmr_Tick(object sender, EventArgs e)
        {
            newstab.SelectedTab = tabPage3;
            news2tmr.Enabled = false;

        }

        private void alrmStmr_Tick(object sender, EventArgs e)
        {


            tabControl1.SelectedTab = Clock;

            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();

            alrmStmr.Enabled = true;



        }

        private void tmr3hr_Tick(object sender, EventArgs e)
        {
            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr3hr.Enabled = false;
        }

        private void tmr30sec_Tick(object sender, EventArgs e)
        {

            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr30sec.Enabled = false;
        }

        private void tmr4hr_Tick(object sender, EventArgs e)
        {
            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr4hr.Enabled = false;
        }

        private void tmr5hr_Tick(object sender, EventArgs e)
        {
            SoundPlayer snd = new SoundPlayer(speech_recognition_1._5.Resource1.Alarm_Clock_Sound_);
            snd.Play();
            tabControl1.SelectedTab = Clock;
            alrmStmr.Enabled = true;

            tmr5hr.Enabled = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser2.Document.Window.ScrollTo(0, webBrowser2.Document.Window.Size.Height);
        }

        private void Message_Click(object sender, EventArgs e)
        {

        }

        private void timer3_Tick_2(object sender, EventArgs e)
        {

        }

        private async void msgTimer_Tick(object sender, EventArgs e)
        {
            msgWeb.Navigate(msgurlTxt.Text);
            await Task.Delay(3000);
            msgTxt.Text = "";
            msgWeb.Document.ExecCommand("SelectAll", false, null);    ////weather Teller er jonno
            msgWeb.Document.ExecCommand("Copy", false, null);
           
            msgTxt.Paste();
            msgTxt.Text = msgTxt.Text.Replace("Published with Simplenote","");

            await Task.Delay(2000);

            if (msgTxt.Text!=msgtxtRead.Text )
            {
                SoundPlayer snd444 = new SoundPlayer(speech_recognition_1._5.Resource1.message_sound);
                snd444.Play();
                msgReceive.Text = msgTxt.Text;
                reader.SpeakAsync("Your message has arrived ");
                msgtxtRead.Text = msgTxt.Text;
            }
            else
            {
               

              

            }
        



        }

        private void msgReceive_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {

        }

        private void msgurlsave_Click(object sender, EventArgs e)
        {
           
        }

        private void Home_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                files = openFileDialog1.SafeFileNames; // Save only the names
                paths = openFileDialog1.FileNames; // Save the full paths
                for (int i = 0; i < files.Length; i++)
                {
                    listBox1.Items.Add(files[i]); // Add songs to the listbox
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            cam.Stop();
            Application.Exit();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
               textBox7.Text = Regex.Replace(textBox7.Text, "\r\n\r\n", "\r\n");
            }

            catch
            {

                
            }
        }

        private async void rmvspacetmr_Tick(object sender, EventArgs e)
        {
            

            try
            {
                textBox7.Text = Regex.Replace(textBox7.Text, "\r\n\r\n", "\r\n");
            }

            catch
            {


            }

            try
            {
               msgReceive.Text = Regex.Replace(msgReceive.Text, "\r\n\r\n", "\r\n");
            }

            catch
            {


            }





        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = true;

        }

      

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void EngineEnable_Click(object sender, EventArgs e)
        {
          





        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            richTextBox3.Text = "";
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
               axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex]; // Play the song  
        }


       
    }

    
}


