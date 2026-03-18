namespace FStar.UI.Models;

public record DataPoint(double X, double Y);

public record ChartSeries(
    string Label,
    IReadOnlyList<DataPoint> Points,
    string Color = "#2563eb",
    double StrokeWidth = 2.0,
    bool ShowDots = false);

public record BarItem(string Label, double Value, string Color = "#2563eb");

public enum BarOrientation { Vertical, Horizontal }

public record BarSegment(string Label, double Value, string Color);

public record BarStack(string Category, IReadOnlyList<BarSegment> Segments);

public record WaterfallItem(string Label, double Value, bool IsTotal = false);

public record TornadoFactor(string Label, double PositiveContribution, double NegativeContribution);

public record NumberLinePoint(double Value, string Color);

public record AnimatedSeries(string Label, double[] Values, string Color);

public record ChartMarker(double X, double Y, string Color, string? Label = null, IReadOnlyList<string>? Tooltip = null);

public record RadarAxis(string Label, double Value, double Max = 10.0, IReadOnlyList<string>? Tooltip = null);
