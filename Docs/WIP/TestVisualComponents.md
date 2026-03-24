# Visual Component Test Plan

This test plan is designed for Claude Code to visually verify common elements across all chart pages in the F* Blazor application using the preview tools (preview_start, preview_screenshot, preview_snapshot, preview_inspect, preview_click, preview_eval).

---

## Test Environment Setup

1. **Kill any existing instances** of the app process before starting. The preview will fail if the executable is already running.
   ```
   Bash -> taskkill /F /IM FStarEquations.App.exe 2>/dev/null; taskkill /F /IM dotnet.exe 2>/dev/null
   ```
   This ensures port 5013 is free and `preview_start` can bind successfully.
2. Start the dev server with `preview_start` (name: `blazor-app`).
3. Navigate to each page route listed below.
4. Use `preview_snapshot` and `preview_screenshot` to verify element presence.
5. Use `preview_inspect` to verify CSS properties where noted.

---

## Pages Under Test

| # | Route                  | Page Name              |
|---|------------------------|------------------------|
| 1 | `/base-model`          | BaseModelPage          |
| 2 | `/variance`            | VariancePage           |
| 3 | `/creation-evaluation` | CreationEvaluationPage |
| 4 | `/negative-force`      | NegativeForcePage      |
| 5 | `/force-dynamics`      | ForceDynamicsPage      |
| 6 | `/transfer`            | TransferPage           |
| 7 | `/tacit-knowledge`     | TacitKnowledgePage     |
| 8 | `/tipping-point`       | TippingPoint           |
| 9 | `/divergence`          | DivergencePage         |
|10 | `/motivation`          | MotivationPage         |
|11 | `/organizational`      | OrganizationalPage     |
|12 | `/model-growth`        | ModelGrowthPage        |
|13 | `/sovereignty`         | SovereigntyPage        |
|14 | `/terminal-dynamics`   | TerminalDynamics       |
|15 | `/timeline`            | TimelineDashboard      |
|16 | `/cascade`             | CascadeDashboard       |

---

## Common Element Tests

Run every test in this section on **each** of the 16 pages listed above unless the test specifies otherwise.

### TEST-01: Page Title and Heading

**What to verify:**
- A `<PageTitle>` element sets the browser tab title.
- An `<h1>` heading is visible at the top of the page.
- The heading text is non-empty.

**How to verify:**
```
preview_snapshot  -> confirm an h1 element exists with non-empty text
preview_eval      -> document.title !== ''
```

**Pass criteria:** Both the document title and the h1 heading are present and contain text.

---

### TEST-02: ChartCard Wrapper

**What to verify:**
- At least one `.chart-card` element is rendered on the page.
- Each ChartCard has a visible header (`.chart-card-header`).
- Each ChartCard header contains a title string.

**How to verify:**
```
preview_snapshot  -> look for elements with role or class matching chart-card
preview_inspect   -> selector: '.chart-card' (confirm it exists)
preview_inspect   -> selector: '.chart-card-header' (confirm non-empty text)
```

**Pass criteria:** Every page has at least one ChartCard with a visible, non-empty header.

---

### TEST-03: Chart/Theory Tabs

**What to verify:**
- Each ChartCard contains a tab bar (`.chart-card-tabs`).
- Two tabs are present: "Chart" and "Theory".
- The "Chart" tab is active by default.
- Clicking "Theory" switches the content area to show theory text.
- Clicking "Chart" switches back to the chart SVG.

**How to verify:**
```
preview_snapshot  -> confirm two tab buttons exist per ChartCard
preview_click     -> selector: '.chart-card-tabs button:nth-child(2)' (Theory tab)
preview_snapshot  -> confirm theory content is now visible (.theory-content)
preview_click     -> selector: '.chart-card-tabs button:nth-child(1)' (Chart tab)
preview_snapshot  -> confirm chart SVG is visible again
```

**Pass criteria:** Tabs toggle between chart and theory views without errors.

---

### TEST-04: SVG Chart Rendering

**What to verify:**
- At least one `<svg>` element is rendered inside the chart content area.
- The SVG has a non-zero width and height (viewBox is set).
- The SVG contains visible child elements (paths, rects, circles, lines, or text).

**How to verify:**
```
preview_eval      -> document.querySelectorAll('.chart-card-content svg').length > 0
preview_inspect   -> selector: '.chart-card-content svg' (check bounding box > 0)
preview_eval      -> document.querySelector('.chart-card-content svg').children.length > 0
```

**Pass criteria:** Every page renders at least one non-empty SVG chart.

---

### TEST-05: FloatingSliderPanel Presence

**What to verify:**
- A FloatingSliderPanel (`.floating-slider-panel`) is rendered.
- The panel has a visible title bar.
- The panel contains at least one ParameterSlider.

