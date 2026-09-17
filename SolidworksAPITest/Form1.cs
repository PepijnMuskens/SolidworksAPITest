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
                //SolidWorker.SetSize(x,y,z);
            }
            catch
            {
                Console.WriteLine("textinput is not a number");
            }
            SolidWorker.UpdateModel();
            SolidWorker.CreateCube();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(textBoxCreateTussenplaatBovenA.Text);
                double b = Convert.ToDouble(textBoxCreateTussenplaatBovenB.Text);
                double c = Convert.ToDouble(textBoxCreateTussenplaatBovenC.Text);
                double d = Convert.ToDouble(textBoxCreateTussenplaatBovenD.Text);
                SolidWorker.CreateTussenplaatBoven(a,b,c,d);
            }
            catch
            {
                Console.WriteLine("textinput is not a number");
            }
            
        }

        private void textBoxCreateTussenplaatBovenA_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
