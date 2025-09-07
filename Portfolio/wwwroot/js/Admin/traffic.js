$(function () {
    const ctx = document.getElementById('trafficChart').getContext('2d');
    let trafficChart;

    function loadChart(range, startDate, endDate) {
        $.get('/Admin/Traffic/Summary', { range, startDate, endDate }, function (chartData) {
            const labels = chartData.map(d => d.label);
            const dataValues = chartData.map(d => d.count);

            if (trafficChart) trafficChart.destroy();

            trafficChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Visits',
                        data: dataValues,
                        borderColor: 'black',
                        borderWidth: 2,
                        tension: 0.3,
                        fill: true,
                        backgroundColor: 'rgba(0,123,255,0.2)'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: { legend: { display: false } },
                    scales: { y: { beginAtZero: true } }
                }
            });
        });
    }

    const table = $('#trafficTable').DataTable({
        ajax: {
            url: '/Admin/Traffic/Logs',
            data: function (d) {
                const range = $('#rangeSelect').val();
                if (range === 'custom') {
                    d.startDate = $('#startDate').val();
                    d.endDate = $('#endDate').val();
                } else {
                    d.startDate = null;
                    d.endDate = null;
                    d.range = range;
                }
            },
            dataSrc: 'data'
        },
        columns: [
            { data: 'ipAddress' },
            { data: 'userId' },
            { data: 'pathAccessed' },
            { data: 'userAgent' },
            {
                data: 'timeStamp',
                render: function (data, type) {
                    if (type === 'display' || type === 'filter') {
                        return new Date(data).toLocaleString();
                    }
                    return data;
                }
            }
        ],
        order: [[4, 'desc']]
    });

    // Toggle date inputs when custom range is selected
    $('#rangeSelect').on('change', function () {
        if ($(this).val() === 'custom') {
            $('.custom-range').removeClass('d-none');
        } else {
            $('.custom-range').addClass('d-none');
        }
    });

    $('#applyFilters').on('click', function () {
        const range = $('#rangeSelect').val();
        let startDate = null, endDate = null;

        if (range === 'custom') {
            startDate = $('#startDate').val();
            endDate = $('#endDate').val();
        }

        loadChart(range, startDate, endDate);
        table.ajax.reload();
    });

    // initial load with default "week"
    loadChart('week');
});
