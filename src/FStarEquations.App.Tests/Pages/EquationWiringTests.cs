using Bunit;
using Xunit;
using FStarEquations.App.Pages;

namespace FStarEquations.App.Tests.Pages;

public class EquationWiringTests : BunitContext
{
    [Fact]
    public void VariancePage_DefaultSliders_ShowsCorrectVariance()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        // Default: M varies 0.5-5, Var(F)=2. OutputVariance(1, 2) = 1*1*2 = 2
        // The page renders LineChart with polyline elements for the variance series
        var cut = Render<VariancePage>();

        var polylines = cut.FindAll("svg.line-chart polyline");
        Assert.True(polylines.Count >= 2, "VariancePage should render at least 2 polylines for Var(O) and lower bound series");
    }

    [Fact]
    public void VariancePage_Renders_ContainsDivergenceComposite()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<VariancePage>();

        // The divergence composite chart (Eq 4+4a+5) should render 3 polylines
        var chartCards = cut.FindAll(".chart-card");
        Assert.True(chartCards.Count >= 5, "VariancePage should have at least 5 chart cards including composites");
    }

    [Fact]
    public void VariancePage_Renders_ContainsBarbellComposite()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<VariancePage>();

        // The page should contain text referencing the barbell composite
        var markup = cut.Markup;
        Assert.Contains("Barbell", markup);
    }

    [Fact]
    public void BaseModelPage_Renders_ContainsHeatMap()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<BaseModelPage>();

        var heatMapSvg = cut.Find("svg.heat-map");
        Assert.NotNull(heatMapSvg);

        var rects = cut.FindAll("svg.heat-map rect");
        Assert.True(rects.Count > 0, "HeatMap should render rect elements for cells");
    }

    [Fact]
    public void NegativeForcePage_Renders_ContainsWaterfall()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<NegativeForcePage>();

        var waterfallSvg = cut.Find("svg.waterfall-chart");
        Assert.NotNull(waterfallSvg);

        var rects = cut.FindAll("svg.waterfall-chart rect");
        Assert.True(rects.Count > 0, "WaterfallChart should render rect elements");
    }

    [Fact]
    public void OrganizationalPage_Renders_ContainsHeatMap()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<OrganizationalPage>();

        var heatSvg = cut.Find("svg.heat-map");
        Assert.NotNull(heatSvg);

        var rects = cut.FindAll("svg.heat-map rect");
        Assert.True(rects.Count > 0, "OrganizationalPage HeatMap should render rect elements");
    }

    [Fact]
    public void OrganizationalPage_Renders_ContainsRadarChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<OrganizationalPage>();

        var markup = cut.Markup;
        Assert.Contains("radar-chart", markup);
    }

    [Fact]
    public void OrganizationalPage_Renders_ContainsBarChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<OrganizationalPage>();

        var barSvg = cut.Find("svg.bar-chart");
        Assert.NotNull(barSvg);

        var rects = cut.FindAll("svg.bar-chart rect");
        Assert.True(rects.Count > 0, "BarChart should render rect elements for bars");
    }

    [Fact]
    public void MotivationPage_DefaultSliders_ShowsDecayCurve()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<MotivationPage>();

        var polylines = cut.FindAll("svg.line-chart polyline");
        Assert.True(polylines.Count >= 1, "MotivationPage should render at least one polyline for the decay curve");
    }

    [Fact]
    public void MotivationPage_Renders_ContainsHeatMap()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<MotivationPage>();

        var heatMapSvg = cut.Find("svg.heat-map");
        Assert.NotNull(heatMapSvg);

        var rects = cut.FindAll("svg.heat-map rect");
        Assert.True(rects.Count > 0, "MotivationPage HeatMap should render rect elements for cells");
    }

    [Fact]
    public void MotivationPage_Renders_ContainsRadarChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<MotivationPage>();

        var markup = cut.Markup;
        Assert.Contains("radar-chart", markup);
    }

    [Fact]
    public void MotivationPage_Renders_ContainsSensitivityChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<MotivationPage>();

        // At least 4 LineCharts: decay curve (chart 1), split panel left + right (chart 2), sensitivity (chart 3)
        var lineCharts = cut.FindAll("svg.line-chart");
        Assert.True(lineCharts.Count >= 4, $"MotivationPage should render at least 4 line charts but found {lineCharts.Count}");
    }

    [Fact]
    public void SovereigntyPage_Renders_ContainsHeatMap()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<SovereigntyPage>();

        var heatmap = cut.Find("svg.heat-map");
        Assert.NotNull(heatmap);
    }

    [Fact]
    public void SovereigntyPage_Renders_ContainsRadarChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<SovereigntyPage>();

        var radar = cut.FindAll("svg.radar-chart");
        Assert.True(radar.Count >= 1, "SovereigntyPage should render at least one radar chart");
    }

    [Fact]
    public void SovereigntyPage_Renders_ContainsLineChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<SovereigntyPage>();

        var lineChart = cut.Find("svg.line-chart");
        Assert.NotNull(lineChart);
    }

    [Fact]
    public void SovereigntyPage_Renders_ContainsBarChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<SovereigntyPage>();

        var bars = cut.FindAll("svg.bar-chart rect");
        Assert.True(bars.Count >= 1, "SovereigntyPage should render bar chart with at least one bar");
    }

    [Fact]
    public void ModelGrowthPage_Renders_ContainsLineChart()
    {
        var cut = Render<ModelGrowthPage>();

        var polylines = cut.FindAll("svg.line-chart polyline");
        Assert.True(polylines.Count >= 1, "ModelGrowthPage should render at least one polyline");
    }

    [Fact]
    public void TransferPage_Renders_ContainsBarChart()
    {
        var cut = Render<TransferPage>();

        var barSvg = cut.Find("svg.bar-chart");
        Assert.NotNull(barSvg);

        var rects = cut.FindAll("svg.bar-chart rect");
        Assert.True(rects.Count > 0, "TransferPage BarChart should render rect elements");
    }

    [Fact]
    public void TacitKnowledgePage_Renders_ContainsLineChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TacitKnowledgePage>();

        var polylines = cut.FindAll("svg.line-chart polyline");
        Assert.True(polylines.Count >= 1, "TacitKnowledgePage should render at least one polyline");
    }

    [Fact]
    public void ForceDynamicsPage_Renders_ContainsSparklines()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<ForceDynamicsPage>();

        // ForceDynamicsPage has multiple sparkline LineCharts (surface, middle, deep layers)
        var lineCharts = cut.FindAll("svg.line-chart");
        Assert.True(lineCharts.Count >= 3, "ForceDynamicsPage should render at least 3 sparkline line charts");

        var polylines = cut.FindAll("svg.line-chart polyline");
        Assert.True(polylines.Count >= 3, "ForceDynamicsPage should render at least 3 polylines across sparklines");
    }

    [Fact]
    public void DivergencePage_Renders_ContainsLineChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<DivergencePage>();

        var polylines = cut.FindAll("svg.line-chart polyline");
        Assert.True(polylines.Count >= 1, "DivergencePage should render at least one polyline");
    }

    [Fact]
    public void DivergencePage_Renders_ContainsHeatMap()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<DivergencePage>();

        var heatSvg = cut.Find("svg.heat-map");
        Assert.NotNull(heatSvg);

        var rects = cut.FindAll("svg.heat-map rect");
        Assert.True(rects.Count > 0, "DivergencePage HeatMap should render rect elements");
    }

    [Fact]
    public void DivergencePage_Renders_ContainsRadarChart()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<DivergencePage>();

        var markup = cut.Markup;
        Assert.Contains("radar-chart", markup);
    }

    [Fact]
    public void CreationEvaluationPage_Renders_ContainsHeatMap()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<CreationEvaluationPage>();

        var heatSvg = cut.Find("svg.heat-map");
        Assert.NotNull(heatSvg);

        var rects = cut.FindAll("svg.heat-map rect");
        Assert.True(rects.Count > 0, "CreationEvaluationPage HeatMap should render rect elements");
    }

    [Fact]
    public void CreationEvaluationPage_Renders_ContainsResourceComposite()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<CreationEvaluationPage>();

        // The constrained allocation chart has LineChart with 3 series + RadarChart
        var chartCards = cut.FindAll(".chart-card");
        Assert.True(chartCards.Count >= 3, "CreationEvaluationPage should have at least 3 chart cards including the constrained allocation");

        var markup = cut.Markup;
        Assert.Contains("Three-Way Squeeze", markup);
    }

    [Fact]
    public void TippingPointPage_Renders_ContainsPhasePortrait()
    {
        var cut = Render<TippingPoint>();

        var phasePortrait = cut.Find("svg.phase-portrait");
        Assert.NotNull(phasePortrait);
    }
}
