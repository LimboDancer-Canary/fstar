using Bunit;
using Xunit;
using FStarEquations.App.Pages;

namespace FStarEquations.App.Tests.Pages;

public class PageRenderTests : BunitContext
{
    [Fact]
    public void HomePage_Renders_WithoutThrowing()
    {
        var cut = Render<Home>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void TippingPointPage_Renders_WithoutThrowing()
    {
        // TippingPoint uses NumberLine, TornadoDiagram, PhasePortrait components
        // being built by Agent 1 concurrently. If this fails to compile, skip it.
        var cut = Render<TippingPoint>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void BaseModelPage_Renders_WithoutThrowing()
    {
        var cut = Render<BaseModelPage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void VariancePage_Renders_WithoutThrowing()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = Render<VariancePage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void CreationEvaluationPage_Renders_WithoutThrowing()
    {
        var cut = Render<CreationEvaluationPage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void NegativeForcePage_Renders_WithoutThrowing()
    {
        var cut = Render<NegativeForcePage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void ForceDynamicsPage_Renders_WithoutThrowing()
    {
        var cut = Render<ForceDynamicsPage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void TacitKnowledgePage_Renders_WithoutThrowing()
    {
        var cut = Render<TacitKnowledgePage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void DivergencePage_Renders_WithoutThrowing()
    {
        var cut = Render<DivergencePage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void OrganizationalPage_Renders_WithoutThrowing()
    {
        var cut = Render<OrganizationalPage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void MotivationPage_Renders_WithoutThrowing()
    {
        var cut = Render<MotivationPage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void SovereigntyPage_Renders_WithoutThrowing()
    {
        var cut = Render<SovereigntyPage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void ModelGrowthPage_Renders_WithoutThrowing()
    {
        var cut = Render<ModelGrowthPage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void TransferPage_Renders_WithoutThrowing()
    {
        var cut = Render<TransferPage>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void CascadeDashboard_Renders_WithoutThrowing()
    {
        var cut = Render<CascadeDashboard>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void TerminalDynamics_Renders_WithoutThrowing()
    {
        var cut = Render<TerminalDynamics>();
        Assert.NotNull(cut.Find("h1"));
    }

    [Fact]
    public void TimelineDashboard_Renders_WithoutThrowing()
    {
        var cut = Render<TimelineDashboard>();
        Assert.NotNull(cut.Find("h1"));
    }
}
