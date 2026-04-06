# fstar

An interactive simulation of the equation framework from [*The Multiplier, Mirror and The Tipping Point*](https://realization-engine.github.io/fstar/docs/The_Multiplier_and_the_Mirror.html) by Dennis A. Landi.

## The Framework

The paper asks what happens when LLMs amplify human capability, and finds that the answer depends on the capability itself. In the expression $O = M \times F$, $M$ is the LLM (the multiplier) and $F$ is the human (the FORCE). $F$ is not a single number; it is a composite of domain expertise, architectural judgment, taste, debugging intuition, calibrated uncertainty, and intrinsic motivation, combined multiplicatively so that a zero in any critical component collapses the whole product (Eq. 1, Cobb-Douglas form).

Three concepts organize the framework:

- **The Multiplier** describes magnitude. The LLM scales output in proportion to what the human brings.
- **The Mirror** describes mechanism. The LLM reflects the user's input back along two channels: a *substance* channel (whose fidelity scales with the user's FORCE) and a *presentation* channel (which always polishes, regardless of substance). The gap between these channels is the source of epistemic risk.
- **The Tipping Point** describes direction. There exists a threshold $F^*$ (Eq. 14) above which the mirror functions as a feedback instrument for growth, and below which it flatters, confirms, and accelerates decline. The same tool, the same user, entirely different long-term trajectory.

FORCE has three layers with different half-lives and different relationships to the LLM:

| Layer | Contents | Half-life | LLM substitution |
|-------|----------|-----------|------------------|
| Surface | Syntax, APIs, tool configs | Months | Near-complete |
| Middle | Judgment, taste, pattern recognition | Years | Partial |
| Deep | Structural intuition, felt sense of failure | Decades | Near-zero |

The paper derives 32 equations across these topics:

- **Variance amplification** (Eqs. 4-5): Output variance scales as $M^2$, not $M$. LLMs are a divergence engine, not a leveler.
- **The barbell effect** (Eq. 6): Labor markets split into three tiers; the middle hollows out.
- **Creation vs. evaluation** (Eqs. 7-7a): Creation cost collapses; evaluation becomes the binding constraint.
- **Negative force** (Eqs. 8-10): An engineer with a wrong mental model and an LLM builds the wrong thing faster.
- **Atrophy dynamics** (Eqs. 11, 11a-c): FORCE evolves under four pressures: traditional struggle, deliberate LLM engagement (which compounds), passive reliance (which decays), and organizational de-investment.
- **Tacit knowledge pipeline** (Eqs. 12-13): Knowledge transmission depends on shared work between seniors and juniors; shared work decays exponentially with $M$.
- **The tipping point** (Eqs. 14, 14a): The bifurcation where amplification flips from compounding to erosion. Hysteresis makes recovery harder than descent.
- **Divergence trajectories** (Eqs. 15-16a): The gap between high-force and low-force individuals widens, and widens faster over time.
- **Organizational dynamics** (Eqs. 17-22): ROI paradox, legibility crisis, Goodhart's trap, decision bottleneck, competitive moats.
- **Motivation decay** (Eq. 23): Intrinsic motivation decays with accumulated autonomy loss and enters FORCE multiplicatively.
- **Sovereignty** (Eqs. 24-24a): National capability is discounted by the probability of continued access to a foreign-owned multiplier.
- **Model growth** (Eq. 25): $M$ grows exponentially, but most dynamics are convex in $M$, so problems compound faster as the technology improves.
- **F-to-M transfer** (Eqs. 26-31): FORCE flows into the model through RLHF, fine-tuning, and training signal. Successful transfer raises $F^*$, paradoxically pushing more people below the threshold.
- **Cohort discontinuity** (Eq. 32): Post-LLM cohorts enter with a lower FORCE ceiling because the environmental conditions for building FORCE have been structurally altered.

These interact through seven reinforcing feedback loops (the Cascade), producing three terminal trajectories for the coupled $F(t)$/$M(t)$ system: a virtuous regime where both grow, managed decline where a lower equilibrium is reached, and a collapse spiral where both degrade.

## The Application

This repo implements all 32 equations as an interactive Blazor WebAssembly application. Every equation is a pure C# function in a shared library, tested independently, and visualized on dedicated pages with parameter sliders and scenario presets (Virtuous Cycle, Managed Decline, Collapse Spiral).

### Tech Stack

- .NET 10.0 / Blazor WebAssembly (runs entirely in-browser)
- Custom SVG chart components (no external charting libraries)
- KaTeX for LaTeX equation rendering
- Forward Euler numerical integration for coupled ODEs

