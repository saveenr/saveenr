using System;
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
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = false;
            var count = 0;

            await System.Threading.Tasks.Task.Run(() => {count = this.LogRunningOperation();});

            this.buttonStart.Text = string.Format(@"Counter {0}", count);
            this.buttonStart.Enabled = true;
        }

        private int LogRunningOperation()
        {
            int count=0;
            for (var i = 0; i <= 5000000; i++)
            {
                this.UpdateUI(i);
                count = i;
            }
            return count;
        }

        public void UpdateUI(int value)
        {
            var timeNow = DateTime.Now;

            // this prevents excessive refreshing
            // only update the UI if sufficient time has passed since the last update
            var time_span = DateTime.Now - this.previousTime;
            if (time_span.Milliseconds <= 100)
            {
                return;
            }

            this.synchronizationContext.Post(
                o => { this.UpdateStartButton((int)o);}
                , value);

            this.previousTime = timeNow;
        }

        public void UpdateStartButton(int n)
        {
            this.buttonStart.Text = string.Format(@"Counter {0}", n);
        }
    }
}
