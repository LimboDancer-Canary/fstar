// Canvas rendering primitives for TimeSeriesAnimator
// These are composed by the Blazor component via JS interop.

export function initCanvas(canvasId, width, height) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;
    canvas.width = width;
    canvas.height = height;
    const ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, width, height);
}

export function clearCanvas(canvasId) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;
    const ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
}

export function drawLine(canvasId, x1, y1, x2, y2, color, width) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;
    const ctx = canvas.getContext('2d');
    ctx.beginPath();
    ctx.moveTo(x1, y1);
    ctx.lineTo(x2, y2);
    ctx.strokeStyle = color;
    ctx.lineWidth = width;
    ctx.stroke();
}

export function drawCircle(canvasId, cx, cy, r, color) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;
    const ctx = canvas.getContext('2d');
    ctx.beginPath();
    ctx.arc(cx, cy, r, 0, 2 * Math.PI);
    ctx.fillStyle = color;
    ctx.fill();
}

export function drawText(canvasId, text, x, y, font, color) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;
    const ctx = canvas.getContext('2d');
    ctx.font = font;
    ctx.fillStyle = color;
    ctx.fillText(text, x, y);
}

// Draggable panel support
let dragState = {};

export function initDraggable(elementId, titleBarId, defaultX, defaultY) {
    const el = document.getElementById(elementId);
    const titleBar = document.getElementById(titleBarId);
    if (!el || !titleBar) return;

    // Set initial position
    if (defaultX < 0) {
        defaultX = Math.max(16, window.innerWidth - 320);
    }
    el.style.left = defaultX + 'px';
    el.style.top = defaultY + 'px';

    const state = { dragging: false, offsetX: 0, offsetY: 0 };

    function onPointerDown(e) {
        state.dragging = true;
        const rect = el.getBoundingClientRect();
        state.offsetX = e.clientX - rect.left;
        state.offsetY = e.clientY - rect.top;
        e.preventDefault();
    }

    function onPointerMove(e) {
        if (!state.dragging) return;
        const x = Math.max(0, Math.min(window.innerWidth - 60, e.clientX - state.offsetX));
        const y = Math.max(0, Math.min(window.innerHeight - 40, e.clientY - state.offsetY));
        el.style.left = x + 'px';
        el.style.top = y + 'px';
    }

    function onPointerUp() {
        state.dragging = false;
    }

    titleBar.addEventListener('pointerdown', onPointerDown);
    document.addEventListener('pointermove', onPointerMove);
    document.addEventListener('pointerup', onPointerUp);

    dragState[elementId] = { onPointerDown, onPointerMove, onPointerUp, titleBar };
}

export function destroyDraggable(elementId) {
    const s = dragState[elementId];
    if (!s) return;
    s.titleBar.removeEventListener('pointerdown', s.onPointerDown);
    document.removeEventListener('pointermove', s.onPointerMove);
    document.removeEventListener('pointerup', s.onPointerUp);
    delete dragState[elementId];
}

// Chart scroll observer – reports which chart section is most visible
let chartObserverState = {};

export function initChartObserver(dotNetRef, methodName, elementIds) {
    destroyChartObserver();

    const ratios = {};
    elementIds.forEach(id => { ratios[id] = 0; });

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            ratios[entry.target.id] = entry.intersectionRatio;
        });

        // Pick the element with the greatest visible ratio
        let bestId = null;
        let bestRatio = 0;
        for (const id of elementIds) {
            if (ratios[id] > bestRatio) {
                bestRatio = ratios[id];
                bestId = id;
            }
        }

        const index = bestId ? elementIds.indexOf(bestId) : -1;
        if (index >= 0 && bestRatio > 0.05) {
            dotNetRef.invokeMethodAsync(methodName, index);
        }
    }, {
        threshold: [0, 0.1, 0.25, 0.5, 0.75, 1.0]
    });

    elementIds.forEach(id => {
        const el = document.getElementById(id);
        if (el) observer.observe(el);
    });

    chartObserverState = { observer, dotNetRef };
}

export function destroyChartObserver() {
    if (chartObserverState.observer) {
        chartObserverState.observer.disconnect();
        chartObserverState = {};
    }
}

let animationFrameIds = {};

export function startAnimationLoop(canvasId, dotNetRef, methodName) {
    function loop() {
        dotNetRef.invokeMethodAsync(methodName);
        animationFrameIds[canvasId] = requestAnimationFrame(loop);
    }
    animationFrameIds[canvasId] = requestAnimationFrame(loop);
}

export function stopAnimationLoop(canvasId) {
    if (animationFrameIds[canvasId]) {
        cancelAnimationFrame(animationFrameIds[canvasId]);
        delete animationFrameIds[canvasId];
    }
}

// ── Time-series animation ──────────────────────────────────────────────

let animationCache = {};

