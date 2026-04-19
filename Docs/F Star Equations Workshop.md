# F* Equations Workshop

*Worked examples for every equation in* **The Multiplier, Mirror and The Tipping Point**

*Companion to* [How to Read the Equations](How-to-Read-the-Equations.md) *and* [The Multiplier and the Mirror](The_Multiplier_and_the_Mirror.md)

---

The following examples use concrete numbers to walk through every equation in the framework. The numbers are illustrative, chosen to make the arithmetic easy to follow. In practice, the exact values would be calibrated to a specific organization or context; what matters here is the *shape* of the result, not the specific digits.

A recurring cast of characters appears throughout:

- **Engineer A ("Ada")**: a senior engineer with 15 years of experience, high Force across all layers.
- **Engineer B ("Ben")**: a mid-level engineer, competent but undistinguished, moderate Force.
- **Engineer C ("Cora")**: a post-LLM junior, 18 months of experience, low Force and still building.

---

## Eq. 1: The Base Model (Cobb-Douglas Force)

$$O = M \times F \quad \text{where} \quad F = \prod_{i} f_i^{w_i}$$

Suppose Ada has seven capability components, each scored on a 0-to-1 scale, with equal weights ($w_i = 1/7 \approx 0.143$ each):

| Component ($f_i$) | Score | Raised to $w_i$ ($f_i^{0.143}$) |
|-------------------|-------|----------------------------------|
| Domain expertise | 0.90 | $0.90^{0.143} = 0.985$ |
| Architectural judgment | 0.85 | $0.85^{0.143} = 0.977$ |
| Taste | 0.80 | $0.80^{0.143} = 0.968$ |
| Clarity of specification | 0.75 | $0.75^{0.143} = 0.959$ |
| Debugging intuition | 0.90 | $0.90^{0.143} = 0.985$ |
| Calibrated uncertainty | 0.85 | $0.85^{0.143} = 0.977$ |
| Intrinsic motivation | 0.95 | $0.95^{0.143} = 0.993$ |

$$F_{\text{Ada}} = 0.985 \times 0.977 \times 0.968 \times 0.959 \times 0.985 \times 0.977 \times 0.993 \approx 0.85$$

With a multiplier of $M = 3$: $O_{\text{Ada}} = 3 \times 0.85 = 2.55$

**Now set one component to near-zero.** Suppose calibrated uncertainty drops to 0.05 (the engineer is confidently wrong and does not know it):

$$0.05^{0.143} = 0.65$$

This single factor pulls $F$ from $0.85$ down to approximately $0.57$. Output drops from $2.55$ to $1.71$. A collapse in one component dragged the entire product down by a third, even though six other components remained strong. That is the multiplicative structure at work: a chain is only as strong as its weakest link.

If that component dropped to 0.01: $0.01^{0.143} = 0.50$. Now $F \approx 0.44$ and output is $1.32$, roughly half of what it was. One near-zero component has halved total output.

**Compare with Ben**, whose scores are uniformly moderate (all components at 0.60):

$$F_{\text{Ben}} = (0.60^{0.143})^7 = (0.927)^7 \approx 0.59$$

$$O_{\text{Ben}} = 3 \times 0.59 = 1.77$$

Ben's output is 70% of Ada's, despite each of his components being only 10-35% lower than Ada's. The multiplicative form amplifies moderate weakness across many dimensions into a substantial overall gap.

---

## Eq. 1a: LLM Substitution by Layer

$$M_{\text{effective}}^{\text{surface}} \gg M_{\text{effective}}^{\text{middle}} > M_{\text{effective}}^{\text{deep}} \approx 1$$

Assign illustrative multiplier values to each layer:

| Layer | Example $M_{\text{effective}}$ | What This Means |
|-------|-------------------------------|-----------------|
| Surface (syntax, APIs, configs) | 20 | The LLM handles nearly all of this; asking it to write a boilerplate REST endpoint is 20x faster than writing it from memory |
| Middle (judgment, taste, pattern recognition) | 2 | The LLM helps somewhat; it can suggest architectural patterns, but the engineer must evaluate which one fits |
| Deep (structural intuition, Spidey-sense) | 1.1 | The LLM barely helps; diagnosing why a distributed system fails under a specific load pattern requires experience the model does not have |

When Ada needs to recall the exact syntax for configuring an ASP.NET middleware pipeline, $M_{\text{effective}}^{\text{surface}} = 20$ makes the task trivial. When she needs to decide whether to use event sourcing or CRUD for a new service, $M_{\text{effective}}^{\text{middle}} = 2$ provides useful input she still must judge. When a production outage hits at 2 AM and the logs show a pattern she has seen once before in 2017, $M_{\text{effective}}^{\text{deep}} \approx 1$; she is on her own.

**The practical consequence**: the LLM is most powerful where the skill is least durable, and least powerful where the skill is most durable. Surface knowledge (half-life: months) gets the biggest multiplier. Deep knowledge (half-life: decades) gets almost none.

---

## Eq. 2: Output Across Domains

$$O = \sum_{d} M_s(d) \times F(d)$$

Suppose Ada works across three domains in a given sprint:

| Domain ($d$) | $M_s(d)$ | $F_{\text{Ada}}(d)$ | Contribution |
|-------------|----------|---------------------|-------------|
| CRUD API endpoints | 10 | 0.70 | $10 \times 0.70 = 7.0$ |
| Distributed cache architecture | 1.5 | 0.90 | $1.5 \times 0.90 = 1.35$ |
| Race condition diagnosis | 0.7 | 0.85 | $0.7 \times 0.85 = 0.60$ |

$$O_{\text{Ada}} = 7.0 + 1.35 + 0.60 = 8.95$$

Notice the race condition domain: $M_s = 0.7$, which is *below 1*. The LLM is a net negative there. Ada would have produced $1 \times 0.85 = 0.85$ units of value without it. With it, she produces only 0.60, because the LLM's confident but wrong suggestions cost her time.

**Now run the same sprint for Cora** (the junior):

| Domain ($d$) | $M_s(d)$ | $F_{\text{Cora}}(d)$ | Contribution |
|-------------|----------|----------------------|-------------|
| CRUD API endpoints | 10 | 0.40 | $10 \times 0.40 = 4.0$ |
| Distributed cache architecture | 1.5 | 0.15 | $1.5 \times 0.15 = 0.23$ |
| Race condition diagnosis | 0.7 | 0.10 | $0.7 \times 0.10 = 0.07$ |

$$O_{\text{Cora}} = 4.0 + 0.23 + 0.07 = 4.30$$

In the high-$M_s$ domain (CRUD), Cora does reasonably well; the mirror reflects even modest input into usable output. In the low-$M_s$ domains, her contribution is negligible. The variable multiplier concentrates her value in the one area where the LLM substitutes most completely.

---

## Eq. 3: Provider Investment Reshapes Skill Value

$$\frac{\partial V_s}{\partial I_p} = \frac{\partial M_s(d)}{\partial I_p} \times \bar{F}_s$$

Suppose a model provider invests \$500M in improving the LLM's frontend capabilities. This increases $M_s(\text{frontend})$ from 5 to 8, a gain of 3. The average Force of frontend engineers is $\bar{F}_{\text{frontend}} = 0.70$.

$$\frac{\partial V_{\text{frontend}}}{\partial I_p} = 3 \times 0.70 = 2.1 \text{ units of increased market value}$$

Now suppose the same provider invests \$500M in embedded systems. This increases $M_s(\text{embedded})$ from 1.2 to 1.5, a gain of 0.3 (harder domain, less training data). The average Force of embedded engineers is $\bar{F}_{\text{embedded}} = 0.80$.

$$\frac{\partial V_{\text{embedded}}}{\partial I_p} = 0.3 \times 0.80 = 0.24 \text{ units of increased market value}$$

The same dollar investment produces nearly 9x more market-value shift in frontend than in embedded. The provider's training priorities are an invisible hand: they did not set out to devalue embedded engineers, but the investment differential has that effect. Frontend engineers become more economically productive (their multiplier grew by 3). Embedded engineers see barely any change (their multiplier grew by 0.3).

---

## Eq. 4: Output Variance Scales with M Squared

$$\text{Var}(O) = M^2 \times \text{Var}(F)$$

Suppose a team of five engineers has Force values of: 0.6, 0.8, 1.0, 1.2, 1.4.

The mean is $\bar{F} = 1.0$. The variance is:

$$\text{Var}(F) = \frac{(0.6-1)^2 + (0.8-1)^2 + (1-1)^2 + (1.2-1)^2 + (1.4-1)^2}{5} = \frac{0.16 + 0.04 + 0 + 0.04 + 0.16}{5} = 0.08$$

**Before the LLM** ($M = 1$): $\text{Var}(O) = 1^2 \times 0.08 = 0.08$

**With a 3x multiplier**: $\text{Var}(O) = 3^2 \times 0.08 = 9 \times 0.08 = 0.72$

**With a 5x multiplier**: $\text{Var}(O) = 5^2 \times 0.08 = 25 \times 0.08 = 2.00$

The spread in output went from 0.08 to 0.72 to 2.00. A ninefold increase, then a twenty-fivefold increase. Meanwhile, the mean output only tripled or quintupled. The spread grew much faster than the average. That is the divergence engine: the tool amplifies differences faster than it amplifies the center.

---

