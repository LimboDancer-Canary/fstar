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
