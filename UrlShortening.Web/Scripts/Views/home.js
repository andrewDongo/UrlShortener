
var btnShortenUrlSubmit = '#btnShorten';
var formShorten = '#shortenUrlForm';
var textOriginalUrl = '#Url';
var textStamp = '#Stamp';
var textPot = '#Pot';
var shortenUrlRequestUrl = 'api/shorten';
var shortenedUrlTemplate = '#shortUrlTemplate';
var requestErrorTemplate = '#requestErrorTemplate';
var shortenedResultContainer = '#shortenedUrlContainer';

// function to read model state errors and create list markup
var composeValidationSummary = function (data) {
    if (data[""].length > 0) {
        var index;
        var template = "<ul>";
        for (index = 0; index < data[""].length; index++) {
            template += '<li>' + data[""][index] + '</li>';
        }
        template += "</ul>";
        return template;
    }
    return "";
};

// ajax request to shorten url
var shortenUrlRequest = function (data) {
    $(btnShortenUrlSubmit).button('loading'); // disable submit button & indicate processing

    $.post(shortenUrlRequestUrl, data, function (result) {
        // request successful

        $(btnShortenUrlSubmit).button('reset'); // reset submit button
        $(shortenedResultContainer).empty(); // empty results container
        $(shortenedUrlTemplate).tmpl(result).appendTo(shortenedResultContainer); // append results to container

    }).fail(function (xhr, status, error) {
        //request failed

        $(btnShortenUrlSubmit).button('reset'); // reset submit button
        var message = 'Url could not be shortened. '; // initialize error message
        if (xhr.responseJSON.message || xhr.responseJSON.modelState) {
            if (!xhr.responseJSON.modelState && xhr.responseJSON.message) {
                message += xhr.responseJSON.message;
            } else if (xhr.responseJSON.modelState && xhr.responseJSON.modelState) {
                message += composeValidationSummary(xhr.responseJSON.modelState);
            }
        }
        var display = { message: message };
        $(shortenedResultContainer).empty();  // empty results container
        $(requestErrorTemplate).tmpl(display).appendTo(shortenedResultContainer); // append message to container
    });
};

// build data required for shorten url ajax request
var shortenUrlProcess = function () {

    var url = $(textOriginalUrl).val();
    var serverStamp = $(textStamp).val();
    var honeyPot = $(textPot).val();

    var data = { url: url, stamp: serverStamp, pot: honeyPot };
    shortenUrlRequest(data);
};

// set event handlers for page
var pageEventHandlers = function () {

    $(btnShortenUrlSubmit).click(function (event) {
        if ($(formShorten).valid()) {
            shortenUrlProcess();
            event.stopImmediatePropagation();
            event.preventDefault();
        }
    });
};

// on dom ready
$(function () {
    pageEventHandlers();
});