## Eq. 4a: Variance Lower Bound with Correlated M and F

$$\text{Var}(O) > M^2 \times \text{Var}(F)$$

Using the same team, suppose the effective multiplier each engineer extracts is correlated with their Force (high-Force engineers ask sharper questions and get sharper answers):

| Engineer | $F$ | Effective $M$ | $O = M \times F$ |
|----------|-----|---------------|-------------------|
| 1 | 0.6 | 2.0 | 1.2 |
| 2 | 0.8 | 2.5 | 2.0 |
| 3 | 1.0 | 3.0 | 3.0 |
| 4 | 1.2 | 3.5 | 4.2 |
| 5 | 1.4 | 4.0 | 5.6 |

Mean output: $\bar{O} = (1.2 + 2.0 + 3.0 + 4.2 + 5.6)/5 = 3.2$

$$\text{Var}(O) = \frac{(1.2-3.2)^2 + (2.0-3.2)^2 + (3.0-3.2)^2 + (4.2-3.2)^2 + (5.6-3.2)^2}{5}$$
$$= \frac{4.0 + 1.44 + 0.04 + 1.0 + 5.76}{5} = \frac{12.24}{5} = 2.45$$

Compare to Eq. 4's prediction with a flat $M = 3$: $\text{Var}(O) = 9 \times 0.08 = 0.72$

The actual variance (2.45) is more than three times the flat-$M$ prediction (0.72). The positive correlation between $M$ and $F$ amplifies divergence well beyond what the simple model predicts. This is why Eq. 4 is a lower bound, not an exact answer.

---

## Eq. 5: Absolute Output Gap

$$\Delta O = M \times (F_{\text{high}} - F_{\text{low}})$$

Ada has $F = 0.85$. Cora has $F = 0.30$. The Force gap is $0.85 - 0.30 = 0.55$.

| Multiplier $M$ | Output gap $\Delta O$ | Ada's output | Cora's output |
|----------------|----------------------|-------------|--------------|
| 1 (no LLM) | $1 \times 0.55 = 0.55$ | 0.85 | 0.30 |
| 3 | $3 \times 0.55 = 1.65$ | 2.55 | 0.90 |
| 5 | $5 \times 0.55 = 2.75$ | 4.25 | 1.50 |
| 10 | $10 \times 0.55 = 5.50$ | 8.50 | 3.00 |

Before the LLM, Ada outproduced Cora by 0.55 units. At $M = 10$, Ada outproduces Cora by 5.50 units. The *absolute* gap has grown tenfold. Both engineers produce more, but the distance between them has become a chasm. This is the concrete face of the divergence engine: a pre-existing difference, amplified linearly by the multiplier.

---

## Eq. 6: Labor Market Splits into Three Tiers

$$V(F) \to \begin{cases} V_{\text{high}} \cdot F & \text{if } F > F_{\text{threshold}} \\ V_{\text{new}} & \text{if orchestration skill is high} \\ \varepsilon & \text{if } F \in [F_{\text{low}}, F_{\text{threshold}}] \end{cases}$$

Set $F_{\text{threshold}} = 0.70$ and $V_{\text{high}} = 200{,}000$ (annual salary premium per unit of Force above threshold). Set $V_{\text{new}} = 120{,}000$ for LLM orchestration roles.

| Person | $F$ | Tier | Market Value |
|--------|-----|------|-------------|
| Ada | 0.85 | Judgment (above threshold) | $200{,}000 \times 0.85 = \$170{,}000$ premium |
| Ben | 0.59 | Commoditized middle | $\approx \varepsilon$ (near zero premium) |
| Cora | 0.30 (but high orchestration) | LLM operation | $\$120{,}000$ (new category) |
| Dave | 0.55 (low orchestration) | Commoditized middle | $\approx \varepsilon$ |

Ada commands a premium because her Force exceeds the judgment threshold. Cora, despite low traditional Force, earns value in a genuinely new category. Ben and Dave, in the competent-but-undistinguished middle, find their market value collapsing. The barbell: value at the top, value in a new bottom-tier role, and a hollowed-out middle.

The structural fragility of Cora's tier: her orchestration skill is surface-layer Force (prompt patterns, API behaviors, context-window management). By Eq. 1a, $M_{\text{effective}}^{\text{surface}}$ is highest for this layer, meaning the LLM is an almost perfect substitute for the skill that defines her role. The role is being created and threatened by the same technology.

---

## Eq. 7: Evaluation as Binding Constraint

$$\text{Throughput} = \frac{\text{Budget}_{\text{eval}}}{C_{\text{evaluate}}}$$

A team has three senior engineers capable of code review (the evaluation budget). Each can review 4 pull requests per day. Total evaluation capacity: 12 PR reviews/day.

**Pre-LLM**: the team generates 10 PRs per day. All 10 get reviewed. Throughput = 10.

**Post-LLM**: the same team now generates 40 PRs per day (the LLM accelerated creation by 4x). But evaluation capacity is still 12 reviews/day; possibly fewer, because LLM-generated code requires *more* careful review (the presentation channel makes defects harder to spot).

$$\text{Throughput} = \frac{12}{1} = 12 \text{ reviewed PRs/day}$$

The other 28 PRs either queue up, ship unreviewed (risky), or get reviewed superficially (even riskier). Creation is no longer the bottleneck. Evaluation is. The team's actual useful output is capped at 12, not 40, no matter how fast the LLM generates code.

---

## Eq. 7a: Optimal Allocation Paradox

$$F_{\text{high}} \to \text{evaluation} \implies F_{\text{high}} \not\to \text{creation}$$

Continuing the example above. Ada is the team's best creator. With $M = 3$ and $F = 0.85$, her creation output is $O = 2.55$ per unit time, the highest on the team.

But the evaluation bottleneck means the team needs Ada reviewing code, not writing it. If Ada spends her day reviewing 4 PRs instead of creating, the team loses 2.55 units of creation output but gains 4 units of review throughput (clearing the bottleneck).

| Ada's allocation | Creation output lost | Review throughput gained | Net effect on team |
|-----------------|---------------------|------------------------|-------------------|
| 100% creation | 0 lost | 0 reviews | Team has 8 reviews/day (only Ben and Cora reviewing) |
| 50/50 split | 1.28 lost | +2 reviews | Team has 10 reviews/day |
| 100% evaluation | 2.55 lost | +4 reviews | Team has 12 reviews/day |

The paradox: the person who produces the most value through creation produces even more value by *not creating*, because she is the bottleneck-clearing evaluator. The optimal allocation sends your best people to the role that produces no visible output.

---

## Eq. 8: Additive Force with Negative Components

$$F_{\text{directed}} = \sum_{i} w_i \cdot f_i \qquad \text{where some } f_i < 0$$

Consider an engineer with a wrong mental model of the system's concurrency design. He believes the service is single-threaded when it is actually multi-threaded with shared mutable state. Score his components (using a -1 to +1 scale):

| Component | $f_i$ | $w_i$ | $w_i \cdot f_i$ |
|-----------|--------|--------|-----------------|
| Domain expertise (wrong model) | -0.60 | 0.20 | -0.12 |
| Architectural judgment | +0.40 | 0.15 | +0.06 |
| Taste | +0.50 | 0.10 | +0.05 |
| Specification clarity | +0.30 | 0.15 | +0.045 |
| Debugging intuition | +0.20 | 0.15 | +0.03 |
| Calibrated uncertainty (overconfident) | -0.40 | 0.15 | -0.06 |
| Motivation | +0.70 | 0.10 | +0.07 |

$$F_{\text{directed}} = -0.12 + 0.06 + 0.05 + 0.045 + 0.03 - 0.06 + 0.07 = +0.065$$

Barely positive. The positive components partially offset the negative ones, but the net Force is near zero. In the multiplicative model (Eq. 1), the near-zero components would collapse $F$ entirely. In the additive model, the wrong mental model and the overconfidence partially cancel out the good components but do not fully zero them. The result is a person whose net contribution is ambiguous: slightly positive in aggregate, but the *direction* of the domain expertise component means his actions on concurrency issues are actively harmful, even though his aggregate score is marginally above zero.

---

## Eq. 9: Damage Scales with Multiplier

$$D = M \times |F_{\text{negative}}| \times \tau$$

Take the engineer above and focus on the concurrency domain where his Force is negative. Suppose $F_{\text{negative}} = -0.60$ in that domain, so $|F_{\text{negative}}| = 0.60$.

| Scenario | $M$ | $\tau$ (weeks unchecked) | $D$ (damage) |
|----------|-----|--------------------------|-------------|
| Pre-LLM, caught in 1 week | 1 | 1 | $1 \times 0.60 \times 1 = 0.60$ |
| Post-LLM, caught in 1 week | 5 | 1 | $5 \times 0.60 \times 1 = 3.00$ |
| Post-LLM, caught in 4 weeks | 5 | 4 | $5 \times 0.60 \times 4 = 12.00$ |
| Post-LLM, caught in 12 weeks | 5 | 12 | $5 \times 0.60 \times 12 = 36.00$ |

Pre-LLM, this engineer could only build the wrong thing as fast as he could type. Damage was 0.60 per week. Post-LLM, the same engineer builds the wrong thing 5x faster, and if the evaluation bottleneck (Eq. 7) delays detection, the damage multiplies by the weeks of unchecked work. At 12 weeks unchecked with a 5x multiplier, damage is 60 times what it would have been pre-LLM with quick detection. Each of the three dimensions, stronger LLM, more wrong engineer, longer detection delay, independently worsens the outcome. Together they multiply.