### Project Structure

```
src/
  FStarEquations/           Core equation library (13 modules, all 32 equations)
  FStarEquationsTests/      Unit tests for every equation family
  FStar.UI/                 Reusable chart and control components
  FStarEquations.App/       Blazor host application (18 pages)
  FStarEquations.App.Tests/ UI component tests
Docs/                       The article and supporting documents
```

### Equation Modules

| Module | Equations | What it computes |
|--------|-----------|------------------|
| BaseModel | 1, 1a, 2, 3 | Cobb-Douglas FORCE, layer ordering, cross-domain output, skill value sensitivity |
| VarianceAmplification | 4, 4a, 5, 6 | Output variance, correlated variance, output gap, barbell market value |
| CreationEvaluation | 7, 7a | Evaluation throughput, high-force reallocation |
| NegativeForce | 8, 9, 10 | Directed force, damage scaling, epistemic gap |
| ForceDynamics | 11, 11a-c, 14, 14a | Force ODE, layered decay, tipping point, hysteresis detection |
| TacitKnowledge | 12, 12a, 12b, 13 | Knowledge stock, transmission, shared work decay, pipeline break |
| DivergenceTrajectories | 15a-b, 16, 16a, 32 | High/low-force trajectories, gap widening/acceleration, cohort step-down |
| OrganizationalDynamics | 17-22 | ROI, assessment SNR, Goodhart gaming, throughput, indecision cost, competitive advantage |
| Motivation | 23 | Motivation decay with accumulated autonomy loss |
| Sovereignty | 24, 24a | National capability, sovereign resilience |
| ModelGrowth | 25 | Exponential multiplier expansion |
| ForceToModelTransfer | 26-31 | Transfer rate by layer, absorption ceiling, resource competition, bus factor, tipping point rise, data quality spiral |
| Integration | - | Forward Euler solvers for single-variable and multi-variable ODE systems |

### Visualization Pages

| Page | Route | Focus |
|------|-------|-------|
| Home | `/` | Navigation hub |
| Force is Not a Number | `/base-model` | Eq. 1: Cobb-Douglas force, radar profiles, layer substitution |
| Variance Amplifier | `/variance` | Eqs. 4-6: Variance scaling, output gap, barbell distribution |
| Creation vs. Evaluation | `/creation-evaluation` | Eqs. 7-7a: Evaluation bottleneck, force reallocation |
| Negative Force | `/negative-force` | Eqs. 8-10: Damage scaling, epistemic gap |
| The Atrophy Problem | `/force-dynamics` | Eqs. 11, 11a-c: Layered decay, phase portrait |
| Tacit Knowledge | `/tacit-knowledge` | Eqs. 12-13: Knowledge stock, transmission, pipeline break |
| The Tipping Point | `/tipping-point` | Eqs. 14, 14a: Bifurcation, hysteresis, sensitivity analysis |
| Accelerating Gap | `/divergence` | Eqs. 15-16a, 32: Divergence trajectories, cohort discontinuity |
| Organizational Consequences | `/organizational` | Eqs. 17-22: ROI, SNR, throughput, competitive advantage |
| The Meaning Problem | `/motivation` | Eq. 23: Motivation decay |
| Sovereignty | `/sovereignty` | Eqs. 24-24a: National capability, resilience |
| Model Growth | `/model-growth` | Eq. 25: Exponential M(t) growth |
| F-to-M Transfer | `/transfer` | Eqs. 26-31: Transfer dynamics, data quality spiral |
| Terminal Dynamics | `/terminal-dynamics` | Eqs. 11, 25, 31: Phase portrait of coupled F/M system |
| The Cascade | `/cascade` | All equations: Interactive feedback loop diagram |
| Full Timeline | `/timeline` | All ODEs: 20-year stacked time-series simulation |

### Chart Types

Nine custom SVG chart components: line charts, heatmaps, bar charts, phase portraits, radar charts, number lines, tornado diagrams, waterfall charts, and an animated time-series player. All are interactive with tooltips, crosshairs, and parameter-driven updates.

## Links

- [Live Application](https://realization-engine.github.io/fstar/)
- [The Article (rendered)](https://realization-engine.github.io/fstar/docs/The_Multiplier_and_the_Mirror.html)
- [Citations](https://realization-engine.github.io/fstar/docs/The_Multiplier_and_the_Mirror_Citations.html)

## Running Locally

```bash
dotnet build src/FStarEquations.slnx
dotnet test src/FStarEquations.slnx
dotnet run --project src/FStarEquations.App
```

---

*Build the FORCE. The multiplication takes care of itself.*