**How to verify:**
```
preview_snapshot  -> confirm floating-slider-panel element exists
preview_inspect   -> selector: '.floating-slider-panel'
preview_eval      -> document.querySelectorAll('.parameter-slider').length > 0
```

**Pass criteria:** The slider panel is present with at least one slider on every page.

**Exception:** `/cascade` may use a different control layout; verify manually.

---

### TEST-06: ParameterSlider Structure

**What to verify:**
- Each slider has a label (`.parameter-slider-label`) with text.
- Each slider has a value display (`.parameter-slider-value`) showing a number.
- Each slider has an `<input type="range">` element.
- The range input has `min`, `max`, and `step` attributes set.

**How to verify:**
```
preview_eval      -> Array.from(document.querySelectorAll('.parameter-slider')).every(s =>
                       s.querySelector('.parameter-slider-label')?.textContent?.trim() &&
                       s.querySelector('input[type=range]')?.min !== undefined)
```

**Pass criteria:** All sliders have labels, value readouts, and properly configured range inputs.

---

### TEST-07: Slider Reactivity

**What to verify:**
- Moving a slider updates the displayed value next to it.
- Moving a slider causes the chart SVG content to change (re-render).

**How to verify:**
```
preview_eval      -> capture initial value: document.querySelector('.parameter-slider-value').textContent
preview_fill      -> selector: '.parameter-slider input[type=range]', value: (midpoint of range)
preview_eval      -> confirm value text changed from initial
preview_eval      -> confirm SVG content has changed (e.g., path 'd' attribute differs)
```

**Pass criteria:** Slider movement updates both the value display and the chart.

---

### TEST-08: Preset Buttons

**What to verify:**
- The FloatingSliderPanel contains at least one preset button.
- Clicking a preset button updates all slider values.
- Clicking a preset button causes charts to re-render.

**How to verify:**
```
preview_eval      -> document.querySelectorAll('.floating-slider-panel button').length >= 1
preview_click     -> first preset button
preview_eval      -> verify slider values changed
preview_screenshot -> visual confirmation of chart change
```

**Pass criteria:** Preset buttons exist and trigger full parameter and chart updates.

**Applicable pages:** All pages that define presets. Pages without presets should still have the panel but may lack preset buttons; note this as expected.

---

### TEST-09: Chart Readout Text

**What to verify:**
- Below or near each chart, a readout section (`.chart-readout`) displays current computed values.
- The readout contains at least one numeric value.
- The readout updates when a slider is moved.

**How to verify:**
```
preview_snapshot  -> look for chart-readout elements with numeric text
preview_eval      -> document.querySelectorAll('.chart-readout').length > 0
preview_fill      -> move a slider
preview_eval      -> confirm readout text changed
```

**Pass criteria:** Readout text is present and reactive to slider changes.

---

### TEST-10: HeatMap Crosshair (where applicable)

**Applicable to pages with HeatMap charts:** base-model, variance, creation-evaluation, negative-force, transfer, divergence, motivation, organizational, sovereignty.

**What to verify:**
- The HeatMap SVG contains crosshair lines (typically two `<line>` elements styled red or with a crosshair class).
- The crosshair position updates when the corresponding sliders are moved.

**How to verify:**
```
preview_eval      -> document.querySelectorAll('svg line.crosshair, svg .crosshair line').length >= 2
preview_fill      -> move the X-axis slider
preview_eval      -> confirm crosshair line x1/x2 attributes changed
```

**Pass criteria:** Crosshair lines exist and track slider values.

---

### TEST-11: RadarChart Rendering (where applicable)

**Applicable to pages with RadarChart:** base-model, variance, creation-evaluation, negative-force, transfer, divergence, motivation, organizational, sovereignty.

**What to verify:**
- A RadarChart SVG is rendered (typically smaller, ~280x280).
- The radar contains concentric ring circles and a filled polygon path.
- The polygon path updates when sliders change.

**How to verify:**
```
preview_eval      -> check for radar SVG with circles and polygon path
preview_fill      -> move a slider
preview_eval      -> confirm polygon 'd' attribute changed
```

**Pass criteria:** Radar chart renders with rings and a reactive polygon.

---

### TEST-12: LineChart Markers (where applicable)

**Applicable to pages with LineChart markers:** base-model, creation-evaluation, negative-force, force-dynamics, tacit-knowledge, model-growth.

**What to verify:**
- The LineChart SVG contains at least one marker element (circle or similar) indicating the current parameter value on the curve.
- The marker position updates when the associated slider changes.

**How to verify:**
```
preview_eval      -> document.querySelectorAll('svg circle.marker, svg .chart-marker').length >= 1
preview_fill      -> move the associated slider
preview_eval      -> confirm marker cx/cy changed
```

**Pass criteria:** Markers are visible and move with slider changes.

---

### TEST-13: FloatingSliderPanel Minimize/Restore

