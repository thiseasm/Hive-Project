
function getLocation() {
    var coords = [];
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            coords = showPosition,
            coords = showError,
            { timeout: 10000 });
        
    } else {
        return undefined;
    }
    return coords;

    function showPosition(position) {
        var coordinates = [position.coords.latitude, position.coords.longitude];
        return coordinates;
    }

    function showError(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                return "User denied the request for Geolocation.";
            case error.POSITION_UNAVAILABLE:
                return "Location information is unavailable.";
            case error.TIMEOUT:
                return "The request to get user location timed out.";
            case error.UNKNOWN_ERROR:
                return "An unknown error occurred.";
        }
    }
}





