startTimepicker = $("#start-time").bfhtimepicker();
endTimepicker = $("#end-time").bfhtimepicker();

$(".bfh-timepicker").on("mousemove",
    function () {
        $(this).css("cursor", "pointer");
    });


var delayTimer;
$("#search-in").on("change",
    function(e) {
        e.preventDefault();
        var data = $("#search-in").val();
        if (data.length < 2)
            return;
        clearTimeout(delayTimer);
        
        delayTimer = setTimeout(function () {
            searchResources();
        }, 1000);
    });

$("#search-btn").click(function () {
    var stime = $("#start-time input").val();
    var etime = $("#end-time input").val();
    searchResources(stime, etime);
});

searchResources = function (start, end) {
    var data = $("#search-in").val();
    var url = $("#search-url").val();
    $.ajax({
        method: "GET",
        url: url,
        data: {
            search: data,
            startTime: start,
            endTime: end
        },
        success: function (html) {
            $("#resources").html(html);
        }
    });
}

//startTimepicker.on("change.bfhtimepicker",
//    function () {
//        var stime = $("#start-time input").val();
//        var etime = $("#end-time input").val();
//        searchResources(stime, etime);
//    });

//endTimepicker.on("change.bfhtimepicker",
//    function () {
//        var stime = $("#start-time input").val();
//        var etime = $("#end-time input").val();
//        searchResources(stime, etime);
//    });