**What to verify:**
- The panel title bar has a minimize button.
- Clicking minimize collapses the panel to a small pill/icon.
- Clicking the pill restores the full panel.

**How to verify:**
```
preview_snapshot  -> find minimize button in the panel title bar
preview_click     -> minimize button
preview_snapshot  -> confirm panel is collapsed (pill visible, sliders hidden)
preview_click     -> the collapsed pill
preview_snapshot  -> confirm panel is fully restored with sliders visible
```

**Pass criteria:** Minimize and restore cycle works without errors.

---

### TEST-14: Scroll-Aware Slider Filtering (where applicable)

**Applicable to pages with multiple chart groups and scroll-aware filtering:** creation-evaluation, negative-force, base-model.

**What to verify:**
- When scrolling to a different chart section, the visible sliders change to match that chart's parameter group.
- A context label in the panel updates to reflect the active chart.
- A "Show all" toggle is available and shows all sliders when activated.

**How to verify:**
```
preview_eval      -> window.scrollTo(0, document.getElementById('chart-1').offsetTop)
preview_snapshot  -> confirm slider group changed
preview_eval      -> check context label text
preview_click     -> "Show all" toggle
preview_snapshot  -> confirm all sliders are now visible
```

**Pass criteria:** Slider groups filter by visible chart, and the toggle overrides filtering.

---

### TEST-15: Theory Content Rendering

**What to verify:**
- Clicking the "Theory" tab reveals a `.theory-content` section.
- The theory content contains at least one KaTeX-rendered equation (`.katex` or `.katex-display` class).
- The theory content contains descriptive text paragraphs.

**How to verify:**
```
preview_click     -> Theory tab
preview_snapshot  -> confirm .theory-content is visible
preview_eval      -> document.querySelectorAll('.theory-content .katex, .theory-content .katex-display').length > 0
preview_eval      -> document.querySelector('.theory-content p')?.textContent?.length > 10
```

**Pass criteria:** Theory tabs contain both rendered LaTeX equations and explanatory text.

---

### TEST-16: No Console Errors on Load

**What to verify:**
- After navigating to a page and waiting for it to fully render, no JavaScript errors appear in the console.

**How to verify:**
```
preview_eval      -> navigate to the page route
preview_console_logs level: 'error' -> check for errors
```

**Pass criteria:** Zero console errors on page load. Warnings may be noted but are not failures.

---

### TEST-17: No Failed Network Requests

**What to verify:**
- After page load, no network requests return 4xx or 5xx status codes.

**How to verify:**
```
preview_network filter: 'failed' -> check for failed requests
```

**Pass criteria:** Zero failed network requests.

---

## Page-Specific Tests

These tests target elements unique to individual pages.

### TEST-PS-01: TippingPoint NumberLine (`/tipping-point`)

- Verify a horizontal NumberLine SVG is rendered with a red threshold line.
- Verify population dots are plotted on the line.
- Verify a TornadoDiagram SVG renders horizontal diverging bars.

### TEST-PS-02: TerminalDynamics PhasePortrait (`/terminal-dynamics`)

- Verify a PhasePortrait SVG renders a trajectory curve (F vs M).
- Verify axis labels for F(t) and M(t) are present.
- Verify additional LineCharts show F(t) and M(t) over time.

### TEST-PS-03: NegativeForce WaterfallChart (`/negative-force`)

- Verify a WaterfallChart SVG renders bars in green (positive), red (negative), and blue (total).
- Verify bar labels or tooltips display contribution values.

### TEST-PS-04: CascadeDashboard Node Diagram (`/cascade`)

- Verify a custom SVG node-link diagram is rendered.
- Verify nodes are clickable and link to individual equation pages.
- Verify directed edges have arrow markers.

### TEST-PS-05: TimelineDashboard Wide Charts (`/timeline`)

- Verify multiple wide LineCharts (900px+ width) render in a vertical stack.
- Verify at least 3 timeline panels are present: M(t), F(t), Force Layers.
- Verify each panel has its own ChartCard wrapper and readout.

---

## Execution Checklist

Use this checklist to track progress when running the tests. Mark each cell P (pass), F (fail), or N/A.

| Test       | base-model | variance | creation-eval | negative-force | force-dynamics | transfer | tacit-knowledge | tipping-point | divergence | motivation | organizational | model-growth | sovereignty | terminal-dynamics | timeline | cascade |
|------------|------------|----------|---------------|----------------|----------------|----------|-----------------|---------------|------------|------------|----------------|--------------|-------------|-------------------|----------|---------|
| TEST-01    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-02    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-03    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-04    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-05    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-06    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-07    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-08    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-09    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-10    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-11    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-12    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-13    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-14    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-15    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-16    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
| TEST-17    |            |          |               |                |                |          |                 |               |            |            |                |              |             |                   |          |         |