export function setAnimationData(canvasId, seriesData, totalFrames, duration,
                                  yMin, yMax, mLeft, mRight, mTop, mBottom,
                                  xLabel, yLabel) {
    animationCache[canvasId] = {
        series: seriesData, totalFrames, duration,
        yMin, yMax, mLeft, mRight, mTop, mBottom,
        xLabel: xLabel || '', yLabel: yLabel || ''
    };
}

export function clearAnimationData(canvasId) {
    delete animationCache[canvasId];
}

export function drawAnimationFrame(canvasId, frameIndex) {
    const canvas = document.getElementById(canvasId);
    const data = animationCache[canvasId];
    if (!canvas || !data) return;

    const ctx = canvas.getContext('2d');
    const w = canvas.width;
    const h = canvas.height;
    const { series, totalFrames, duration, yMin, yMax,
            mLeft, mRight, mTop, mBottom } = data;
    const plotW = w - mLeft - mRight;
    const plotH = h - mTop - mBottom;
    const yRange = yMax - yMin;

    ctx.clearRect(0, 0, w, h);

    // Axes
    ctx.strokeStyle = '#6b7280';
    ctx.lineWidth = 1;
    ctx.beginPath();
    ctx.moveTo(mLeft, h - mBottom);
    ctx.lineTo(w - mRight, h - mBottom);
    ctx.moveTo(mLeft, mTop);
    ctx.lineTo(mLeft, h - mBottom);
    ctx.stroke();

    // Axis labels
    ctx.font = '11px sans-serif';
    ctx.fillStyle = '#9ca3af';
    ctx.textAlign = 'left';
    ctx.fillText(fmtNum(yMax), 2, mTop + 11);
    ctx.fillText(fmtNum(yMin), 2, h - mBottom - 4);
    ctx.fillText('0', mLeft, h - 4);
    ctx.textAlign = 'right';
    ctx.fillText(fmtNum(duration), w - mRight, h - 4);
    ctx.textAlign = 'left';

    // Axis titles
    const { xLabel, yLabel } = data;
    ctx.font = '12px sans-serif';
    ctx.fillStyle = '#4b5563';
    if (xLabel) {
        ctx.textAlign = 'center';
        ctx.fillText(xLabel, mLeft + plotW / 2, h - 2);
        ctx.textAlign = 'left';
    }
    if (yLabel) {
        ctx.save();
        ctx.translate(12, mTop + plotH / 2);
        ctx.rotate(-Math.PI / 2);
        ctx.textAlign = 'center';
        ctx.fillText(yLabel, 0, 0);
        ctx.restore();
    }

    if (totalFrames < 2 || yRange === 0) return;

    for (const s of series) {
        const maxIdx = Math.min(frameIndex, s.values.length - 1);
        if (maxIdx < 1) continue;

        ctx.strokeStyle = s.color;
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(
            mLeft,
            mTop + (1.0 - (s.values[0] - yMin) / yRange) * plotH
        );
        for (let i = 1; i <= maxIdx; i++) {
            ctx.lineTo(
                mLeft + (i / (totalFrames - 1)) * plotW,
                mTop + (1.0 - (s.values[i] - yMin) / yRange) * plotH
            );
        }
        ctx.stroke();

        // Leading dot
        const dx = mLeft + (maxIdx / (totalFrames - 1)) * plotW;
        const dy = mTop + (1.0 - (s.values[maxIdx] - yMin) / yRange) * plotH;
        ctx.beginPath();
        ctx.arc(dx, dy, 4, 0, 2 * Math.PI);
        ctx.fillStyle = s.color;
        ctx.fill();
    }
}

function fmtNum(n) {
    if (Math.abs(n) >= 1000) return n.toFixed(0);
    if (Math.abs(n) >= 1) return n.toPrecision(3);
    return n.toPrecision(2);
}

export function startTimeSeriesAnimation(canvasId, dotNetRef, callbackMethod,
                                          duration, dt, resumeFrom) {
    stopAnimationLoop(canvasId);
    const data = animationCache[canvasId];
    if (!data) return;

    const wallStart = performance.now();
    const simStart = resumeFrom || 0;
    let lastNotify = 0;

    function loop(timestamp) {
        const elapsed = (timestamp - wallStart) / 1000;
        const simTime = Math.min(simStart + elapsed, duration);
        const frame = Math.min(Math.floor(simTime / dt), data.totalFrames);

        drawAnimationFrame(canvasId, frame);

        if (simTime >= duration) {
            dotNetRef.invokeMethodAsync(callbackMethod, duration, true);
            delete animationFrameIds[canvasId];
            return;
        }

        if (timestamp - lastNotify > 100) {
            dotNetRef.invokeMethodAsync(callbackMethod, simTime, false);
            lastNotify = timestamp;
        }

        animationFrameIds[canvasId] = requestAnimationFrame(loop);
    }
    animationFrameIds[canvasId] = requestAnimationFrame(loop);
}