---

## Eq. 10: The Epistemic Gap

$$\Delta_{\text{epistemic}}(i) \propto \frac{M_p}{M_s(d) \cdot F_i}$$

Compare the epistemic gap for Ada and Cora in two domains. Assume $M_p = 8$ (high; the LLM always produces polished, confident-sounding output).

| Person | Domain | $M_s(d)$ | $F_i$ | $M_p / (M_s \cdot F_i)$ | Gap magnitude |
|--------|--------|----------|--------|--------------------------|--------------|
| Ada | CRUD endpoints | 10 | 0.85 | $8 / (10 \times 0.85) = 0.94$ | Small: output looks good and *is* good |
| Ada | Race condition | 0.7 | 0.85 | $8 / (0.7 \times 0.85) = 13.4$ | Moderate: output looks good but substance is thin |
| Cora | CRUD endpoints | 10 | 0.30 | $8 / (10 \times 0.30) = 2.7$ | Moderate: looks good, substance okay |
| Cora | Race condition | 0.7 | 0.10 | $8 / (0.7 \times 0.10) = 114$ | Enormous: looks brilliant, substance is almost zero |

The gap is widest in the bottom-right cell: a low-Force user working on a domain where the substance multiplier is low. The output *looks* professional ($M_p = 8$ in every case), but the actual substance behind it is negligible. Cora's LLM-generated race condition analysis reads like a senior engineer wrote it. It is not. And Cora lacks the Force to know the difference.

---

## Eq. 11: Force Dynamics (Canonical Form)

$$\frac{dF}{dt} = \alpha \cdot S(t) + \gamma \cdot E(t) \cdot F(t) - \beta \cdot R(t) - \sigma \cdot M_{\text{absorbed}}(t)$$

Use these values for Ada over a given quarter:

| Parameter | Value | Meaning |
|-----------|-------|---------|
| $\alpha$ | 0.2 | Learning rate from struggle |
| $S$ | 0.7 | Ada actively seeks hard problems (high struggle) |
| $\gamma$ | 0.4 | Growth from deliberate engagement |
| $E$ | 0.6 | She uses the LLM as a sparring partner 60% of the time |
| $F$ | 0.85 | Her current Force |
| $\beta$ | 0.3 | Decay from passive reliance |
| $R$ | 0.2 | She relies passively only 20% of the time |
| $\sigma$ | 0.1 | Mild org de-investment |
| $M_{\text{absorbed}}$ | 0.5 | Model has absorbed moderate expertise |

$$\frac{dF}{dt} = (0.2)(0.7) + (0.4)(0.6)(0.85) - (0.3)(0.2) - (0.1)(0.5)$$
$$= 0.14 + 0.204 - 0.06 - 0.05 = +0.234$$

Ada's Force is *growing* at 0.234 per quarter. The growth terms (0.14 from struggle + 0.204 from deliberate engagement) outweigh the decay terms (0.06 from passive reliance + 0.05 from org de-investment). The compounding term ($\gamma E F = 0.204$) is the largest single contributor, and it will get larger as $F$ grows, accelerating the growth.

**Now run the same equation for Cora:**

| Parameter | Value | Meaning |
|-----------|-------|---------|
| $\alpha$ | 0.2 | Same learning rate |
| $S$ | 0.2 | Less struggle available; LLM smooths most friction |
| $\gamma$ | 0.4 | Same engagement coefficient |
| $E$ | 0.2 | Cora uses LLM as oracle, not sparring partner |
| $F$ | 0.30 | Her current Force |
| $\beta$ | 0.3 | Same decay rate |
| $R$ | 0.7 | Heavy passive reliance |
| $\sigma$ | 0.1 | Same org de-investment |
| $M_{\text{absorbed}}$ | 0.5 | Same model capability |

$$\frac{dF}{dt} = (0.2)(0.2) + (0.4)(0.2)(0.30) - (0.3)(0.7) - (0.1)(0.5)$$
$$= 0.04 + 0.024 - 0.21 - 0.05 = -0.196$$

Cora's Force is *declining* at 0.196 per quarter. The decay terms dominate. The compounding term ($\gamma E F = 0.024$) is tiny because both $E$ and $F$ are low. Same equation, same coefficients, radically different outcome. Ada compounds upward. Cora spirals downward.

---

## Eq. 11a: Surface Layer Decay

$$\frac{df_{\text{surface}}}{dt} = -\beta_s \cdot R$$

Let $\beta_s = 0.5$ (surface skills decay quickly) and $R = 0.6$ (moderate passive reliance).

$$\frac{df_{\text{surface}}}{dt} = -(0.5)(0.6) = -0.30 \text{ per quarter}$$

If Ada's surface-layer Force starts at 0.90, after one quarter it drops to $0.90 - 0.30 = 0.60$. After two quarters: $0.60 - 0.30 = 0.30$.

This looks alarming, but the paper says this loss is *benign*. Why? Because by Eq. 1a, the LLM is an almost perfect substitute for the surface layer ($M_{\text{effective}}^{\text{surface}} = 20$). Ada does not need to remember the exact syntax for configuring middleware. The LLM handles it. The surface layer decaying is like forgetting a phone number you have saved in your contacts; the information is still accessible, just not in your head.

---

## Eq. 11b: Middle Layer Dynamics

$$\frac{df_{\text{middle}}}{dt} = \alpha_m \cdot S + \gamma_m \cdot E \cdot f_{\text{middle}} - \beta_m \cdot R$$

This is the critical battleground. Let $\alpha_m = 0.15$, $\gamma_m = 0.3$, $\beta_m = 0.25$.

**Ada** ($S = 0.7$, $E = 0.6$, $f_{\text{middle}} = 0.80$, $R = 0.2$):

$$\frac{df_{\text{middle}}}{dt} = (0.15)(0.7) + (0.3)(0.6)(0.80) - (0.25)(0.2) = 0.105 + 0.144 - 0.05 = +0.199$$

Ada's judgment is growing. The compounding term ($0.144$) is larger than the struggle term ($0.105$), and both dwarf the decay ($0.05$).

**Cora** ($S = 0.2$, $E = 0.2$, $f_{\text{middle}} = 0.20$, $R = 0.7$):

$$\frac{df_{\text{middle}}}{dt} = (0.15)(0.2) + (0.3)(0.2)(0.20) - (0.25)(0.7) = 0.03 + 0.012 - 0.175 = -0.133$$

Cora's judgment is eroding. The decay term ($0.175$) overwhelms both growth terms combined ($0.042$). This is the silent middle-layer atrophy the paper warns about: Cora's judgment is declining, but the presentation channel ($M_p$) keeps her output looking professional, so she may not notice.

---

## Eq. 11c: Deep Layer Dynamics

$$\frac{df_{\text{deep}}}{dt} = \alpha_d \cdot S - \beta_d \cdot R \qquad \text{where } \beta_d \ll \beta_s$$

Let $\alpha_d = 0.05$ (deep Force builds slowly), $\beta_d = 0.02$ (and decays slowly).

**Ada** ($S = 0.7$, $R = 0.2$):

$$\frac{df_{\text{deep}}}{dt} = (0.05)(0.7) - (0.02)(0.2) = 0.035 - 0.004 = +0.031$$

Slow but positive. Ada's structural intuition is still building, primarily through direct struggle. Notice there is no $\gamma$ term; the LLM cannot help build deep Force. Only direct experience does.

**Cora** ($S = 0.2$, $R = 0.7$):

$$\frac{df_{\text{deep}}}{dt} = (0.05)(0.2) - (0.02)(0.7) = 0.01 - 0.014 = -0.004$$

Barely negative. Deep Force decays very slowly ($\beta_d = 0.02$ vs $\beta_s = 0.50$). But Cora never had much deep Force to begin with ($f_{\text{deep}} \approx 0.05$), and the slow decay means she is not building it either. The LLM smoothed away the friction that would have built it.

---

## Eq. 12: Organizational Knowledge Stock

$$K_{\text{tacit}}(t+1) = K_{\text{tacit}}(t) \times (1 - \delta) + T(t)$$

An engineering department has a tacit knowledge stock of $K = 100$ units. The natural decay (retirements, turnover, memory fade) is $\delta = 0.10$, meaning 10% of the stock evaporates each year.

**Year 1**: Transmission $T = 15$ (seniors teaching juniors through shared work).

$$K(1) = 100 \times (1 - 0.10) + 15 = 90 + 15 = 105$$

Knowledge grew. Transmission exceeded decay. The pipeline is healthy.

**Year 2**: Transmission drops to $T = 8$ (LLM has absorbed many shared tasks; seniors and juniors work together less).

$$K(2) = 105 \times 0.90 + 8 = 94.5 + 8 = 102.5$$

Still growing, but barely. The margin between transmission and decay has thinned from 5 to 2.5.

**Year 3**: Transmission drops to $T = 5$.

$$K(3) = 102.5 \times 0.90 + 5 = 92.25 + 5 = 97.25$$

Knowledge is now *declining*. Decay ($10.25$) exceeds transmission ($5$). The pipeline has broken (Eq. 13). And nobody noticed, because the seniors carrying the knowledge are still there, still producing. The decline becomes visible only when they leave.

