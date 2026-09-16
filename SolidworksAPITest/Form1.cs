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
            SolidWorker.UpdateModel();
        }
    }
}
