using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Globalization;

namespace SolidworksAPITest
{
    public partial class Form1 : Form
    {
        SolidWorker SolidWorker;
        private DeksloofCalculator? deksloofCalculator;
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(textBoxCreateTussenplaatBovenA.Text);
                double b = Convert.ToDouble(textBoxCreateTussenplaatBovenB.Text);
                double c = Convert.ToDouble(textBoxCreateTussenplaatBovenC.Text);
                double d = Convert.ToDouble(textBoxCreateTussenplaatBovenD.Text);
                SolidWorker.CreateTussenplaatBoven(a, b, c, d);
            }
            catch
            {
                Console.WriteLine("textinput is not a number");
            }

        }

        private void textBoxCreateTussenplaatBovenA_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(textBox4.Text);
            double b = Convert.ToDouble(textBox5.Text);
            double c = Convert.ToDouble(textBox6.Text);
            double j = Convert.ToDouble(textBox7.Text);

            Dictionary<string, double> measurements = new Dictionary<string, double>();
            measurements.Add("A", a);
            measurements.Add("B", b);
            measurements.Add("C", c);
            measurements.Add("J", j);

            SolidWorker.UpdateEquations(measurements);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SolidWorker.saveFile("testpart");
        }

        private void OpenFileBtn_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter =
                "Text files (*.txt)|*.txt|" +
                "All files (*.*)|*.*";

            dialog.Title = "Select coordinate TXT file";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                List<DeksloofCalculator.Point3D> coordinates =
                    LoadCoordinatesFromFile(dialog.FileName);

                if (coordinates.Count < 2)
                {
                    MessageBox.Show(
                        "The selected file does not contain enough valid coordinates.\n\n" +
                        "At least two coordinates are required.",
                        "Invalid file",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                deksloofCalculator =
                    new DeksloofCalculator(coordinates);

                MessageBox.Show(
                    $"Successfully loaded {coordinates.Count} coordinates.\n\n" +
                    $"Calculated anker sizes: {coordinates.Count - 1}",
                    "File loaded",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not load the coordinate file.\n\n{ex.Message}",
                    "File error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private List<DeksloofCalculator.Point3D> LoadCoordinatesFromFile(
           string filePath)
        {
            List<DeksloofCalculator.Point3D> coordinates = [];

            string[] lines = File.ReadAllLines(filePath);

            int lineNumber = 0;

            foreach (string line in lines)
            {
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');

                if (parts.Length != 3)
                {
                    throw new FormatException(
                        $"Invalid format on line {lineNumber}.\n\n" +
                        $"Expected: X,Y,Z\n" +
                        $"Received: {line}");
                }

                if (!double.TryParse(
                        parts[0].Trim(),
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double x))
                {
                    throw new FormatException(
                        $"Invalid X coordinate on line {lineNumber}.\n\n" +
                        $"Value: {parts[0]}");
                }

                if (!double.TryParse(
                        parts[1].Trim(),
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double y))
                {
                    throw new FormatException(
                        $"Invalid Y coordinate on line {lineNumber}.\n\n" +
                        $"Value: {parts[1]}");
                }

                if (!double.TryParse(
                        parts[2].Trim(),
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double z))
                {
                    throw new FormatException(
                        $"Invalid Z coordinate on line {lineNumber}.\n\n" +
                        $"Value: {parts[2]}");
                }

                coordinates.Add(new DeksloofCalculator.Point3D(x * 1000, y * 1000, z * 1000));
            }

            return coordinates;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (SolidWorker == null)
                    SolidWorker = new SolidWorker();

                if (deksloofCalculator == null)
                {
                    MessageBox.Show(
                        "Please load a TXT coordinate file first.",
                        "No coordinate file",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                List<DeksloofCalculator.DrawingValues> values = deksloofCalculator.CalculateAllDrawingValues();

                dataGridViewDrawingValues.DataSource = null;
                dataGridViewDrawingValues.DataSource = values;

                FormatDataGridView();

                if (values.Count == 0)
                {
                    MessageBox.Show(
                        "The file does not contain enough coordinates " +
                        "to calculate a drawing value.\n\n" +
                        "At least 3 calculated anker sizes are required " +
                        "for the first drawing value.",
                        "Not enough data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                // solidWorker.CreateCube(100);
                // solidWorker.ModifyDeksloof();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Calculation error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridViewDrawingValues.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridViewDrawingValues.AllowUserToAddRows = false;
            dataGridViewDrawingValues.AllowUserToDeleteRows = false;
            dataGridViewDrawingValues.ReadOnly = true;
            dataGridViewDrawingValues.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridViewDrawingValues.MultiSelect = false;

            // Format numeric columns to whole millimetres.
            if (dataGridViewDrawingValues.Columns.Contains("A"))
                dataGridViewDrawingValues.Columns["A"]
                    .DefaultCellStyle.Format = "F0";

            if (dataGridViewDrawingValues.Columns.Contains("B"))
                dataGridViewDrawingValues.Columns["B"]
                    .DefaultCellStyle.Format = "F3";

            if (dataGridViewDrawingValues.Columns.Contains("J"))
                dataGridViewDrawingValues.Columns["J"]
                    .DefaultCellStyle.Format = "F3";

            if (dataGridViewDrawingValues.Columns.Contains("C"))
                dataGridViewDrawingValues.Columns["C"]
                    .DefaultCellStyle.Format = "F0";

            if (dataGridViewDrawingValues.Columns.Contains("D"))
                dataGridViewDrawingValues.Columns["D"]
                    .DefaultCellStyle.Format = "F0";

            if (dataGridViewDrawingValues.Columns.Contains("E"))
                dataGridViewDrawingValues.Columns["E"]
                    .DefaultCellStyle.Format = "F0";

            if (dataGridViewDrawingValues.Columns.Contains("F"))
                dataGridViewDrawingValues.Columns["F"]
                    .DefaultCellStyle.Format = "F0";

            if (dataGridViewDrawingValues.Columns.Contains("G"))
                dataGridViewDrawingValues.Columns["G"]
                    .DefaultCellStyle.Format = "F0";

            if (dataGridViewDrawingValues.Columns.Contains("K"))
                dataGridViewDrawingValues.Columns["K"]
                    .DefaultCellStyle.Format = "F0";
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            double partnr = Convert.ToDouble(textBox4.Text);
            Dictionary<string, double> measurements = new Dictionary<string, double>();
            //    measurements.Add("A", a);
            //    measurements.Add("B", b);
            //    measurements.Add("C", c);
            //    measurements.Add("D", d);
            //    measurements.Add("D", d);

            //    measurements.Add("D", d);
            //    measurements.Add("D", d);
            //    measurements.Add("D", d);
            //
        } 
    }
}