---

## Eq. 12a: Knowledge Transmission

$$T(t) = \phi \times W(t) \times F_{\text{senior}}(t)$$

Three factors multiply together. Let $\phi = 0.5$ (decent mentorship culture), $W = 0.6$ (moderate shared work), $F_{\text{senior}} = 0.85$.

$$T = 0.5 \times 0.6 \times 0.85 = 0.255$$

**Now zero out one factor.** Suppose shared work drops to $W = 0.05$ (seniors and juniors rarely work together; the LLM handles what used to be pair-programming tasks):

$$T = 0.5 \times 0.05 \times 0.85 = 0.021$$

Transmission has collapsed by 92%, not because the seniors lost their knowledge or the culture got worse, but because the vehicle for transmission (shared work) was eliminated. This is the multiplicative structure again: any factor near zero kills the product.

**Or suppose the seniors have atrophied** ($F_{\text{senior}} = 0.30$, from years of passive reliance), while shared work is restored to $W = 0.6$:

$$T = 0.5 \times 0.6 \times 0.30 = 0.09$$

Still very low. The seniors do not have enough Force to transmit. Bringing them back into shared work does not help if the knowledge they carry has already eroded.

---

## Eq. 12b: Shared Work Decays with M

$$W(t) = W_0 \cdot e^{-\psi \cdot M}$$

Let $W_0 = 1.0$ (normalized baseline) and $\psi = 0.3$ (moderate sensitivity).

| $M$ (multiplier) | $W(t) = e^{-0.3 M}$ | Shared work remaining |
|------------------|---------------------|----------------------|
| 1 (pre-LLM) | $e^{-0.3} = 0.74$ | 74% of baseline |
| 3 | $e^{-0.9} = 0.41$ | 41% |
| 5 | $e^{-1.5} = 0.22$ | 22% |
| 10 | $e^{-3.0} = 0.05$ | 5% |

As the multiplier grows, shared work vanishes exponentially. At $M = 3$, more than half the learning-by-doing opportunities are gone. At $M = 10$, 95% are gone. The tasks that juniors used to learn from, the boilerplate, the framework wiring, the debugging sessions, are the first to be absorbed by the LLM because they are precisely the high-volume, well-specified work where $M_s$ is highest.

---

## Eq. 13: Pipeline Break Condition

$$T(t) < \delta \cdot K_{\text{tacit}}(t) \implies K_{\text{tacit}} \to 0 \text{ over time}$$

Continuing from Eq. 12. The pipeline breaks when transmission falls below decay. With $\delta = 0.10$ and $K = 100$:

$$\delta \cdot K = 0.10 \times 100 = 10$$

Transmission must exceed 10 per year to keep the stock from declining. From Eq. 12a, $T = \phi \times W \times F_{\text{senior}}$. If $\phi = 0.5$ and $F_{\text{senior}} = 0.85$, the minimum shared work needed is:

$$10 = 0.5 \times W \times 0.85 \implies W = \frac{10}{0.425} = 23.5$$

In normalized terms ($W_0 = 30$), that is $W/W_0 = 0.78$. From Eq. 12b, $W/W_0 = e^{-0.3 M}$. Setting $e^{-0.3 M} = 0.78$:

$$-0.3M = \ln(0.78) = -0.248 \implies M = 0.83$$

The pipeline breaks at $M \approx 0.83$. In practice, this means the pipeline is already under strain at *very modest* levels of LLM adoption. By the time $M$ reaches 3 (a conservative estimate for current tools), $W/W_0$ has dropped to 0.41 and transmission has fallen well below the break threshold. The pipeline broke years ago. The stock is already declining. It just has not been noticed yet.

---

## Eq. 14: The Bifurcation Threshold (Tipping Point)

$$F^* = \frac{\beta \cdot R + \sigma \cdot M_{\text{absorbed}}}{\gamma \cdot E}$$

Suppose we have the following parameter values for a given organization and usage pattern:

| Parameter | Value | Meaning |
|-----------|-------|---------|
| $\beta$ | 0.3 | Moderate decay from passive LLM use |
| $R$ | 0.6 | The engineer relies passively about 60% of the time |
| $\sigma$ | 0.1 | Mild organizational pullback on human development |
| $M_{\text{absorbed}}$ | 0.5 | The model has absorbed moderate expertise |
| $\gamma$ | 0.4 | Active engagement is a reasonably effective growth channel |
| $E$ | 0.5 | The engineer actively engages about 50% of the time |

$$F^* = \frac{(0.3)(0.6) + (0.1)(0.5)}{(0.4)(0.5)} = \frac{0.18 + 0.05}{0.20} = \frac{0.23}{0.20} = 1.15$$

The tipping point is $F^* = 1.15$. Any engineer with $F > 1.15$ is in the compounding-growth basin. Any engineer with $F < 1.15$ is in the compounding-decay basin.

**Now watch what happens when passive reliance increases.** If $R$ rises from 0.6 to 0.9:

$$F^* = \frac{(0.3)(0.9) + 0.05}{0.20} = \frac{0.32}{0.20} = 1.60$$

The tipping point jumped from 1.15 to 1.60. Engineers who were safely above the threshold are now below it, not because their Force declined, but because the threshold rose.

**And when deliberate engagement increases.** If $E$ rises from 0.5 to 0.8, with original $R = 0.6$:

$$F^* = \frac{0.23}{(0.4)(0.8)} = \frac{0.23}{0.32} = 0.72$$

The tipping point drops from 1.15 to 0.72. More engineers clear the threshold. Deliberate engagement is the lever that moves the threshold downward, making compounding growth accessible to more people.

---

## Eq. 14a: Hysteresis

$$\left|\frac{dF}{dt}\right|_{\text{decay}} > \left|\frac{dF}{dt}\right|_{\text{recovery}} \quad \text{at the same distance from } F^*$$

Suppose $F^* = 1.0$. Compare two engineers equidistant from the tipping point: one at $F = 0.80$ (below by 0.20) and one at $F = 1.20$ (above by 0.20), using Eq. 11's structure.

**Engineer at $F = 1.20$ (above $F^*$, compounding)**:

Using approximate values from the growth basin: $dF/dt = 0.14 + (0.4)(0.6)(1.20) - 0.06 - 0.05 = 0.14 + 0.288 - 0.11 = +0.318$

**Engineer at $F = 0.80$ (below $F^*$, decaying)**:

Using approximate values from the decay basin: $dF/dt = 0.04 + (0.4)(0.2)(0.80) - 0.21 - 0.05 = 0.04 + 0.064 - 0.26 = -0.156$

Now suppose the decaying engineer recognizes the problem and tries to recover. She increases $E$ to 0.6 and reduces $R$ to 0.3:

$$dF/dt = 0.10 + (0.4)(0.6)(0.80) - (0.3)(0.3) - 0.05 = 0.10 + 0.192 - 0.09 - 0.05 = +0.152$$

Recovery rate: $+0.152$. Compare to the uncorrected decay rate: $|-0.156| = 0.156$. The rates are nearly equal. But the recovering engineer had to make a *deliberate behavioral change* (more engagement, less reliance) to achieve that recovery rate. The decaying engineer hit the same rate without trying; passive reliance is the default mode. Decay is effortless; recovery requires discipline. This asymmetry is why $F^*$ is a cliff, not a hill.

And the engineer recovering from $F = 0.80$ does not have the novelty advantage. The first time you struggle through a debugging session, the novelty aids encoding. Re-learning after atrophy lacks that novelty, feels more tedious, and competes against the knowledge that the LLM shortcut exists.

---

## Eq. 15a: High-Force Trajectory

$$\frac{dF_H}{dt} = \alpha \cdot S_0 + \gamma \cdot M \cdot F_H$$

Let $\alpha = 0.2$, $S_0 = 0.5$, $\gamma = 0.1$, $M = 3$, and Ada's $F_H = 0.85$.

$$\frac{dF_H}{dt} = (0.2)(0.5) + (0.1)(3)(0.85) = 0.10 + 0.255 = +0.355$$

After one period, $F_H \approx 0.85 + 0.355 = 1.205$. Next period:

$$\frac{dF_H}{dt} = 0.10 + (0.1)(3)(1.205) = 0.10 + 0.362 = +0.462$$

The rate itself increased from 0.355 to 0.462. The compounding term ($\gamma M F_H$) grows as $F_H$ grows, so the speedometer keeps climbing. Ada is not just growing; she is growing *faster each period*. This is the signature of exponential growth driven by the multiplicative feedback loop.

---

## Eq. 15b: Low-Force Trajectory

$$\frac{dF_L}{dt} = \max\!\left(\alpha \cdot S_0 - \beta \cdot M,\; -\kappa \cdot F_L\right)$$

Let $\alpha = 0.2$, $S_0 = 0.5$, $\beta = 0.15$, $M = 3$, $\kappa = 0.3$, and Cora's $F_L = 0.30$.

First term: $\alpha S_0 - \beta M = (0.2)(0.5) - (0.15)(3) = 0.10 - 0.45 = -0.35$

Second term: $-\kappa F_L = -(0.3)(0.30) = -0.09$

$$\frac{dF_L}{dt} = \max(-0.35,\; -0.09) = -0.09$$

