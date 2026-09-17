using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidworksAPITest
{
    public partial class Form1 : Form
    {
        SolidWorker solidWorker;
        public Form1()
        {
            InitializeComponent();
            solidWorker = new SolidWorker();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (solidWorker == null)
                    solidWorker = new SolidWorker();

                solidWorker.CreateCube(100);   // 100 mm cube
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "SolidWorks error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
