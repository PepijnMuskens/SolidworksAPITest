namespace SolidworksAPITest
{
    public enum ComparisonStatus
    {
        Match,
        SmallDifference,
        LargeDifference
    }

    public record ValueComparison(
        string ValueName,
        double Calculated,
        double Reference,
        double Difference,
        ComparisonStatus Status
    );

    public record DrawingComparison(
        int DrawingNumber,
        List<ValueComparison> Values
    );
}