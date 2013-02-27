$(function () {

    // jQuery hack to integrate Bootstrap CSS with MVC3 validation CSS styles
    // Reference : http://www.braindonor.net/blog/integrating-bootstrap-error-styling-with-mvcs-unobtrusive-error-validation/381/

    $('span.field-validation-valid, span.field-validation-error').each(function () {
        $(this).addClass('help-inline');
    });
    $('form').submit(function () {
        if ($(this).valid()) {
            $(this).find('div.control-group').each(function () {
                if ($(this).find('span.field-validation-error').length == 0) {
                    $(this).removeClass('error');
                }
            });
        }
        else {
            $(this).find('div.control-group').each(function () {
                if ($(this).find('span.field-validation-error').length > 0) {
                    $(this).addClass('error');
                }
            });
        }
    });
    $('form').each(function () {
        $(this).find('div.control-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('error');
            }
        });
    });


    $(".time-picker").datetimepicker({
        controlType: 'select',
        dateFormat: 'dd-mm-yy',
        timeFormat: "h:mm:ss TT",
        changeMonth: true,
        changeYear: true
    });

    $(".date-picker").datepicker({
        dateFormat: 'dd-mm-yy',
        changeMonth: true,
        changeYear: true
    });

    $("#tabs").tabs();

    Handlebars.registerHelper('datetime', function (jsonDateTimeString) {
        var dateTime = moment(jsonDateTimeString).subtract({ hours: 5, minutes: 30 });

        return dateTime.format("DD/MM/YYYY hh:mm A");
    });

    Handlebars.registerHelper('time', function (jsonDateTimeString) {
        var dateTime = moment(jsonDateTimeString).subtract({hours : 5, minutes : 30});

        return dateTime.format("hh:mm A");
    });

    Handlebars.registerHelper('date', function (jsonDateTimeString) {
        var dateTime = moment(jsonDateTimeString).subtract({ hours: 5, minutes: 30 });

        return dateTime.format("DD/MM/YYYY");
    });

    Handlebars.registerHelper('packageType', function (packageType) {
        var packageTypes = ["No Package", "Package of 3 Hours", "Package of 5 Hours"];
        return packageTypes[packageType];
    });

    Handlebars.registerHelper('each_with_index', function (context, options) {
        var fn = options.fn, inverse = options.inverse;
        var ret = "";

        if (context && context.length > 0) {
            for (var i = 0, j = context.length; i < j; i++) {
                ret = ret + fn(_.extend({}, context[i], { i: i, iPlus1: i + 1 }));
            }
        } else {
            ret = inverse(this);
        }
        return ret;
    });

    $(document).ajaxStart(function () {
        var $ajaxButton = $(".ajax-button"),
            loadingText = $ajaxButton.data('loading-text');
        $ajaxButton.html(loadingText).attr('disabled', true);
    });

    $(document).ajaxStop(function () {
        var $ajaxButton = $(".ajax-button"),
            labelText = $ajaxButton.data('label-text');
        $ajaxButton.html(labelText).removeAttr('disabled');
    });
});