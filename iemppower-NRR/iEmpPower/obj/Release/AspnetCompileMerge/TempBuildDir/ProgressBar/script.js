



//alert(document.getElementById('MainContent_lblMlCnt').innerText);
var a,b,c;
$(document).ready(function() {
   // alert();
    a = eval(document.getElementById('MainContent_HFM').value);
    b = eval(document.getElementById('MainContent_HFF').value);
    c = a + b;

    a = (a / c) * 100;
    b = (b / c) * 100;

    $("#divprog1").circularProgress({
        line_width: 6,
        color: "#00617c",
        starting_position: 0, // 12.00 o' clock position, 25 stands for 3.00 o'clock (clock-wise)
        percent: 0, // percent starts from
        //percentage: "",
        text: document.getElementById('MainContent_HFM').value
    }).circularProgress('animate', a, 5000);


    $("#divprog2").circularProgress({
        line_width: 6,
        color: "#00ff00",
        starting_position: 0, // 12.00 o' clock position, 25 stands for 3.00 o'clock (clock-wise)
        percent: 0, // percent starts from
        //percentage: b,
        text: document.getElementById('MainContent_HFF').value
    }).circularProgress('animate', b, 5000);

    $("#divprog3").circularProgress({
        line_width: 6,
        color: "#00617c",
        starting_position: 0, // 12.00 o' clock position, 25 stands for 3.00 o'clock (clock-wise)
        percent: 0, // percent starts from
        //percentage: b,
        text: c
    }).circularProgress('animate', 100, 5000);
  
});