The $\max$ function selects the *less negative* value, capping the decay rate. Cora's Force decays at $-0.09$, not $-0.35$. After one period: $F_L \approx 0.30 - 0.09 = 0.21$. Next period:

$$-\kappa F_L = -(0.3)(0.21) = -0.063$$
$$\frac{dF_L}{dt} = \max(-0.35,\; -0.063) = -0.063$$

The decay rate slows as $F_L$ shrinks (the $-\kappa F_L$ term gets smaller). Cora's Force approaches zero asymptotically but never quite reaches it in the multiplicative model. She does not become *negative*; she becomes *negligible*.

---

## Eq. 16: Gap Widens

$$\frac{d}{dt}(F_H - F_L) = \gamma \cdot M \cdot F_H + \beta \cdot M > 0$$

Using the values from Eqs. 15a and 15b, with $\gamma = 0.1$, $M = 3$, $F_H = 0.85$, $\beta = 0.15$:

$$\frac{d}{dt}(F_H - F_L) = (0.1)(3)(0.85) + (0.15)(3) = 0.255 + 0.45 = 0.705$$

The gap is widening at 0.705 per period. Both terms are strictly positive (they are products of positive numbers), so the gap *always* widens. Ada's compounding growth contributes 0.255. Cora's decay contributes 0.45. The gap widens from both ends: the strong get stronger and the weak get weaker simultaneously.

---

## Eq. 16a: Gap Accelerates

$$\frac{d^2}{dt^2}(F_H - F_L) > 0$$

One period later, $F_H$ has grown to 1.205 (from Eq. 15a). The gap-widening rate becomes:

$$\frac{d}{dt}(F_H - F_L) = (0.1)(3)(1.205) + (0.15)(3) = 0.362 + 0.45 = 0.812$$

The rate increased from 0.705 to 0.812. The acceleration is:

$$\frac{d^2}{dt^2}(F_H - F_L) \approx 0.812 - 0.705 = 0.107 > 0$$

The gap is not just widening; it is widening *faster*. This is because $F_H$ appears in the first term and is itself increasing. Every period, Ada's growth adds more to the gap-widening rate. This is the Matthew Effect: to those who have, more is given. To those who have not, even what they have is taken away.

---

## Eq. 17: Marginal Return Proportional to Force

$$\Delta O_j = (M - 1) \times F_j$$

$(M - 1)$ is the *net* gain from having the LLM. At $M = 4$ (net gain of 3):

| Person | $F_j$ | $\Delta O_j = 3 \times F_j$ | Pre-LLM output | Post-LLM output |
|--------|--------|-------|-----------------|-----------------|
| Ada | 0.85 | 2.55 | 0.85 | 3.40 |
| Ben | 0.59 | 1.77 | 0.59 | 2.36 |
| Cora | 0.30 | 0.90 | 0.30 | 1.20 |

Giving Ada the LLM produces 2.55 units of additional output. Giving Cora the same tool produces 0.90 units. The ROI of Ada's Copilot seat is 2.8 times the ROI of Cora's. Uniform distribution is equitable. It is not optimal. If the organization had limited seats, the rational allocation gives Ada priority.

---

## Eq. 18: Assessment Signal Collapses

$$\text{SNR}_{\text{assessment}} = \frac{\text{Var}(F_{\text{true}})}{\text{Var}(F_{\text{true}}) + M_p^2 \cdot \text{Var}(\varepsilon_{\text{LLM}}) + \text{Var}(\eta)}$$

Let $\text{Var}(F_{\text{true}}) = 0.10$ (true skill spread), $\text{Var}(\varepsilon_{\text{LLM}}) = 0.05$ (LLM presentation noise), and $\text{Var}(\eta) = 0.02$ (other assessment noise).

| $M_p$ | $M_p^2 \cdot 0.05$ | Denominator | SNR |
|--------|-------|-------------|-----|
| 1 (no LLM polish) | 0.05 | $0.10 + 0.05 + 0.02 = 0.17$ | $0.10 / 0.17 = 0.59$ |
| 3 | 0.45 | $0.10 + 0.45 + 0.02 = 0.57$ | $0.10 / 0.57 = 0.18$ |
| 5 | 1.25 | $0.10 + 1.25 + 0.02 = 1.37$ | $0.10 / 1.37 = 0.07$ |
| 10 | 5.00 | $0.10 + 5.00 + 0.02 = 5.12$ | $0.10 / 5.12 = 0.02$ |

At $M_p = 1$ (pre-LLM), a manager can detect about 59% of the true capability signal from observable output. At $M_p = 5$, only 7%. At $M_p = 10$, the signal is down to 2%, effectively zero. Everyone's output looks equally polished. Ada's brilliant design doc and Cora's borrowed-from-the-LLM design doc read with the same fluency and confidence. The manager cannot tell them apart from the output alone.

---

## Eq. 19: Goodhart's Trap

$$F_{\text{measured}} = F_{\text{true}} + \delta_{\text{gaming}}(M_p)$$

Suppose the organization introduces a skills assessment to combat the legibility crisis. Engineers can use LLMs to prepare.

| Person | $F_{\text{true}}$ | Gaming boost $\delta(M_p = 5)$ | $F_{\text{measured}}$ | Measured ranking |
|--------|------|------|------|------|
| Ada | 0.85 | 0.10 (modest; she hardly needs it) | 0.95 | 1st |
| Ben | 0.59 | 0.30 (substantial; LLM helps him rehearse) | 0.89 | 2nd |
| Cora | 0.30 | 0.35 (near-maximum; LLM coaches her extensively) | 0.65 | 3rd |

Without gaming: Ada > Ben > Cora (gap: 0.55). With gaming: Ada > Ben > Cora (gap: 0.30). The ranking is preserved, but the *visible gap* has compressed by nearly half. Ben now appears almost as capable as Ada. Management assigns him to critical-path work based on the assessment. The assessment measured $F_{\text{true}} + \delta(M_p)$, not $F_{\text{true}}$.

As $M_p$ grows, $\delta_{\text{gaming}}$ increases for everyone but especially for lower-Force individuals (they have more room to inflate). The measurement becomes increasingly worthless precisely when it matters most.

---

## Eq. 20: Decision Speed as Binding Constraint

$$\text{Throughput} = \min(R_{\text{decision}},\; M \times R_{\text{execution}})$$

A product team decides what to build at a rate of 5 features per sprint ($R_{\text{decision}} = 5$). The team can execute at a base rate of 3 features per sprint ($R_{\text{execution}} = 3$).

| $M$ | $M \times R_{\text{execution}}$ | Binding constraint | Throughput |
|-----|------|------|------|
| 1 (pre-LLM) | 3 | Execution (3 < 5) | 3 |
| 2 | 6 | Decision speed (5 < 6) | 5 |
| 5 | 15 | Decision speed (5 < 15) | 5 |
| 10 | 30 | Decision speed (5 < 30) | 5 |

Pre-LLM, execution was the bottleneck and the team shipped 3 features. At $M = 2$, the bottleneck flips to decision speed: the team can build 6 features per sprint but can only decide what to build for 5. At $M = 10$, the team has 30 units of execution capacity sitting idle, waiting for decisions. Increasing $M$ further produces zero additional throughput. The binding constraint has shifted entirely from "can we build it?" to "have we decided what to build?"

---

## Eq. 21: Indecision Cost Scales with M

$$\text{OC}_{\text{indecision}} = M \times R_{\text{execution}} \times \Delta t_{\text{delay}}$$

The team from above debates a feature spec for 2 extra days ($\Delta t_{\text{delay}} = 0.4$ sprints).

| $M$ | $R_{\text{execution}}$ | $\Delta t$ | Opportunity cost |
|-----|------|------|------|
| 1 | 3 | 0.4 | $1 \times 3 \times 0.4 = 1.2$ features wasted |
| 3 | 3 | 0.4 | $3 \times 3 \times 0.4 = 3.6$ features wasted |
| 10 | 3 | 0.4 | $10 \times 3 \times 0.4 = 12.0$ features wasted |

The same 2-day delay that cost 1.2 features pre-LLM now costs 12 features at $M = 10$. The cost of indecision scales linearly with the multiplier. Every hour spent debating what to build burns $M$ times more idle execution capacity. Companies that win will be the ones that can *decide* fastest.

---

## Eq. 22: Competitive Advantage as Force Differential

$$A = M \times (F_{\text{firm}} - F_{\text{competitor}})$$

Firm Alpha has average workforce Force of 0.80. Firm Beta has average Force of 0.60. Both subscribe to the same LLM ($M = 5$).

$$A = 5 \times (0.80 - 0.60) = 5 \times 0.20 = 1.0$$

**Pre-LLM** ($M = 1$): $A = 1 \times 0.20 = 0.20$

The advantage quintupled from 0.20 to 1.0, purely because the multiplier amplified an existing Force differential. Alpha did not hire better engineers. Beta did not get worse. The multiplier took a modest existing gap and made it decisive. "We have 500 engineers" stops mattering. "Our engineers are 0.20 Force-units better on average" becomes a 5x-amplified competitive moat.

**When the gap is zero**: If $F_{\text{firm}} = F_{\text{competitor}} = 0.70$, then $A = M \times 0 = 0$ regardless of how large $M$ becomes. A more powerful LLM does not help if the Force differential is zero. Access is equal. Force is not.

---

## Eq. 23: Motivation Decays with Autonomy Loss

