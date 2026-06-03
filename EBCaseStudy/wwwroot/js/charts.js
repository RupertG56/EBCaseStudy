window.createCategoryChart = (canvasId, labels, datasets) => {
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    // destroy previous chart if exists
    if (window._categoryChart) {
        try { window._categoryChart.destroy(); } catch { }
        window._categoryChart = null;
    }

    // convert datasets: Chart.js accepts arrays of numbers/null
    const mapped = datasets.map(ds => ({
        label: ds.label,
        data: ds.data.map(v => v === null ? null : v),
        backgroundColor: ds.backgroundColor,
        borderColor: ds.backgroundColor,
        borderWidth: 1
    }));

    window._categoryChart = new Chart(ctx.getContext('2d'), {
        type: 'bar',
        data: {
            labels: labels,
            datasets: mapped
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: {
                    stacked: false
                },
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Average price'
                    }
                }
            },
            plugins: {
                tooltip: { mode: 'index', intersect: false },
                legend: { position: 'top' }
            }
        }
    });
};