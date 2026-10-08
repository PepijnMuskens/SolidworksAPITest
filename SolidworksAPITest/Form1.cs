using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace SolidworksAPITest
{
    public partial class Form1 : Form
    {
        SolidWorker solidWorker;

        private DeksloofCalculator? deksloofCalculator;

        public Form1()
        {
            InitializeComponent();

            solidWorker = new SolidWorker();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
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

                coordinates.Add(
                    new DeksloofCalculator.Point3D(
                        x * 1000,
                        y * 1000,
                        z * 1000));
            }

            return coordinates;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (solidWorker == null)
                    solidWorker = new SolidWorker();

                if (deksloofCalculator == null)
                {
                    MessageBox.Show(
                        "Please load a TXT coordinate file first.",
                        "No coordinate file",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                List<DeksloofCalculator.DrawingValues> values =
                    deksloofCalculator.CalculateAllDrawingValues();

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

        private void btnCompare_Click(object sender, EventArgs e)
        {
            try
            {
                
                // 1. Make sure a TXT file has been loaded
                if (deksloofCalculator == null)
                {
                    MessageBox.Show(
                        "Please load a TXT coordinate file first.",
                        "No coordinate file",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                
                // 2. Select the reference Excel file
                using OpenFileDialog openDialog = new OpenFileDialog();

                openDialog.Filter = "Excel files (*.xlsx)|*.xlsx|" + "All files (*.*)|*.*";

                openDialog.Title = "Select reference Excel file";

                if (openDialog.ShowDialog() != DialogResult.OK)
                    return;

                string referenceFile = openDialog.FileName;

                
                // 3. Calculate the current values
                List<DeksloofCalculator.DrawingValues> calculated = deksloofCalculator.CalculateAllDrawingValues();

                if (calculated.Count == 0)
                {
                    MessageBox.Show(
                        "There are no calculated drawing values to compare.",
                        "No values",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                
                // 4. Read the reference Excel
                var reader = new ReferenceExcelReader();

                List<DeksloofCalculator.DrawingValues> reference = reader.Read(referenceFile);

                if (reference.Count == 0)
                {
                    MessageBox.Show(
                        "No reference values were found in the Excel file.",
                        "No reference data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                
                // 5. Compare calculated vs reference
                var comparer = new DrawingValuesComparer(tolerance: 1.0);

                List<DrawingComparison> comparisons = comparer.Compare(calculated, reference);

                
                // 6. Ask where to save the comparison
                using SaveFileDialog saveDialog = new();

                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";

                saveDialog.Title = "Save comparison Excel file";

                saveDialog.FileName = "Comparison.xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                
                // 7. Write the comparison Excel
                var writer = new ExcelComparisonWriter();

                writer.Write(saveDialog.FileName, comparisons);

                
                // 8. Finished
                MessageBox.Show(
                    $"Comparison completed successfully.\n\n" +
                    $"Reference file:\n{referenceFile}\n\n" +
                    $"Comparison file:\n{saveDialog.FileName}",
                    "Comparison complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"The comparison could not be completed.\n\n" +
                    $"{ex.Message}",
                    "Comparison error",
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
    }
}
