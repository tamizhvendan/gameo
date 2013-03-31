$(function () {

    var stackedChartConfig = {
        chart: {
            type: 'column',
            renderTo: 'revenueStackedChart'
        },
        title: {
            text: 'Total Revenue for the last 7 months'
        },
        xAxis: {},
        yAxis: {
            min: 0,
            title: {
                text: 'Total Collection'
            },
            stackLabels: {
                enabled: true,
                style: {
                    fontWeight: 'bold',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                }
            }
        },
        legend: {
            align: 'right',
            x: -100,
            verticalAlign: 'top',
            y: 20,
            floating: true,
            backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColorSolid) || 'white',
            borderColor: '#CCC',
            borderWidth: 1,
            shadow: false
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' +
                        this.series.name + ': ' + this.y + '<br/>' +
                        'Total: ' + this.point.stackTotal;
            }
        },
        plotOptions: {
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                }
            }
        }
    };


    function renderChart(data) {
        stackedChartConfig.xAxis.categories = _.pluck(data, "UserFriendlyMonthlyString");
        stackedChartConfig.series = [
            { name: "Revenue by Games", data: _.pluck(data, "RevenueByGames") },
            { name: "Revenue by Membership Recharges", data: _.pluck(data, "RevenueByMembershipRecharges") }
        ];

        new Highcharts.Chart(stackedChartConfig);
    }


    $("#getRevenue").click(function () {

        var branchName = $("#branchName").val();

        $.ajax({
            url: "/Admin/Revenue/GetMonthlyRevenue",
            type: "POST",
            data: JSON.stringify({ branchName: branchName }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                renderChart(data);
            }
        });
    });


});