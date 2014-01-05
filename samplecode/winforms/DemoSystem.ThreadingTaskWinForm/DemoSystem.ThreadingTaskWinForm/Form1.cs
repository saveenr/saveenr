using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DemoSystem.ThreadingTaskWinForm
{
    public partial class Form1 : Form
    {
        private System.Threading.Tasks.Task<int> task;

        public Form1()
        {
            InitializeComponent();
        }

        private void update_ui(string fmt, params object[] tokens)
        {
            var s = string.Format(fmt, tokens);
            var snl = string.Format("{0}\n", s);
            this.textBox1.AppendText(snl);
        }

        public delegate void delegate_updateui(string fmt, params object[] tokens);

        public void ts_updateui(string fmt, params object[] tokens)
        {
            var xx = new object[] {fmt, tokens};
            this.Invoke(new delegate_updateui(this.update_ui),xx);
        }

        private System.Threading.CancellationTokenSource cTokenSource;

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.update_ui("Start Clicked");

            cTokenSource = new System.Threading.CancellationTokenSource();
            System.Threading.CancellationToken cToken = cTokenSource.Token;

            task = System.Threading.Tasks.Task<int>.Factory
                .StartNew(
                        () => GenerateNumbers(cToken), cToken)
                .ContinueWith( t =>
                                   {
                                       this.ts_updateui("TASK: ContinueWith: DONE");
                                       return 1;
                                   })
                ;

            cToken.Register(() => cancelNotification());
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.update_ui("Stop Button Clicked");
            this.cTokenSource.Cancel();
        }

        int GenerateNumbers(System.Threading.CancellationToken ct)
        {
            int i;
            this.ts_updateui("TASK: Started");
            for (i = 0; i < 10; i++)
            {
                this.ts_updateui("TASK: {0}\n", i);
                System.Threading.Thread.Sleep(500);
                if (ct.IsCancellationRequested)
                {
                    this.ts_updateui("TASK: Cancellation was requested");
                    break;
                }
            }

            this.ts_updateui("TASK: Ended");
            return i;
        }

        // Notify when task is cancelled
        static void cancelNotification()
        {
            Console.WriteLine("Cancellation request made!!");
        }
    }
}
