using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Isotope.UI.CommonControls
{
    public partial class FormRunner : Form
    {
        public FormRunner()
        {
            InitializeComponent();
        }

        private void buttonUnselectAll_Click(object sender, System.EventArgs e)
        {
            WinFormUtil.SetItemsChecked(this.checkedListBox1, false);
        }

        public void LoadRunners<T>(System.Reflection.Assembly assembly) where T : RunnerBase
        {
            if (assembly == null)
            {
                throw new System.ArgumentNullException("assembly");
            }

            var runners = FindRunners<T>(assembly);
            foreach (var runnerBase in runners)
            {
                this.checkedListBox1.Items.Add(runnerBase);
            }
            this.labelItemCount.Text = this.checkedListBox1.Items.Count.ToString();
        }

        /// <summary>
        /// Finds all the Runner classes in an assembly - where T is a class that derives from runner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static List<RunnerBase> FindRunners<T>(System.Reflection.Assembly assembly) where T : RunnerBase
        {
            if (assembly == null)
            {
                throw new System.ArgumentNullException("assembly");
            }

            // Get all the types of
            var runnertype = typeof (T);
            var runnertypes = assembly.GetTypes()
                .Where(i => i.IsSubclassOf(runnertype))
                .Where(i => !i.IsAbstract)
                .OrderBy(i => i.FullName)
                .ToList();

            // Find all default public default constructors
            var constuctor_parameter_types = new System.Type[] { };
            var constructors = runnertypes
                .Select(t => t.GetConstructor(constuctor_parameter_types))
                .Where(c => c.IsPublic)
                .ToList();

            var type_to_constructor = runnertypes.ToDictionary(t => t, t => t.GetConstructor(constuctor_parameter_types));
            foreach (var kv in type_to_constructor)
            {
                if (kv.Value == null)
                {
                    string msg = string.Format("Type {0} has no default constructor", kv.Key.Name);
                    throw new System.ArgumentException(msg);
                }

                if (!kv.Value.IsPublic)
                {
                    string msg = string.Format("Type {0} has a default constructore put it is not public", kv.Key.Name);
                    throw new System.ArgumentException(msg);
                }
            }

            var constructor_parameter_values = new object[] {};

            var runners = constructors
                .Select(c => c.Invoke(constructor_parameter_values))
                .Select(o => (RunnerBase) o)
                .ToList();

            if (runners.Any(r => r == null))
            {
                string msg = string.Format("One of the runners is null");
                throw new System.ArgumentException(msg);
            }

            return runners;
        }

        private void buttonRun_Click(object sender, System.EventArgs e)
        {
            Run();
        }

        private void Run()
        {
            this.WriteLine("Start");

            var runners = new List<RunnerBase>();
            foreach (var i in this.checkedListBox1.CheckedItems)
            {
                runners.Add((RunnerBase) i);
            }

            this.WriteLine("Number of items to run: {0}", runners.Count);

            foreach (var runner in runners)
            {
                this.WriteLine("> {0}", runner.ToString());
                try
                {
                    runner.Run();
                }
                catch (System.Exception exc)
                {
                    this.WriteLine("Caught Exception");
                    string msg = runner.ToString() + exc.Message;
                    MessageBox.Show(msg);
                    this.WriteLine("Stopping the run");
                    break;
                }
            }
            WriteLine("Stoppped.");
        }

        private void WriteLine(string s)
        {
            if (this.OnWriteText != null)
            {
                this.OnWriteText(s);
            }
        }

        private void WriteLine(string fmt, params object[] args)
        {
            string s = string.Format(fmt, args);
            this.WriteLine(s);
        }

        public event WriteText OnWriteText;

        public delegate void WriteText(string s);

        private void buttonSelectAll_Click(object sender, System.EventArgs e)
        {
            WinFormUtil.SetItemsChecked(this.checkedListBox1, true);
        }
    }
}