$(function () {
    function renderChart(data) {
        $(".chart").show();

        var rendering = {
            chart: {
                renderTo: 'trendChart'
            },
            title: {
                text: 'Gaming Trend'
            },
            xAxis: {
                categories: [
                    '9-11',
                    '11-13',
                    '13-15',
                    '15-17',
                    '17-19',
                    '19-22'
                ],
                title: {
                    text: 'TimeRange in a Day'
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Number of Customers'
                },
                tickInterval: 10
            },
            legend: {
                layout: 'vertical',
                backgroundColor: '#FFFFFF',
                align: 'left',
                verticalAlign: 'top',
                x: 80,
                y: 50,
                floating: true,
                shadow: true
            },
            tooltip: {
                formatter: function() {
                    if (this.point.name) { // the pie chart
                        return this.point.name + ': ' + this.y + ' Customer(s)';
                    } else {
                        return '' + this.y + ' Customer(s)';
                    }
                }
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            }
        };
            
        var series = [],
            total = [0, 0, 0, 0, 0, 0],
            splineChartData = {
                type: 'spline',
                name: 'Total Number of customers',
                data: total,
                align: 'center',
                verticalAlign: 'bottom',
                marker: {
                    lineWidth: 2,
                    lineColor: Highcharts.getOptions().colors[3],
                    fillColor: 'white'
                }
            },
            pieChartData = {
                type: 'pie',
                name: 'Console Usage',
                data: [],
                size: 300,
                showInLegend: false,
                dataLabels: {
                    enabled: false
                }
                
            };
            
        for (i = 0; i < data.length; i++) {
                
            var r = data[i];
                
            r.type = "column";
            series.push(data[i]);
                
            for (var j = 0; j < total.length; j++) {
                total[j] += r.data[j];
            }

            pieChartData.data.push({
                name: r.name,
                y : r.data.reduce(function (seed, d) { return seed + d; }, 0)
            });
        }

        rendering.series = series;
        rendering.chart.renderTo = "columnChart";
        chart = new Highcharts.Chart(rendering);

        series = [];
        series.push(splineChartData);
        rendering.series = series;
        rendering.chart.renderTo = "splineChart";
        chart = new Highcharts.Chart(rendering);
            
        series = [];
        series.push(pieChartData);
        rendering.series = series;
        rendering.chart.renderTo = "pieChart";
        chart = new Highcharts.Chart(rendering);
            
    };
        

    $("#findTrend").on("click", function () {
            
        var request = {
            from: $("#from").val(),
            to: $("#to").val(),
            branchName: $("#BranchName").val()
        };

        var result = [{
            name: 'Game Console 1',
            data: [7, 0, 14, 23, 8, 10]
        },{
            name: 'Game Console 2',
            data: [6, 1, 4, 3, 2, 1]
        }, {
            name: 'X Box 1',
            data: [6, 10, 24, 3, 8, 10]
        }, {
            name: 'X Box 2',
            data: [16, 10, 4, 30, 18, 20]
        }];

            

        $.ajax({
            url: "/Admin/GamingTrend/GetTrend",
            type : "POST",
            data : JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            success : function(data) {
                renderChart(data);
            }
        });
    });
});
