using Bunit;
using Xunit;
using FStarEquations.App.Pages;

namespace FStarEquations.App.Tests.Pages;

public class TheoryCardTests : BunitContext
{
    [Fact]
    public void TransferPage_ChartCards_HaveTheoryTabs()
    {
        var cut = Render<TransferPage>();

        var tabBars = cut.FindAll(".chart-card-tabs");
        Assert.True(tabBars.Count >= 7, $"Expected at least 7 tab bars but found {tabBars.Count}");
    }

    [Fact]
    public void BaseModelPage_ChartCards_HaveTheoryTabs()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<BaseModelPage>();

        var tabBars = cut.FindAll(".chart-card-tabs");
        Assert.True(tabBars.Count >= 2, $"Expected at least 2 tab bars but found {tabBars.Count}");
    }

    [Fact]
    public void VariancePage_TheoryTab_ContainsKatexBlock()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        var cut = Render<VariancePage>();

        // Click the first Theory tab
        var theoryTabs = cut.FindAll(".tab");
        var theoryTab = theoryTabs.First(t => t.TextContent.Contains("Theory"));
        theoryTab.Click();

        // Verify KatexBlock span is present in the rendered theory content
        var katexSpans = cut.FindAll("span.katex-display-block, span.katex-inline-block");
        Assert.True(katexSpans.Count >= 1, "Expected at least one KatexBlock span in theory content");
    }

    [Fact]
    public void CascadeDashboard_HasTheoryTab()
    {
        var cut = Render<CascadeDashboard>();

        var tabBars = cut.FindAll(".chart-card-tabs");
        Assert.True(tabBars.Count >= 1, $"Expected at least 1 tab bar but found {tabBars.Count}");
    }
}