$$f_{\text{mot}}(t) = f_{\text{mot}}(0) \times e^{-\lambda \cdot A(t)}$$

Ada started with high motivation ($f_{\text{mot}}(0) = 0.95$). Her sensitivity to autonomy loss is $\lambda = 0.5$ (moderate). Accumulated autonomy loss grows each quarter she spends reviewing LLM output instead of designing systems.

| Quarter | $A(t)$ | $f_{\text{mot}}(t) = 0.95 \times e^{-0.5 A}$ |
|---------|--------|------|
| 0 | 0 | $0.95 \times 1.0 = 0.95$ |
| 2 | 0.5 | $0.95 \times 0.78 = 0.74$ |
| 4 | 1.0 | $0.95 \times 0.61 = 0.58$ |
| 8 | 2.0 | $0.95 \times 0.37 = 0.35$ |
| 12 | 3.0 | $0.95 \times 0.22 = 0.21$ |

After three years of accumulated displacement, Ada's motivation has fallen from 0.95 to 0.21. Since $f_{\text{mot}}$ enters Eq. 1 *multiplicatively*, this drags down her entire Force product. If her other components remained at 0.85, her Force drops from $0.85$ to roughly $0.85 \times (0.21/0.95)^{0.143} \approx 0.85 \times 0.79 = 0.67$. A 21% decline in total Force, driven entirely by motivation, not skill loss.

This is the paper's warning about the meaning problem. Ada did not become less competent. She became less engaged. And in the Cobb-Douglas form, disengagement is operationally indistinguishable from incompetence: both drag down the product.

---

## Eq. 24: National Capability Discounted by Access Risk

$$E[\text{national capability}] = \sum_{i \in \text{workforce}} F_i \times M \times P(\text{access})$$

Country X has 100,000 software engineers with an average Force of 0.60. The LLM multiplier is $M = 5$. But the model is provided by a foreign entity, and geopolitical tensions put access probability at $P(\text{access}) = 0.70$.

$$E[\text{capability}] = 100{,}000 \times 0.60 \times 5 \times 0.70 = 210{,}000$$

If Country X had developed domestic models with guaranteed access ($P = 1.0$):

$$E[\text{capability}] = 100{,}000 \times 0.60 \times 5 \times 1.0 = 300{,}000$$

The access risk discount costs 90,000 units of expected capability, a 30% loss. Now consider a rival, Country Y, with 50,000 engineers, average Force of 0.80, domestic model access ($P = 1.0$):

$$E[\text{capability}] = 50{,}000 \times 0.80 \times 5 \times 1.0 = 200{,}000$$

Country X, with twice the engineers but lower Force and uncertain access, has roughly comparable expected capability to Country Y. At $P = 0.60$ (escalating tensions), Country X drops to $180{,}000$ and falls behind Country Y. National capability is not a headcount game; it is a Force-times-access game.

---

## Eq. 24a: Sovereign Resilience Test

$$\sum_{i} F_i \times 1 \geq \text{minimum viable capability}$$

The test: can the nation function *without* the multiplier ($M = 1$)? Suppose the minimum viable capability for maintaining critical infrastructure is 40,000 units.

**Country X**: $100{,}000 \times 0.60 \times 1 = 60{,}000$. Passes (with margin).

**But after 5 years of atrophy** (Force declined from 0.60 to 0.35 due to passive reliance on the foreign LLM):

$$100{,}000 \times 0.35 \times 1 = 35{,}000 < 40{,}000$$

Fails. The nation is no longer viable without the multiplier. And if access is cut at precisely this moment (the scenario sanctions are designed to create), the atrophied workforce cannot maintain critical systems. The dependency was not just economic; it was structural. The nation traded sovereign capability for amplified output and is now unable to produce either.

---

## Eq. 25: Exponential Multiplier Growth

$$M(t) = M_0 \cdot e^{\mu t}$$

Let $M_0 = 2$ (the multiplier at the start of observation) and $\mu = 0.3$ (30% annual growth rate).

| Year ($t$) | $M(t) = 2 \times e^{0.3t}$ | Interpretation |
|------|------|------|
| 0 | $2.0$ | Starting multiplier |
| 1 | $2.0 \times 1.35 = 2.70$ | One generation of model improvement |
| 2 | $2.0 \times 1.82 = 3.64$ | |
| 3 | $2.0 \times 2.46 = 4.92$ | Nearly 2.5x in three years |
| 5 | $2.0 \times 4.48 = 8.96$ | 4.5x in five years |

The multiplier roughly doubles every 2.3 years at this growth rate. But recall: most of the framework's dynamics are *convex* in $M$. Variance (Eq. 4) scales as $M^2$. At year 5, variance is $8.96^2 / 2^2 = 80.3 / 4 \approx 20$ times what it was at year 0. The epistemic gap (Eq. 10) widens proportionally. The evaluation bottleneck (Eq. 7) tightens. The tipping point (Eq. 14) rises. All simultaneously, all faster than $M$ itself grew.

This is why the paper argues the cost of delayed intervention is superlinear: waiting one model generation does not face a problem that is incrementally harder. It faces a problem where every convex dynamic has compounded.

---

## Eq. 26: Transfer Rate by Layer

$$\frac{dM_{\text{absorbed}}}{dt} = \sum_i \eta_i \cdot f_{i,\text{high}} \cdot \tau_{\text{extract}}$$

An organization has Ada spend 10 hours/week ($\tau_{\text{extract}} = 10$) training the model through RLHF, code review preferences, and documentation.

| Layer ($i$) | $\eta_i$ (transfer efficiency) | $f_{i,\text{Ada}}$ | Contribution |
|------------|------|------|------|
| Surface (syntax, patterns) | 0.95 | 0.90 | $0.95 \times 0.90 \times 10 = 8.55$ |
| Middle (judgment, taste) | 0.45 | 0.80 | $0.45 \times 0.80 \times 10 = 3.60$ |
| Deep (structural intuition) | 0.02 | 0.85 | $0.02 \times 0.85 \times 10 = 0.17$ |

$$\frac{dM_{\text{absorbed}}}{dt} = 8.55 + 3.60 + 0.17 = 12.32 \text{ units/week}$$

The surface layer contributes 69% of the transfer. The deep layer contributes 1.4%. Ada's structural intuition, her most valuable and irreplaceable asset, barely transfers at all. The model is absorbing *what she can articulate* (patterns, preferences, documented heuristics) and almost nothing of *what she cannot* (the felt sense of when a system is about to break).

---

## Eq. 26a: Transfer Efficiency by Layer

$$\eta_{\text{surface}} \approx 1, \qquad \eta_{\text{middle}} \approx 0.3\text{-}0.6, \qquad \eta_{\text{deep}} \approx 0$$

This equation is a set of empirical bounds rather than a formula to compute. The worked example is Eq. 26 above, where $\eta_{\text{surface}} = 0.95$, $\eta_{\text{middle}} = 0.45$, and $\eta_{\text{deep}} = 0.02$. The pattern mirrors Eq. 1a: what transfers most easily is what the LLM already substitutes most completely. The hierarchy is consistent across both equations because the same property, articulability, determines both substitutability and transferability.

---

## Eq. 27: Transfer Ceiling

$$\lim_{t \to \infty} M_{\text{absorbed}} = M_0 + \sum_i \eta_i \cdot f_{i,\text{explicit}}$$

Suppose $M_0 = 3.0$ (baseline model capability). Ada has explicit (articulable) knowledge at each layer:

| Layer | $\eta_i$ | $f_{i,\text{explicit}}$ | Contribution |
|-------|------|------|------|
| Surface | 0.95 | 0.90 | 0.855 |
| Middle | 0.45 | 0.50 (only part of judgment is articulable) | 0.225 |
| Deep | 0.02 | 0.10 (almost none is articulable) | 0.002 |

$$\lim M_{\text{absorbed}} = 3.0 + 0.855 + 0.225 + 0.002 = 4.08$$

Even with infinite time and effort, the model converges to 4.08. Ada's total Force is 0.85, but the model captured only about 1.08 units of her knowledge. The gap between what the model knows ($4.08$) and what Ada knows (all 0.85 of $F$, including the tacit components) is the tacit residual. This gap is what makes the bus factor illusion (Eq. 29) dangerous: the organization thinks it has captured Ada's knowledge. It has captured only the articulable portion.

---

## Eq. 28: Three-Way Resource Competition

$$\tau_{\text{available}} = \tau_{\text{create}} + \tau_{\text{evaluate}} + \tau_{\text{extract}}$$

Ada has 40 hours per week. The organization needs her in three roles:

| Allocation scenario | $\tau_{\text{create}}$ | $\tau_{\text{eval}}$ | $\tau_{\text{extract}}$ | Trade-off |
|---|---|---|---|---|
| Pre-LLM (two-way) | 30 | 10 | 0 | No extraction needed |
| Balanced | 15 | 15 | 10 | Each role gets some time |
| Evaluation-heavy | 10 | 25 | 5 | Bottleneck demands review time |
| Extraction-heavy | 5 | 10 | 25 | Organization prioritizes model training |

In the extraction-heavy scenario, Ada's creation output drops to $M \times F \times (5/40) = 3 \times 0.85 \times 0.125 = 0.32$. Her personal output has collapsed to one-eighth of its potential. The organization is sacrificing her highest-value activity (creation at $M \times F$) for extraction, betting that the long-term model improvement justifies the short-term loss. Whether that bet pays off depends on Eq. 27: the ceiling on what can be extracted may be lower than the organization assumes.

