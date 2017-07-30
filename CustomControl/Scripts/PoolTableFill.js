var setToPoolTable = function (swimLine, timeInterval, value) {

    $("#in-" + timeInterval + "-sline-" + swimLine).html(value);

}

var initToPoolTable = function (poolTableList) {

    var asd = poolTableList.replace(/&quot;/g, "\"");
    var obj = JSON.parse(asd);
    obj.forEach(function (item, i, obj) {
        var s = "#swm-" + item.CurrentSwmPool + " > #in-" + item.TimeIntervalsCount + "-sline-" + item.SwimLinesCount;
        var a = $("#swm-" + item.CurrentSwmPool + " > #in-" + item.TimeIntervalsCount + "-sline-" + item.SwimLinesCount);
        var b = $(s);
        $("#swm-" + item.CurrentSwmPool + " > #in-" + item.TimeIntervalsCount + "-sline-" + item.SwimLinesCount).html(item.Value);

    });
    
}   