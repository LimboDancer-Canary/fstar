# TimeSeriesAnimator — Design Document

## Problem

The repository contains a `TimeSeriesAnimator` chart component shell, but it is not yet wired as a functional reusable control. It renders a canvas and playback buttons, yet it does not currently follow the same end-to-end interaction pattern used by the other UI components in `FStar.UI`.

Before exposing it on a new page, the control needs a design pass that aligns it with the conventions already established by the existing charts, controls, layout wrappers, and JS interop helpers.

## Design Goal

Create a reusable animated chart component that:

1. Fits the existing `FStar.UI` component architecture.
2. Uses the current JS module rather than introducing a new client-side stack.
3. Keeps domain logic in the page, not in the chart control.
4. Exposes a simple parameter-first API consistent with the rest of the component library.
5. Is accessible, testable, and visually consistent with the current chart system.

## Existing Conventions To Reuse

### 1. Component placement by role

The project already separates reusable UI by responsibility:

- `src/FStar.UI/Components/Charts` for visualizations such as `LineChart` and `PhasePortrait`
- `src/FStar.UI/Components/Controls` for interaction primitives such as `ParameterSlider`
- `src/FStar.UI/Components/Layout` for wrappers such as `ChartCard`

`TimeSeriesAnimator` should remain a generic chart component under `Components/Charts`.

### 2. Parameter-first component APIs

Existing components use direct parameters and event callbacks rather than hidden state models.

Examples already in the repo:

- `ParameterSlider` uses `Value` + `ValueChanged`
- `FloatingSliderPanel` uses `Visible` + `VisibleChanged`
- chart components accept typed data inputs and derive their rendering from those inputs

This suggests the animator should expose explicit playback state instead of an opaque frame callback contract.

### 3. JS interop lifecycle pattern

Existing interactive components use a consistent interop shape:

- import `./_content/FStar.UI/FStar.UI.lib.module.js` in `OnAfterRenderAsync`
- guard JS initialization for SSR/prerender with `try/catch (JSException)`
- implement `IAsyncDisposable` for teardown
- destroy JS-side listeners or handles during disposal
- guard `DisposeAsync` against a null module reference to avoid race conditions when the component is removed before JS initialization completes

`FloatingSliderPanel` is the clearest precedent. Its `DisposeAsync` checks the module reference before calling `destroyDraggable`. The animator must apply the same guard before calling `stopAnimationLoop` or `destroyChartObserver`, since disposal can race with the first render.

### 4. Scoped styling and CSS variable usage

Component styling is local and lightweight:

- each component uses its own `.razor.css` when needed
- colors and surfaces are expressed with CSS custom properties such as `--text-primary`, `--border-color`, and `--accent`
- the visual language is restrained and utility-free rather than framework-heavy

`TimeSeriesAnimator` should follow the same approach.

### 5. Accessibility baseline

Existing chart components provide:

- `role="img"`
- descriptive `aria-label` values
- explicit button labels where interactive controls exist

The animator should preserve this baseline for both the canvas and the playback controls.

### 6. Page composition convention

Pages are responsible for:

- route and page title
- scenario setup
- explanatory copy
- chart card layout
- parameter orchestration

Reusable chart components are responsible for:

- rendering
- local interaction behavior
- presentation-specific state

This means `TimeSeriesAnimator` should not know anything about FORCE, the multiplier, or specific equation families.

## Current Relevant Assets

### Existing component shell

Current component:

- `src/FStar.UI/Components/Charts/TimeSeriesAnimator.razor`

Current parameters:

- `Width`
- `Height`
- `TimeSeries`
- `Duration`
- `Dt`
- `IsAnimating`
- `OnFrame`

The current shell proves the intended interaction model, but not the full implementation.

### Existing chart model

The current chart model is already suitable for the component:

```csharp
public record AnimatedSeries(string Label, double[] Values, string Color);
```

This should be reused rather than replaced in the first pass.

### Existing JS helpers

The shared JS module already contains the necessary building blocks:

- `initCanvas`
- `clearCanvas`
- `drawLine`
- `drawCircle`
- `drawText`
- `startAnimationLoop`
- `stopAnimationLoop`

These functions should be leveraged instead of introducing another drawing abstraction.

## Recommended Component Responsibilities

`TimeSeriesAnimator` should own:

- canvas initialization
- axis and series rendering for the current frame
- play/pause/reset button behavior
- animation loop coordination with JS
- current frame advancement
- drawing the current playback marker or progressive line reveal

The host page should own:

- which series are displayed
- initial playback settings
- scenario data and presets
- descriptive text and theory framing
- any page-level readout of current simulation time

## Recommended API Shape

### Parameter definitions

- `Dt` is the data spacing: the time interval between consecutive values in each `AnimatedSeries.Values` array. It is not the animation tick interval. The JS-side animation loop runs at the browser's `requestAnimationFrame` rate; `Dt` only determines how many data points map to a given playback time.
- `Duration` is the total simulated time span. The frame count is `(int)(Duration / Dt)`.

### Keep

These existing parameters fit the current component library style and should remain:

- `Width : int`
- `Height : int`
- `TimeSeries : IReadOnlyList<AnimatedSeries>?`
- `Duration : double`
- `Dt : double`

### Replace or extend

The current `OnFrame` callback is serviceable, but it is less consistent with the rest of the component library than a paired state API.

Recommended public API:

- `bool IsAnimating`
- `EventCallback<bool> IsAnimatingChanged`
- `double CurrentTime`
- `EventCallback<double> CurrentTimeChanged`

This follows the conventions already used elsewhere in the repo: parent owns the state, component reports updates.

### Optional later additions

These are not required for the first pass, but the API should leave room for them:

