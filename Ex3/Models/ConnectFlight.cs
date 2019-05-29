@{
    Layout = null;
}


<html>
<head>
    <style>
        html, body {
            width: 100%;
            height: 100%;
        }
    </style>
</head>
<body >
    <canvas id="canvas" style="position:absolute; left:0px; top:0px"></canvas>
    <img id="map" src="/map.png" alt="world map" hidden="hidden" />
</body>
</html>
<script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>

<div>
    <h2>FLIGHT:</h2>
    <table>
        <tr>
            <td>Lon:  </td>
            <td><p type="text" id="Lon" size="5"></p></td>
        </tr>
        <tr>
            <td>lat:  </td>
            <td><p type="text" id="Lat" size="5"></p></td>
        </tr>
    </table>
</div>


<script>
    var IntervalTime = @Session["time"] * 1000
    var xPos = null;
    var yPos = null;
    var toDel = 0;
    var x;
    var y;
    var c = document.getElementById("canvas");
    myTimer = (function (imgWi, imgHi) {
        $.post("@Url.Action("GetFlightData")").done(function (xml) {
            var xmlDoc = $.parseXML(xml),
                $xml = $(xmlDoc),
                lat = $xml.find("lat").text();
            lon = $xml.find("lon").text();
            var c = document.getElementById("canvas").getContext("2d");
            alert(window.innerWidth);
            x = (window.innerWidth / 360) * (parseFloat(lon.valueOf()) + 180);
            y = (window.innerHeight / 180) * (parseFloat(lat.valueOf()) + 90);
            if (xPos == null || yPos == null) {
                drawArc(x, y);
            }
            else {
                
                x = (window.innerWidth / 360) * (parseFloat(lon.valueOf()) + 180);
                //x = lon + 300;
                y = (window.innerHeight / 180) * (parseFloat(lat.valueOf()) + 90);
                c.beginPath();
                c.lineWidth = "4";
                c.strokeStyle = "red";
                c.moveTo(xPos, yPos);
                c.lineTo(x, y);
                c.stroke();
            }
            xPos = x;
            yPos = y;
        
            $("#lon").text(lon);
            $("#lat").text(lat);
        });
    });
    if (IntervalTime != 0) {
        setInterval(myTimer, IntervalTime);
        myTimer(imgWi, imgHi);
    } else {
        showArc = (function (imgWi, imgHi) {
            $.post("@Url.Action("GetFlightData")").done(function (xml) {
                var xmlDoc = $.parseXML(xml),
                    $xml = $(xmlDoc),
                    lat = $xml.find("lat").text();
                lon = $xml.find("lon").text();
                
                var x = (imgWi / 360) * (parseFloat(lon.valueOf()) + 180);
                var y = (imgHi / 180) * (parseFloat(lat.valueOf()) + 90);
                drawArc(x, y);
        
                $("#lon").text(lon);
                $("#lat").text(lat);
            });
        });
        showArc();
    }

</script>

<script>
    window.onload = function () {
        var c = document.getElementById("canvas");
        c.width = window.innerWidth;
        c.height = window.innerHeight;
        var ctx = c.getContext("2d");
        ctx.translate(0, 0);
        var img = document.getElementById("map");
        ctx.drawImage(img, 0, 0, img.width, img.height, 0, 0, c.width, c.height);
        if (IntervalTime != 0) {
            myTimer(window.innerWidth, window.innerHeight);
        } else {
            showArc(window.innerWidth, window.innerHeight);
        }
    };
</script>

<script>
    drawArc = (function (x, y) {
        var c = document.getElementById("canvas");
        var ctx = c.getContext("2d");
        ctx.beginPath();
        ctx.arc(x, y, 5, 0, 2 * Math.PI);
        ctx.fillStyle = 'red';
        ctx.fill();
        ctx.lineWidth = 2;
        ctx.strokeStyle = 'black';
        ctx.stroke();
    });
</script>


<!--style="border:1px solid #d3d3d3-->
