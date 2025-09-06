$(function () {
    const ctx = document.getElementById('trafficChart').getContext('2d');
    const chartData = window.chartData || [];

    function getGradient(ctx, chartArea, maxValue) {
        const gradient = ctx.createLinearGradient(0, chartArea.bottom, 0, chartArea.top);
        gradient.addColorStop(0, 'green');
        gradient.addColorStop(0.5, 'yellow');
        gradient.addColorStop(1, 'red');
        return gradient;
    }

    const labels = chartData.map(d => d.Label);
    const dataValues = chartData.map(d => d.Count);

    const trafficChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Visits',
                data: dataValues,
                fill: true,
                borderColor: 'black',
                borderWidth: 2,
                pointRadius: 5,
                pointBackgroundColor: dataValues.map(v => {
                    if (v > 80) return 'red';
                    if (v > 50) return 'orange';
                    if (v > 20) return 'yellow';
                    return 'green';
                }),
                tension: 0.3,
                backgroundColor: function (context) {
                    const chart = context.chart;
                    const { ctx, chartArea } = chart;
                    if (!chartArea) return null;
                    return getGradient(ctx, chartArea, Math.max(...dataValues));
                }
            }]
        },
        options: {
            responsive: true,
            plugins: { legend: { display: false } },
            scales: { y: { beginAtZero: true } }
        }
    });

    $('#trafficTable').DataTable({
        ajax: {
            url: '/admin/GetTrafficTable',
            dataSrc: 'data'
        },
        columns: [
            { data: 'ipAddress' },
            { data: 'userId' },
            { data: 'pathAccessed' },
            {
                data: 'timeStamp',
                render: function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        // Convert to local string for display
                        return new Date(data).toLocaleString();
                    }
                    // For ordering, keep raw data
                    return data;
                }
            }
        ],
        order: [[3, 'desc']]
    });
});