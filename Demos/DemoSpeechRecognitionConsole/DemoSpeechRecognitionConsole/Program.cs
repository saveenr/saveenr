using System;
using SSR = System.Speech.Recognition;

namespace DemoSpeechRecognitionConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            var t = new System.Threading.Thread(new System.Threading.ThreadStart(wreck_a_nice_beach));
            t.Start();
            Console.ReadLine();
        }

        private static void wreck_a_nice_beach()
        {
            var sre = new SSR.SpeechRecognitionEngine();
            sre.SetInputToDefaultAudioDevice();
            sre.UnloadAllGrammars();

            var gb1 = new SSR.GrammarBuilder();
            gb1.Append(new SSR.Choices("cut", "copy", "paste", "delete", "quit"));


            var g1 = new SSR.Grammar(gb1);
            sre.LoadGrammar(g1);

            sre.SpeechRecognized += SreOnSpeechRecognized;
            sre.SpeechDetected += SreOnSpeechDetected;
            sre.SpeechHypothesized += SreOnSpeechHypothesized;
            sre.SpeechRecognitionRejected += SreOnSpeechRecognitionRejected;
            sre.AudioSignalProblemOccurred += SreOnAudioSignalProblemOccurred;

            sre.RecognizeAsync(SSR.RecognizeMode.Multiple);
        }

        private static void SreOnAudioSignalProblemOccurred(object sender, SSR.AudioSignalProblemOccurredEventArgs e)
        {
            Console.WriteLine("Audio Level: {0}", e.AudioLevel);
            Console.WriteLine("Audio Position: {0}", e.AudioPosition);
            Console.WriteLine("Audio SignalProblem: {0}", e.AudioSignalProblem);
        }

        private static void SreOnSpeechRecognitionRejected(object sender, SSR.SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine("Rejected {0}", e.Result.Text);
        }

        private static void SreOnSpeechHypothesized(object sender, SSR.SpeechHypothesizedEventArgs e)
        {
            Console.WriteLine("Hypothesized: {0}", e.Result.Text);
        }

        private static void SreOnSpeechDetected(object sender, SSR.SpeechDetectedEventArgs e)
        {
            Console.WriteLine("Detected: {0}", e.AudioPosition);
        }

        private static void SreOnSpeechRecognized(object sender, SSR.SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Recognized: {0}", e.Result.Text);
            Console.WriteLine("--------------------");
        }
    }
}
