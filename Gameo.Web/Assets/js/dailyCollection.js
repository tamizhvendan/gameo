$(function () {

    $("#findCollection").on("click", function () {
        var request = {
            day: $("#day").val(),
            branchName: $("#branchName").val()
        };

        var collectionSummaryTemplate = Handlebars.compile($("#collection-summary-template").html()),
            collectionDetailsTemplate = Handlebars.compile($("#collection-details-template").html());

        $.ajax({
            url: "/Admin/Collection/ViewCollection",
            type: "POST",
            data: JSON.stringify(request),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#collectionSummary").html(collectionSummaryTemplate(data));
                $("#collectionDetails").html(collectionDetailsTemplate(data));
                $("#collectionDetailsAccordion").accordion({
                    collapsible: true
                });
            }
        });

    });
});