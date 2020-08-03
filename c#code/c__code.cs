/*
  This Program is to connect c# and Arduino to transmit data from computer microphone to arduino board
  By Jalal Mansoori
*/
using System;
using System.Windows.Forms;
using System.IO.Ports; // This library is for connecting c# and Arduino to transmit and receive data through ports
//Below are libraries for voice recognition
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace CsharpCode
{
    public partial class Form1 : Form
    {
        //Creating objects
        SerialPort myPort = new SerialPort();
        SpeechRecognitionEngine re = new SpeechRecognitionEngine();
        SpeechSynthesizer ss = new SpeechSynthesizer(); // When you want program to talk back to you 
        Choices commands = new Choices(); // This is an important class as name suggest we will store our commands in this object
        
       
        public Form1()
        {
            InitializeComponent();
            //Details of Arduino board
            myPort.PortName = "COM5"; // My Port name in Arduino IDE selected COM5 you need to change Port name if it is different  just check in arduinoIDE
            myPort.BaudRate = 9600;  // This Rate is Same as arduino Serial.begin(9600) bits per second
            processing();
            
        }

        // Defined Function processing where main instruction will be executed ! 
        void processing()
        { 
            //First of all storing commands
            commands.Add(new string[] { "Blue On", "Red On", "Green On", "Blue Off", "Red Off", "Green Off", "Exit", "All On", "All Off","Arduino Say Good Bye to makers" });

            //Now we will create object of Grammer in which we will pass commands as parameter
            Grammar gr = new Grammar(new GrammarBuilder(commands));

            // For more information about below funtions refer to site https://docs.microsoft.com/en-us/dotnet/api/system.speech.recognition?view=netframework-4.7.2
            re.RequestRecognizerUpdate(); // Pause Speech Recognition Engine before loading commands
            re.LoadGrammarAsync(gr);
            re.SetInputToDefaultAudioDevice();// As Name suggest input device builtin microphone or you can also connect earphone etc...
            re.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(re_SpeechRecognized);
            
            
        }

        void re_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch(e.Result.Text)
            {
                ////For Led State ON 
                // For blue led
                case "Blue On":
                    sendDataToArduino('B');
                    break;

                // For red led
                case "Red On":
                    sendDataToArduino('R');
                    break;

                // For green led
                case "Green On":
                    sendDataToArduino('G');
                    break;
                
                //For Led State OFF 
                // For blue led
                case "Blue Off":
                    sendDataToArduino('Z');
                    break;

                // For red led
                case "Red Off":
                    sendDataToArduino('X');
                    break;

                // For green led
                case "Green Off":
                    sendDataToArduino('C');
                    break;

                //For turning ON all leds at once :)
                case "All On":
                    sendDataToArduino('V');
                    break;

                //For turning OFF all leds at once :)
                case "All Off":
                    sendDataToArduino('M');
                    break;
                //Program will talk back 
                case "Arduino Say Good Bye to makers":
                    ss.SpeakAsync("Good Bye Makers"); // speech synthesis object is used for this purpose
                    break;
                
                // To Exit Program using Voice :)
                case "Exit":
                    Application.Exit();
                    break;
            }
            txtCommands.Text += e.Result.Text.ToString() + Environment.NewLine;// Whenever we command arduino text will append and shown in textbox
        }

        void sendDataToArduino(char character)
        {
            myPort.Open();
            myPort.Write(character.ToString());
            myPort.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            re.RecognizeAsyncStop();
            //btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnStart.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            re.RecognizeAsync(RecognizeMode.Multiple);
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            MessageBox.Show("Voice Recognition Started !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
