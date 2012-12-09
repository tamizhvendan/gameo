﻿$(function () {
    
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
});