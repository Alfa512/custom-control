var delayTimer;
$("#search-in").on("change",
    function(e) {
        e.preventDefault();
        
        clearTimeout(delayTimer);
        
        delayTimer = setTimeout(function () {
            searchResources();
        }, 1000);
    });

$("#search-btn").click(function() {
    searchResources();
});

searchResources = function () {
    var data = $("#search-in").val();
    if (data.length < 2)
        return;
    var url = $("#search-url").val();
    $.ajax({
        method: "GET",
        url: url,
        data: {
            search: data
        },
        success: function (html) {
            $("#resources").html(html);
        }
    });
}