---

## Eq. 29: Transferred Is Not Retained

$$K_{\text{tacit}} \neq K_{\text{model}}$$

This is a qualitative inequality, not a calculation. The worked example is the gap from Eq. 27:

- $K_{\text{model}}$ (what the model absorbed from Ada) = 1.08 units
- $K_{\text{tacit}}$ (what Ada actually knows, including tacit knowledge) = much more

The model learned: "When you see N+1 queries in EF Core, try `.AsSplitQuery()` before `.Include()`."

What Ada actually knows: when to suspect N+1 queries from the *shape* of a latency trace before looking at the code, because she has seen that shape a hundred times and it triggers a pattern-match that is not articulable as a rule. The model captured the fix. It did not capture the diagnostic intuition that identifies the problem.

The organization believes: "We extracted Ada's EF Core expertise into the model." What it extracted: Ada's EF Core *recipes*. What it did not extract: Ada's EF Core *judgment*.

---

## Eq. 30: Successful Transfer Raises the Tipping Point

$$F^*_{\text{post-transfer}} > F^*_{\text{pre-transfer}}$$

From Eq. 14: $F^* = (\beta R + \sigma M_{\text{absorbed}}) / (\gamma E)$.

Before the transfer: $M_{\text{absorbed}} = 0.5$, giving $F^* = 1.15$ (from our earlier example).

After a successful transfer doubles $M_{\text{absorbed}}$ to 1.0:

$$F^*_{\text{post}} = \frac{(0.3)(0.6) + (0.1)(1.0)}{(0.4)(0.5)} = \frac{0.18 + 0.10}{0.20} = \frac{0.28}{0.20} = 1.40$$

The tipping point rose from 1.15 to 1.40. Engineers who were at $F = 1.25$, safely above the old threshold, are now below the new one. They did not get weaker. The model got stronger, organizations de-invested slightly more ($\sigma M_{\text{absorbed}}$ grew), and the bar moved. The *better* the transfer worked, the more people it pushes into the atrophy basin.

---

## Eq. 31: Data Quality Spiral

$$\text{Quality}(M_{t+1}) = g\!\left(\text{Quality}(M_t),\; \bar{F}_{\text{annotators}}(t)\right)$$

The exact function $g$ is unspecified, but the directional logic is clear. Suppose model quality on a 0-to-10 scale:

| Generation | Current quality | $\bar{F}_{\text{annotators}}$ | Quality change | Next-gen quality |
|---|---|---|---|---|
| $t = 0$ | 6.0 | 0.80 (strong pre-LLM workforce) | +1.5 | 7.5 |
| $t = 1$ | 7.5 | 0.70 (moderate atrophy) | +1.0 | 8.5 |
| $t = 2$ | 8.5 | 0.55 (significant atrophy) | +0.3 | 8.8 |
| $t = 3$ | 8.8 | 0.40 (severe atrophy) | -0.2 | 8.6 |
| $t = 4$ | 8.6 | 0.30 (workforce hollowed out) | -0.5 | 8.1 |

The model improved rapidly while the workforce was strong (generations 0-1). As the workforce atrophied (Eq. 11), the quality of human signal feeding the training pipeline degraded. By generation 3, the model is getting *worse* despite continued investment, because the annotators and trainers no longer have the judgment to provide good signal. By generation 4, the model quality is declining and the workforce that could fix it has already eroded.

This is the worst outcome: a workforce that has atrophied in reliance on a strong model, combined with a model that is no longer strong.

---

## Eq. 32: Force Ceiling Bounded by Available Struggle

$$F_{\text{initial}}(c) = F_{\text{max}} \cdot \left(\frac{S_{\text{available}}(c)}{S_{\text{pre-LLM}}}\right)^\rho$$

Let $F_{\text{max}} = 1.0$ (maximum achievable under pre-LLM conditions), $S_{\text{pre-LLM}} = 1.0$ (normalized), and $\rho = 0.7$ (moderate elasticity: early losses of struggle have a significant effect).

| Cohort entry year | $S_{\text{available}}$ | Ratio | $(ratio)^{0.7}$ | $F_{\text{initial}}$ |
|---|---|---|---|---|
| 2020 (pre-LLM) | 1.0 | 1.0 | 1.0 | 1.0 |
| 2024 | 0.70 | 0.70 | $0.70^{0.7} = 0.79$ | 0.79 |
| 2026 | 0.45 | 0.45 | $0.45^{0.7} = 0.56$ | 0.56 |
| 2028 | 0.25 | 0.25 | $0.25^{0.7} = 0.37$ | 0.37 |
| 2030 | 0.10 | 0.10 | $0.10^{0.7} = 0.20$ | 0.20 |

The 2020 cohort could achieve full Force; they had the full pre-LLM struggle environment. The 2026 cohort enters with a ceiling of 0.56, not because they are less talented, but because 55% of the struggle that would have built their Force has been absorbed by the LLM. The 2030 cohort's ceiling is 0.20.

**Combine this with the rising tipping point** (Eq. 30). If $F^* = 0.80$ by 2028, the 2028 cohort enters at $F_{\text{initial}} = 0.37$, well below the tipping point. They are born below a line that is rising. The growth basin is inaccessible to them from day one, absent deliberate intervention to reintroduce productive struggle.

---

## Eq. 33a and Eq. 33b: The Coupled $(F, M)$ System

$$\dot{F} = \alpha S + \gamma E F - \beta R(M) - \sigma M_{\text{abs}}(M) \qquad (33a)$$

$$\dot{M} = \mu(\bar{F}) \cdot M \qquad (33b)$$

The first 32 equations watched $F$ or $M$ evolve separately. Eq. 33 watches them evolve together. To make the arithmetic concrete, commit to the following illustrative forms, chosen so the growth and decay numbers match the Eq. 14 baseline:

- $\alpha = 0.2$, $\beta = 0.3$, $\gamma = 0.4$, $\sigma = 0.1$
- $S = 0.3$, $E = 0.5$
- $R(M) = 0.6 M$ and $M_{\text{abs}}(M) = 0.5 M$ (both linear in $M$)
- $\mu(\bar{F}) = 0.1 \cdot (\bar{F} - 1)$ (linear in workforce Force, zero at $\bar F = 1$)
- Identify $\bar{F} \approx F$ for a single-agent trajectory

Substituting these reduces the system to two lines of arithmetic:

$$\dot{F} = 0.06 + 0.2 F - 0.23 M \qquad \dot{M} = 0.1(F - 1) M$$

**Trajectory 1: Ada starts above the line.** At $t = 0$, $(F, M) = (1.5, 1.0)$.

| $t$ | $F$ | $M$ | $\dot F$ | $\dot M$ |
|-----|-----|-----|----------|----------|
| 0 | 1.50 | 1.00 | $0.06 + 0.30 - 0.23 = +0.13$ | $0.1 \cdot 0.5 \cdot 1.0 = +0.05$ |
| 1 | 1.63 | 1.05 | $0.06 + 0.326 - 0.242 = +0.144$ | $0.1 \cdot 0.63 \cdot 1.05 = +0.066$ |
| 2 | 1.77 | 1.12 | $0.06 + 0.355 - 0.257 = +0.158$ | $0.1 \cdot 0.77 \cdot 1.12 = +0.086$ |

Both rates positive and growing. Ada's trajectory compounds in both variables. The multiplier is rising because she is supplying good signal; her $F$ is rising because deliberate engagement pays off against the growing $M$.

**Trajectory 2: Cora starts below the line.** At $t = 0$, $(F, M) = (0.70, 1.0)$.

| $t$ | $F$ | $M$ | $\dot F$ | $\dot M$ |
|-----|-----|-----|----------|----------|
| 0 | 0.70 | 1.00 | $0.06 + 0.14 - 0.23 = -0.03$ | $0.1 \cdot (-0.30) \cdot 1.0 = -0.030$ |
| 1 | 0.67 | 0.97 | $0.06 + 0.134 - 0.223 = -0.029$ | $0.1 \cdot (-0.33) \cdot 0.97 = -0.032$ |
| 2 | 0.64 | 0.94 | $0.06 + 0.128 - 0.216 = -0.028$ | $0.1 \cdot (-0.36) \cdot 0.94 = -0.034$ |

Both rates negative. Cora's $F$ is dragging the multiplier's growth rate below zero; $M$ is not just failing to grow, it is shrinking. Her decline and the multiplier's decline reinforce each other. This is the coupling Eq. 31 predicted, now made arithmetic.

**Captures:** Two engineers with the same local environment diverge qualitatively, not because the parameters differ, but because their starting positions fall on different sides of the line that will be derived in Eq. 34.

---

## Eq. 34: The Separatrix

$$F^*(M) = \frac{\beta \cdot R(M) + \sigma \cdot M_{\text{abs}}(M)}{\gamma \cdot E} \qquad (34)$$

Using the same forms as Eq. 33:

$$F^*(M) = \frac{(0.3)(0.6 M) + (0.1)(0.5 M)}{(0.4)(0.5)} = \frac{0.18 M + 0.05 M}{0.20} = 1.15 \, M$$

