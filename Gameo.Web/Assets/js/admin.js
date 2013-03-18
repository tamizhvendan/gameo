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
            var $this = $(this);
            var isInvalid = confirm("Are you sure to invalid the Game ?"),
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