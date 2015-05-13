using System;
using System.Threading;
using System.Windows.Forms;

namespace DemoWinFormAsyncAwait
{
    public partial class FormAsyncAwait : Form
    {
        // modified from example given here: http://stephenhaunts.com/2014/10/14/using-async-and-await-to-update-the-ui-thread/

        private readonly System.Threading.SynchronizationContext synchronizationContext;
        private DateTime previousTime = DateTime.Now;

        public FormAsyncAwait()
        {
            this.InitializeComponent();
            this.synchronizationContext = System.Threading.SynchronizationContext.Current;

        }

        private CancellationTokenSource cancellation_token_source;
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = false;
            var count = 0;
            cancellation_token_source = new CancellationTokenSource();

            var cancellation_token = cancellation_token_source.Token;


            await System.Threading.Tasks.Task.Run(
                () => {count = this.LogRunningOperation();}, cancellation_token);

            this.buttonStart.Text = $@"Counter {count}";
            this.buttonStart.Enabled = true;
        }

        private int LogRunningOperation()
        {
            int count=0;
            for (var i = 0; i <= 5000000; i++)
            {
                if (this.cancellation_token_source.IsCancellationRequested)
                {
                    break;
                }
                this.UpdateUI(i);
                count = i;
            }
            return count;
        }

        public void UpdateUI(int value)
        {
            var timeNow = DateTime.Now;

            // this prevents excessive refreshing
            if ((DateTime.Now - this.previousTime).Milliseconds <= 100)
            {
                return;
            }

            this.synchronizationContext.Post(
                o =>
                {
                    int n = (int) o;
                    this._update_ux_for_real(n);
                }
                , value);

            this.previousTime = timeNow;
        }

        public void _update_ux_for_real(int n)
        {
            if (this.buttonStart.IsDisposed)
            {
                return;
            }

            if (this.textBox1.IsDisposed)
            {
                return;
            }
            string s = $@"Counter {n}";
            this.buttonStart.Text = s;
            this.textBox1.AppendText(s+"\r\n");
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.cancellation_token_source.Cancel();
        }
    }
}
