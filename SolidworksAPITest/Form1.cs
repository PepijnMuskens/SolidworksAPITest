using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidworksAPITest
{
    public partial class Form1 : Form
    {
        SolidWorker SolidWorker;
        public Form1()
        {
            InitializeComponent();
            SolidWorker = new SolidWorker();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x, y, z = 0;
            try
            {
                x = Convert.ToInt32(textBox1.Text);
                y = Convert.ToInt32(textBox2.Text);
                z = Convert.ToInt32(textBox3.Text);
            }
            catch
            {
                Console.WriteLine("textinput is not a number");
            }
            SolidWorker.SetSize(x,y,z);
            SolidWorker.UpdateModel();
        }
    }
}