- `bool ShowLegend`
- `bool ShowCurrentMarker`
- `string? XLabel`
- `string? YLabel`
- `EventCallback OnReset`

## Interaction Model

### Playback controls

First-pass built-in controls should remain minimal:

- Play / Pause
- Reset

This is enough to validate the control without introducing a more complex scrubber UI on day one.

### Frame behavior

The component should treat `Duration / Dt` as the total number of frame steps. On each animation tick:

1. advance the playback time
2. compute the corresponding frame index
3. redraw the chart to that frame
4. notify the parent through `CurrentTimeChanged`
5. stop cleanly at the end of the series

### Reset behavior

Reset should:

- stop the animation loop
- return playback to time `0`
- redraw the initial frame
- notify the parent that both current time and animating state changed

### Data change during playback

If the parent supplies a new `TimeSeries` reference while the animation is running, the component should:

1. pause the animation
2. reset playback to time `0`
3. redraw the initial frame with the new data
4. notify the parent of both state changes (`IsAnimatingChanged`, `CurrentTimeChanged`)

This keeps behavior predictable. The user presses Play again to animate the new data. The component should detect the change by comparing the `TimeSeries` reference in `OnParametersSet`.

## Rendering Model

The first-pass rendering model should be deliberately simple and consistent with the rest of the repo.

### Recommended first-pass visuals

- draw axes directly on the canvas
- compute Y bounds from all supplied series
- reveal each line progressively up to the current frame
- optionally draw a small marker dot at the leading point of each series
- optionally draw a small time readout on the canvas

### Performance considerations

The animation loop runs via `requestAnimationFrame` on the JS side, which caps the tick rate to the display refresh rate (typically 60 Hz). Each tick redraws the full canvas: clear, axes, all series up to the current frame. For the expected series sizes (a few hundred to a few thousand points, 1 to 5 series), this is well within budget for a single canvas redraw per frame.

If profiling later reveals dropped frames, the first mitigation should be to skip intermediate data points during drawing (stride the `Values` array) rather than introducing off-screen buffering or partial redraws. This keeps the rendering path simple.

### Scope constraints

The first implementation should assume:

- all series share the same timestep
- all series use the same playback duration
- all series are line series
- data is dense enough to animate frame-by-frame

Avoid in the first pass:

- zoom/pan
- legends with toggles
- hover tooltips
- multiple axis scales
- a new model hierarchy

## Styling Recommendations

Add a local stylesheet:

- `src/FStar.UI/Components/Charts/TimeSeriesAnimator.razor.css`

Recommended styling approach:

- responsive container with `max-width: 100%`
- light control row below the canvas
- use CSS variables already present in the project
- keep the visual weight similar to `LineChart` and `FloatingSliderPanel`

### Resize behavior

The canvas should re-initialize when its container size changes. The JS module already provides `initChartObserver` and `destroyChartObserver` for this purpose. The component should call `initChartObserver` during `OnAfterRenderAsync` to watch the container element, and `destroyChartObserver` during `DisposeAsync`. When a resize is detected, the JS callback should re-run `initCanvas` with the new dimensions and trigger a redraw of the current frame.

Suggested visual structure:

- canvas block
- compact controls row
- optional small status line for current time

## Accessibility Requirements

The control should provide:

- `role="img"` on the canvas
- a descriptive `aria-label`, e.g. `Animated time series chart`
- explicit button labels for play/pause and reset
- visible text that reflects state for keyboard and screen reader users

## Testing Strategy

Existing tests already confirm basic rendering of the current shell. The expanded control should add coverage for:

1. renders canvas and controls
2. toggles play/pause state via callbacks
3. resets playback to time zero
4. tolerates empty series data
5. resets when `TimeSeries` reference changes during playback
6. renders without failing in bUnit with loose JS interop where needed

The first test additions should stay at the component boundary and avoid over-testing JS internals.

### JS interop mocking in bUnit

The component will call several JS functions through the imported module. bUnit tests should use `BunitJSInterop` in loose mode so that unmocked calls return default values without throwing. The following calls should be explicitly set up where their return values affect assertions:

- `import` (module import): return a mock `IJSObjectReference`
- `initCanvas`: no return value; set up as a void invocation
- `startAnimationLoop` / `stopAnimationLoop`: void invocations; verify they are called when play/pause/reset is triggered
- `initChartObserver` / `destroyChartObserver`: void invocations; verify setup during first render and teardown during disposal

Drawing calls (`clearCanvas`, `drawLine`, `drawCircle`, `drawText`) do not need explicit setup in loose mode. They can be left to the default handler since test assertions should target component state and callback behavior, not individual draw calls.

## Page Integration Guidance

Once the control is aligned with the conventions above, it can be exposed on a dedicated page:

- route: to be chosen during page implementation
- menu label: `Time-Series Player`
- placement: bottom of the existing menu as requested

That page should follow the same pattern as the other pages:

- `@page` directive
- `PageTitle`
- `h1`
- `ChartCard` wrapping the demo visualization
- optional theory tab or explanatory copy

## Recommended First Implementation Order

1. Refactor `TimeSeriesAnimator` API to match existing state conventions.
2. Implement JS-backed lifecycle and canvas redraw behavior.
3. Add local CSS matching current chart/control styling.
4. Expand component tests for playback interactions.
5. Create the new `Time-Series Player` page.
6. Add the page to `NavMenu.razor` at the bottom.
7. Add render coverage for the new page.

## Decision

The control should be implemented as a generic reusable chart component that follows the same design conventions already used by `LineChart`, `ParameterSlider`, `FloatingSliderPanel`, and `ChartCard`.

The key design decision is to keep simulation semantics in the page and playback/rendering behavior in the control.