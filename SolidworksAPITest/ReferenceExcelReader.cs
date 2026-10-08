using ClosedXML.Excel;

namespace SolidworksAPITest
{
    public class ReferenceExcelReader
    {
        public List<DeksloofCalculator.DrawingValues> Read(
            string filePath)
        {
            using var workbook = new XLWorkbook(filePath);

            var worksheet = workbook.Worksheet(1);

            var rows = worksheet.RowsUsed();

            // Find the header row
            var headerRow = rows.First();

            Dictionary<string, int> columns = [];

            foreach (var cell in headerRow.CellsUsed())
            {
                string header = cell.GetString()
                    .Trim()
                    .ToUpperInvariant();

                columns[header] = cell.Address.ColumnNumber;
            }

            List<DeksloofCalculator.DrawingValues> values = [];

            foreach (var row in rows.Skip(1))
            {
                if (row.IsEmpty())
                    continue;

                int drawingNumber =
                    row.Cell(columns["DRAWING"]).GetValue<int>();

                double A =
                    row.Cell(columns["A"]).GetValue<double>();

                double B =
                    row.Cell(columns["B"]).GetValue<double>();

                double E =
                    row.Cell(columns["E"]).GetValue<double>();

                double F =
                    row.Cell(columns["F"]).GetValue<double>();

                double G =
                    row.Cell(columns["G"]).GetValue<double>();

                double K =
                    row.Cell(columns["K"]).GetValue<double>();

                double J =
                    row.Cell(columns["J"]).GetValue<double>();

                // C and D aren't currently present in the Excel reference.
                double C = 0;
                double D = 0;

                values.Add(
                    new DeksloofCalculator.DrawingValues(
                        drawingNumber,
                        A,
                        B,
                        J,
                        C,
                        D,
                        E,
                        F,
                        G,
                        K));
            }

            return values;
        }
    }
}