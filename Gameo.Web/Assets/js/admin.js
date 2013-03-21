$(function () {

    $("#findGames").click(function () {
        var request = {
            day: $("#day").val(),
            branchName: $("#branchName").val()
        },
            gameSummaryTemplate = Handlebars.compile($("#game-summary-template").html()),
            $gameSummary = $("#gameSummary");

        $gameSummary.hide();

        $.ajax({
            url: "/Admin/Admin/ViewGames",
            type: "POST",
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $gameSummary.html(gameSummaryTemplate(data));
                $gameSummary.show('slow');
                initializeInvalidateGames();
            }
        });
    });

    function initializeInvalidateGames() {
        $(".invalidateGame").off('click');
        $(".invalidateGame").on('click', function () {
            var $this = $(this),
                $parentRow = $this.parent().parent(),
                gamerName = $($parentRow.children()[1]).html(),
                inTime = $($parentRow.children()[2]).html(),
                outTime = $($parentRow.children()[3]).html(),
                price = $($parentRow.children()[4]).html(),
                gameConfirmationString = " Name : " + gamerName + " \n InTime : " + inTime + "\n OutTime : " + outTime + "\n Price : " + price;

            var isInvalid = confirm("Are you sure to mark the Game as invalid ? \n" + gameConfirmationString),
                id = $this.data('id');

            if (isInvalid) {
                $.ajax({
                    url: "/Admin/Admin/InvalidateGame",
                    type: "POST",
                    data: JSON.stringify({ id: id }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        $this.attr('disabled', 'disabled');
                    }
                });
            }
        });
    }
});