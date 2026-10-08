using ClosedXML.Excel;

namespace SolidworksAPITest
{
    public class ExcelComparisonWriter
    {
        public void Write(string filePath, List<DrawingComparison> comparisons)
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Comparison");

            // Headers
            worksheet.Cell(1, 1).Value = "Drawing";
            worksheet.Cell(1, 2).Value = "Value";
            worksheet.Cell(1, 3).Value = "Calculated";
            worksheet.Cell(1, 4).Value = "Reference";
            worksheet.Cell(1, 5).Value = "Difference";
            worksheet.Cell(1, 6).Value = "Status";

            // Header formatting
            var header = worksheet.Range("A1:F1");

            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.LightGray;

            int row = 2;

            foreach (var drawing in comparisons)
            {
                foreach (var value in drawing.Values)
                {
                    worksheet.Cell(row, 1).Value = drawing.DrawingNumber;

                    worksheet.Cell(row, 2).Value = value.ValueName;

                    worksheet.Cell(row, 3).Value = value.Calculated;

                    worksheet.Cell(row, 4).Value = value.Reference;

                    worksheet.Cell(row, 5).Value = value.Difference;

                    worksheet.Cell(row, 6).Value = GetStatusText(value.Status);

                    ApplyStatusFormatting(
                        worksheet,
                        row,
                        value.Status);

                    row++;
                }
            }

            // Number formatting
            worksheet.Column(3).Style.NumberFormat.Format = "0.0";
            worksheet.Column(4).Style.NumberFormat.Format = "0.0";
            worksheet.Column(5).Style.NumberFormat.Format = "0.000";

            // Make columns fit their contents
            worksheet.Columns().AdjustToContents();

            // Freeze the header
            worksheet.SheetView.FreezeRows(1);

            // Add autofilter
            worksheet.Range(1, 1, row - 1, 6).SetAutoFilter();

            workbook.SaveAs(filePath);
        }

        private static string GetStatusText(ComparisonStatus status)
        {
            return status switch
            {
                ComparisonStatus.Match => "OK",

                ComparisonStatus.SmallDifference => "SMALL DIFFERENCE",

                ComparisonStatus.LargeDifference =>"LARGE DIFFERENCE",

                _ => "UNKNOWN"
            };
        }

        private static void ApplyStatusFormatting(IXLWorksheet worksheet, int row, ComparisonStatus status)
        {
            var range = worksheet.Range(row, 3, row, 6);

            switch (status)
            {
                case ComparisonStatus.Match:
                    range.Style.Fill.BackgroundColor = XLColor.PastelGreen;
                    break;

                case ComparisonStatus.SmallDifference:
                    range.Style.Fill.BackgroundColor = XLColor.PastelYellow;
                    break;

                case ComparisonStatus.LargeDifference:
                    range.Style.Fill.BackgroundColor =XLColor.PastelRed;
                    break;
            }
        }
    }
}