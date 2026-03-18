# fstar

**fstar** — *Validating and operationalizing the Multiplier and Mirror framework*

F* is the tipping point. Above it, LLMs compound your capability. Below it, they erode it. This repo models which side you're on.

`fstar` implements the equation framework from *The Multiplier and the Mirror*, a formal analysis of how LLMs interact with human capability across individuals, organizations, and nations. The framework models force ($F$) — the composite human capability that LLMs amplify — and tracks how it evolves over time through atrophy, compounding, knowledge transfer, and generational change.

The core insight: LLMs are not just multipliers. They are mirrors that reflect what you bring, record what they reflect, and reshape the conditions under which future force is built. The framework captures this in 46 interconnected equations spanning individual cognition, organizational knowledge dynamics, labor market effects, and geopolitical dependency.

This repo aims to:

- **Simulate** the coupled $F(t)$ and $M(t)$ system under different parameter regimes — virtuous, managed decline, and collapse spiral trajectories
- **Validate** the framework's falsifiable predictions against empirical data (output variance post-LLM adoption, cohort performance differentials, evaluation bottleneck metrics)
- **Operationalize** key equations as diagnostic tools — estimating $F^*$ for a given environment, modeling layered force decay rates, projecting tacit knowledge pipeline thresholds
- **Visualize** the phase-plane dynamics, the cohort distribution evolution, the cascade feedback loops, and the sensitivity of outcomes to intervention timing

Key equations implemented include the Cobb-Douglas force model (Eq 1), the atrophy dynamics with layered decay (Eq 11, 11a-c), the tipping point bifurcation with hysteresis (Eq 14, 14a), the F→M transfer with diminishing returns (Eq 26-27), the data quality spiral (Eq 31), and the cohort discontinuity (Eq 32).

The framework generates specific, testable predictions. This repo is where those predictions meet data.

## Links

- [Live App](https://limbodancer-canary.github.io/fstar/)
- [The Multiplier and the Mirror](https://limbodancer-canary.github.io/fstar/docs/The_Multiplier_and_the_Mirror.html) (rendered article)
- [Citations](https://limbodancer-canary.github.io/fstar/docs/The_Multiplier_and_the_Mirror_Citations.html)

Built as a companion to the article. Intended as a tool for anyone, from engineering leaders to educators to policymakers, trying to navigate the force dynamics of an LLM-augmented world.

*Build the force. The multiplication takes care of itself.*
