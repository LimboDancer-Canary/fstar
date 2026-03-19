using Bunit;
using Xunit;
using FStarEquations.App.Pages;

namespace FStarEquations.App.Tests.Pages;

public class SliderInteractionTests : BunitContext
{
    [Fact]
    public void VariancePage_HasFloatingSliderPill()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<VariancePage>();
        var pill = cut.Find(".floating-slider-pill");
        Assert.Contains("Variance", pill.TextContent);
    }

    [Fact]
    public void VariancePage_ClickPill_ShowsSliders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<VariancePage>();
        cut.Find(".floating-slider-pill").Click();
        var sliders = cut.FindAll("input[type=range]");
        Assert.NotEmpty(sliders);
        Assert.Equal("0.5", sliders[0].GetAttribute("min"));
    }

    [Fact]
    public void BaseModelPage_ClickPill_ShowsSliders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<BaseModelPage>();
        cut.Find(".floating-slider-pill").Click();
        var sliders = cut.FindAll("input[type=range]");
        Assert.NotEmpty(sliders);
        // First slider is the X-axis component selector (min=1, max=7)
        Assert.Equal("1", sliders[0].GetAttribute("min"));
        Assert.Equal("7", sliders[0].GetAttribute("max"));
    }

    [Fact]
    public void OrganizationalPage_ClickPill_ShowsSliders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<OrganizationalPage>();
        cut.Find(".floating-slider-pill").Click();
        var sliders = cut.FindAll("input[type=range]");
        Assert.NotEmpty(sliders);
    }

    [Fact]
    public void TippingPointPage_ClickPill_ShowsSliders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TippingPoint>();
        cut.Find(".floating-slider-pill").Click();
        var sliders = cut.FindAll("input[type=range]");
        Assert.NotEmpty(sliders);
    }

    [Fact]
    public void ModelGrowthPage_ClickPill_ShowsSliders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<ModelGrowthPage>();
        cut.Find(".floating-slider-pill").Click();
        var sliders = cut.FindAll("input[type=range]");
        Assert.NotEmpty(sliders);
    }

    [Fact]
    public void TransferPage_ClickPill_ShowsSliders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<TransferPage>();
        cut.Find(".floating-slider-pill").Click();
        var sliders = cut.FindAll("input[type=range]");
        Assert.NotEmpty(sliders);
    }

    [Fact]
    public void ForceDynamicsPage_ClickPill_ShowsSliders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<ForceDynamicsPage>();
        cut.Find(".floating-slider-pill").Click();
        var sliders = cut.FindAll("input[type=range]");
        Assert.NotEmpty(sliders);
    }

    [Fact]
    public void MotivationPage_ClickPill_ShowsGroupHeaders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<MotivationPage>();
        cut.Find(".floating-slider-pill").Click();
        var headers = cut.FindAll(".slider-group-header");
        Assert.True(headers.Count >= 2);
    }

    [Fact]
    public void NegativeForcePage_ClickPill_ShowsSliders()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<NegativeForcePage>();
        cut.Find(".floating-slider-pill").Click();
        var sliders = cut.FindAll("input[type=range]");
        Assert.NotEmpty(sliders);
    }
}
