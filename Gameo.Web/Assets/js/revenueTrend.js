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

    var lineChartConfig = {
        chart: {
            type: "line",
            renderTo: "ebMeterReadingTrend"
        },

        title: {
            text: 'EbMeter Reading Trend for the last 7 months'
        },

        xAxis: {},
        yAxis: {
            title: {
                text: 'EbMeter Reading'
            }
        },
        tooltip: {
            enabled: false,
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.x + ': ' + this.y + '°C';
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true
                },
                enableMouseTracking: false
            }
        }
    };

    function renderChart(data) {
        $(".chart").show();
        stackedChartConfig.xAxis.categories = _.pluck(data, "UserFriendlyMonthlyString");
        stackedChartConfig.series = [
            { name: "Revenue by Membership Recharges", data: _.pluck(data, "RevenueByMembershipRecharges") },
            { name: "Revenue by Games", data: _.pluck(data, "RevenueByGames") }
        ];

        lineChartConfig.xAxis.categories = _.pluck(data, "UserFriendlyMonthlyString");
        lineChartConfig.series = [{
            name: 'EbMeter Reading',
            data: _.pluck(data, "EbMeterReading")
        }];
        new Highcharts.Chart(stackedChartConfig);
        new Highcharts.Chart(lineChartConfig);
    }


    $("#getRevenue").click(function () {

        var branchName = $("#branchName").val();

        $.ajax({
            url: "/Admin/Revenue/GetMonthlyRevenueTrend",
            type: "POST",
            data: JSON.stringify({ branchName: branchName }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                renderChart(data);
            }
        });
    });


});