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
            InitializeComponent();
            synchronizationContext = System.Threading.SynchronizationContext.Current;

        }
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            var count = 0;

            await System.Threading.Tasks.Task.Run(() => {count = this.LogRunningOperation();});

            buttonStart.Text = string.Format(@"Counter {0}", count);
            buttonStart.Enabled = true;
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
            if ((DateTime.Now - previousTime).Milliseconds <= 100)
            {
                return;
            }

            this.synchronizationContext.Post(
                o => { this.updatex((int)o);}
                , value);

            previousTime = timeNow;
        }

        public void updatex(int n)
        {
            this.buttonStart.Text = string.Format(@"Counter {0}", n);
        }
    }
}
