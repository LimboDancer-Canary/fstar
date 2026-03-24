using Bunit;
using Xunit;
using FStar.UI.Components.Charts;
using FStar.UI.Models;

namespace FStarEquations.App.Tests.Components.Charts;

public class TimeSeriesAnimatorTests : BunitContext
{
    private static List<AnimatedSeries> SampleSeries() => new()
    {
        new("Sine", Enumerable.Range(0, 100).Select(i => Math.Sin(i * 0.1)).ToArray(), "#2563eb")
    };

    [Fact]
    public void Renders_ContainsCanvasAndControls()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.TimeSeries, new List<AnimatedSeries>())
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1));

        Assert.NotNull(cut.Find("canvas"));
        Assert.NotNull(cut.Find(".time-series-controls"));
    }

    [Fact]
    public void WithDimensions_SetsCanvasSize()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.Width, 1024)
            .Add(x => x.Height, 768)
            .Add(x => x.TimeSeries, new List<AnimatedSeries>())
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1));

        var canvas = cut.Find("canvas");
        Assert.Equal("1024", canvas.GetAttribute("width"));
        Assert.Equal("768", canvas.GetAttribute("height"));
    }

    [Fact]
    public void PlayButton_FiresIsAnimatingChanged()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var isAnimating = false;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.TimeSeries, SampleSeries())
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1)
            .Add(x => x.IsAnimating, isAnimating)
            .Add(x => x.IsAnimatingChanged, val => isAnimating = val));

        cut.Find("button[aria-label='Play animation']").Click();

        Assert.True(isAnimating);
    }

    [Fact]
    public void ResetButton_SetsCurrentTimeToZero()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var currentTime = 5.0;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.TimeSeries, SampleSeries())
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1)
            .Add(x => x.CurrentTime, currentTime)
            .Add(x => x.CurrentTimeChanged, val => currentTime = val));

        cut.Find("button[aria-label='Reset animation']").Click();

        Assert.Equal(0, currentTime);
    }

    [Fact]
    public void ResetButton_StopsAnimation()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var isAnimating = true;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.TimeSeries, SampleSeries())
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1)
            .Add(x => x.IsAnimating, isAnimating)
            .Add(x => x.IsAnimatingChanged, val => isAnimating = val));

        cut.Find("button[aria-label='Reset animation']").Click();

        Assert.False(isAnimating);
    }

    [Fact]
    public void EmptySeriesData_RendersWithoutError()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.TimeSeries, new List<AnimatedSeries>())
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1));

        Assert.NotNull(cut.Find("canvas"));
        Assert.Equal(2, cut.FindAll("button").Count);
    }

    [Fact]
    public void NullSeriesData_RendersWithoutError()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1));

        Assert.NotNull(cut.Find("canvas"));
    }

    [Fact]
    public void HasAccessibilityAttributes()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.TimeSeries, SampleSeries())
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1));

        var canvas = cut.Find("canvas");
        Assert.Equal("img", canvas.GetAttribute("role"));
        Assert.Equal("Animated time series chart", canvas.GetAttribute("aria-label"));

        var buttons = cut.FindAll("button");
        Assert.All(buttons, b => Assert.NotNull(b.GetAttribute("aria-label")));
    }

    [Fact]
    public void ShowsTimeStatus()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TimeSeriesAnimator>(p => p
            .Add(x => x.TimeSeries, SampleSeries())
            .Add(x => x.Duration, 10)
            .Add(x => x.Dt, 0.1)
            .Add(x => x.CurrentTime, 3.5));

        var status = cut.Find(".time-series-status");
        Assert.Contains("3.50", status.TextContent);
        Assert.Contains("10.0", status.TextContent);
    }
}