The separatrix under these numbers is a straight line of slope 1.15 through the origin. Evaluating at a handful of multiplier values:

| $M$ | $F^*(M)$ | Ada at $F = 1.5$ | Ben at $F = 1.0$ |
|-----|----------|------------------|-------------------|
| 0.5 | 0.575 | above (safely) | above |
| 1.0 | 1.15 | above | **below** |
| 1.3 | 1.495 | barely above | below |
| 2.0 | 2.30 | **below** | below |
| 3.0 | 3.45 | below (far) | below |

Ada held her Force steady at 1.5 from $M = 0.5$ through $M = 3.0$. She did nothing wrong. She did not get weaker. But somewhere between $M = 1.3$ and $M = 1.4$, the separatrix rose past her. At $M = 1.3$ she was in the virtuous basin; by $M = 2.0$ she is in the decay basin. That is what it means for the tipping point to be a curve rather than a number. The threshold is moving. An engineer can be left behind by a world that is upgrading around her, without any decline in her own capability.

**Compare with Eq. 14.** That earlier equation gave $F^* = 1.15$ as a number. That number was Eq. 34 evaluated at $M = 1$. The scalar was always a cross-section of a curve; Eq. 14 just held the multiplier fixed so the cross-section was all you saw.

**Captures:** The tipping point is a state-dependent curve, not a scalar. A trajectory can cross it with no change in $F$, simply because $M$ moved.

---

## Eq. 35: Jacobian and Fixed-Point Classification

$$J(F^*, M^*) = \begin{bmatrix} \dfrac{\partial \dot{F}}{\partial F} & \dfrac{\partial \dot{F}}{\partial M} \\[0.4em] \dfrac{\partial \dot{M}}{\partial F} & \dfrac{\partial \dot{M}}{\partial M} \end{bmatrix} \qquad (35)$$

This is the equation the phase portrait turns on. Working it out concretely proceeds in five steps.

**Step 1: Locate a fixed point.** Using the Eq. 33 system, both rates vanish where:

- $\dot M = 0 \Rightarrow M = 0$ or $F = 1$
- $\dot F = 0$ at $F = 1$: $0.06 + 0.2 - 0.23 M = 0 \Rightarrow M = 0.26 / 0.23 \approx 1.13$

The interior fixed point is at $(F^*, M^*) = (1.00, 1.13)$. This is the "managed decline" equilibrium discussed in Terminal Dynamics, the middle candidate sitting on the separatrix.

**Step 2: Write the four partial derivatives symbolically.**

$$\frac{\partial \dot F}{\partial F} = \gamma E = 0.20 \qquad \frac{\partial \dot F}{\partial M} = -\beta R'(M) - \sigma M_{\text{abs}}'(M) = -(0.18 + 0.05) = -0.23$$

$$\frac{\partial \dot M}{\partial F} = \mu'(\bar F) \cdot M = 0.1 \cdot M \qquad \frac{\partial \dot M}{\partial M} = \mu(\bar F) = 0.1 (F - 1)$$

**Step 3: Evaluate at the fixed point $(1.00, 1.13)$.**

$$J = \begin{bmatrix} 0.20 & -0.23 \\ 0.113 & 0 \end{bmatrix}$$

The second diagonal entry is zero because the fixed point sits exactly where the multiplier's growth rate crosses zero. That is a general feature: any interior fixed point of this system lies on the curve $\mu(\bar F) = 0$, so $\partial \dot M / \partial M = 0$ there.

**Step 4: Compute trace and determinant.**

$$\text{tr}(J) = 0.20 + 0 = 0.20$$

$$\det(J) = (0.20)(0) - (-0.23)(0.113) = +0.026$$

**Step 5: Classify.** Using the trace-determinant shortcut from the reading guide:

$$\text{discriminant} = \text{tr}(J)^2 - 4 \det(J) = 0.040 - 0.104 = -0.064$$

The discriminant is negative, so the eigenvalues are complex conjugates. Solving:

$$\lambda = \frac{1}{2}\bigl(0.20 \pm \sqrt{-0.064}\bigr) = 0.10 \pm 0.127 \, i$$

Both eigenvalues have positive real part (0.10). The fixed point is an **unstable spiral**.

**Reading the classification.** The middle equilibrium under this parameterization is not a destination. A trajectory that lands near $(1.00, 1.13)$ spirals outward: $F$ and $M$ oscillate around the fixed point, with each swing larger than the last, until the trajectory is pulled into one of the surrounding basins. The paper's case-3 saddle and this case-4 unstable spiral share the same practical reading: managed decline looks like a resting place but is not one. Any perturbation sends the system somewhere else.

### How the Classification Flips

Changing one detail of the functional forms can flip the verdict. Three illustrative variations, each starting from the same baseline Eq. 33 system and altering one piece, show the range. The fixed-point location shifts between rows because the alteration changes where both rates vanish.

| Variation | What changes | Fixed point | $J$ | tr, det | Classification |
|-----------|--------------|-------------|-----|---------|-----------------|
| **Baseline** | as above | $(1.00, 1.13)$ | $\begin{pmatrix} 0.20 & -0.23 \\ 0.113 & 0 \end{pmatrix}$ | $+0.20$, $+0.026$ | Unstable spiral |
| **Add F-saturation:** $\gamma E F (1 - F/1.2)$ | $(1.00, 0.41)$ | $\begin{pmatrix} -0.13 & -0.23 \\ 0.041 & 0 \end{pmatrix}$ | $-0.13$, $+0.0094$ | Stable spiral |
| **Steeper $\mu'$:** slope 0.3 not 0.1 | $(1.00, 1.13)$ | $\begin{pmatrix} 0.20 & -0.23 \\ 0.339 & 0 \end{pmatrix}$ | $+0.20$, $+0.078$ | Unstable spiral (faster) |
| **Non-monotone $\mu$:** zero-crossing at $\bar F = 2$ with negative slope there | $(2.00, 2.00)$ | $\begin{pmatrix} 0.20 & -0.23 \\ -0.15 & 0 \end{pmatrix}$ | $+0.20$, $-0.035$ | **Saddle** |

The first variation is the cleanest way to make managed decline an actual attractor: a term that caps $F$ from above sends the trace negative, converting the unstable spiral into a stable one. The fourth shows a genuine saddle: one eigenvalue positive, one negative, with trajectories approaching along the stable manifold and departing along the unstable one. The saddle arises when $\mu$ has a zero-crossing with negative slope, meaning further workforce Force actively reduces the model's growth rate, which could describe a regime where an advanced workforce is outputting judgment the current model cannot encode.

**Captures:** The qualitative behavior at the middle equilibrium is an arithmetic consequence of four partial derivatives. The classification is not a philosophical choice; it falls out of the signs of trace and determinant once the functional forms are specified. That calibration is what the framework leaves empirically open.

---

## Eq. 36: The Irreversibility Frontier

$$\Omega_{\text{irreversible}} = \left\{ (F, M) \;:\; \forall \, (\alpha, \beta, \gamma) \in \Theta_{\text{feasible}}, \; (F(t), M(t)) \to \text{collapse} \right\} \qquad (36)$$

The separatrix of Eq. 34 asks "given these coefficients, where is the line?" Eq. 36 asks the harder question: "given *any* coefficients the institution could actually adopt, is this state recoverable?" The answer depends on $\Theta_{\text{feasible}}$, the realizable range of the dials.

Illustrative feasibility bounds for the coefficients already in play:

| Coefficient | Baseline | Best feasible | Bound set by |
|-------------|----------|---------------|---------------|
| $\alpha$ (learning from struggle) | 0.2 | 0.4 | How much productive struggle an education system can supply |
| $\beta$ (decay from reliance) | 0.3 | 0.1 | How far reliance habits can be suppressed by culture |
| $\gamma$ (growth from engagement) | 0.4 | 0.7 | How much deliberate engagement can be cultivated |

**Recoverable case: Ben at $(F, M) = (0.9, 1.3)$.** The current separatrix sits at $F^*(1.3) = 1.15 \cdot 1.3 = 1.495$, so Ben is below it by 0.595. Pushing coefficients to $(\alpha, \beta, \gamma) = (0.4, 0.1, 0.7)$ shifts the separatrix to $F^*(1.3) = (0.1 \cdot 0.78 + 0.1 \cdot 0.65)/(0.7 \cdot 0.5) = 0.143 / 0.35 = 0.41$. New line sits well below Ben. He is now above the separatrix. **Recoverable.**

**Irreversible case: Cora at $(F, M) = (0.3, 2.5)$.** Current separatrix at $M = 2.5$: $F^* = 2.875$. Cora is below by 2.575. Even at the best feasible coefficients, the separatrix becomes $F^*(2.5) = (0.1 \cdot 1.5 + 0.1 \cdot 1.25)/(0.7 \cdot 0.5) = 0.275 / 0.35 = 0.79$. The line is still above her by 0.49. No feasible coefficient setting produces a separatrix below her state. Cora sits inside $\Omega_{\text{irreversible}}$. **Unrecoverable under any adjustment the institution can actually make.**

**Captures:** Irreversibility is a geometric fact about where the feasible coefficient range allows the separatrix to fall. The window for policy closes at the crossing of the frontier $\partial \Omega$, which is inside the decay basin but strictly above the irreversibility region. A trajectory can be in serious decline and still recoverable, or look superficially similar and be lost.
