var delayTimer;
$("#search-in").on("change",
    function(e) {
        e.preventDefault();
        var data = $("#search-in").val();
        if (data.length < 3)
            return;
        clearTimeout(delayTimer);
        var url = $("#search-url").val();
        delayTimer = setTimeout(function () {
            $.ajax({
                method: "GET",
                url: url,
                data: {
                    search: data
                },
                success: function(html) {
                    $("#resources").html(html);
                }
            });
        }, 2000);
    });