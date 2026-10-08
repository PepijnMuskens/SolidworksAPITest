namespace SolidworksAPITest
{
    public class DrawingValuesComparer
    {
        private readonly double tolerance;

        public DrawingValuesComparer(double tolerance = 1.0)
        {
            this.tolerance = tolerance;
        }

        public List<DrawingComparison> Compare(List<DeksloofCalculator.DrawingValues> calculated, List<DeksloofCalculator.DrawingValues> reference)
        {
            List<DrawingComparison> results = [];

            foreach (var calculatedDrawing in calculated)
            {
                var referenceDrawing = reference.FirstOrDefault(
                    x => x.DrawingNumber == calculatedDrawing.DrawingNumber);

                if (referenceDrawing == null)
                {
                    throw new Exception(
                        $"No reference data found for drawing " +
                        $"{calculatedDrawing.DrawingNumber}.");
                }

                List<ValueComparison> values =
                [
                    CompareValue(
                        "A",
                        calculatedDrawing.A,
                        referenceDrawing.A),

                    CompareValue(
                        "B",
                        calculatedDrawing.B,
                        referenceDrawing.B),

                    CompareValue(
                        "E",
                        calculatedDrawing.E,
                        referenceDrawing.E),

                    CompareValue(
                        "F",
                        calculatedDrawing.F,
                        referenceDrawing.F),

                    CompareValue(
                        "G",
                        calculatedDrawing.G,
                        referenceDrawing.G),

                    CompareValue(
                        "K",
                        calculatedDrawing.K,
                        referenceDrawing.K),

                    CompareValue(
                        "J",
                        calculatedDrawing.J,
                        referenceDrawing.J)
                ];

                results.Add(new DrawingComparison(calculatedDrawing.DrawingNumber, values));
            }

            return results;
        }

        private ValueComparison CompareValue(string name, double calculated, double reference)
        {
            double difference = Math.Abs(calculated - reference);

            ComparisonStatus status;

            if (difference < 0.0001)
            {
                status = ComparisonStatus.Match;
            }
            else if (difference <= tolerance)
            {
                status = ComparisonStatus.SmallDifference;
            }
            else
            {
                status = ComparisonStatus.LargeDifference;
            }

            return new ValueComparison(
                name,
                calculated,
                reference,
                difference,
                status);
        }
    }
}