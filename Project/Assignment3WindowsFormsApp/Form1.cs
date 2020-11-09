using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Activities;
using System.Activities.Statements;

namespace Assignment3WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("cityArg", textBox1.Text);

            Activity sunWorkflow = new SunshineActivity();

            IDictionary<string, object> output = WorkflowInvoker.Invoke(sunWorkflow, input);
            label2.Text = output["resultArg"].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("Message", label2.Text);

            Activity encryptWorkflow = new EncryptActivity();

            IDictionary<string, object> output = WorkflowInvoker.Invoke(encryptWorkflow, input);
            label3.Text = output["Result"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("Message", label3.Text);

            Activity decryptWorkflow = new DecryptActivity();

            IDictionary<string, object> output = WorkflowInvoker.Invoke(decryptWorkflow, input);
            label4.Text = output["Result"].ToString();
        }
    }
}
