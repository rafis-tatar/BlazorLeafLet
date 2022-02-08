
let _map = null;

export function initialize(map) {
    _map = L.map(map);
}
export function setView(lon, lat, zoom) {    
    _map.setView([lon, lat], zoom);
}
export function setTileLayer(urlTemplate, attribution) {    
    L.tileLayer(urlTemplate, attribution).addTo(_map);
}
export function AddCircle(circle, options) {
    L.circle(circle, options).addTo(_map);
}
export function AddPolyline(polyline, options) {
    L.polyline(polyline, options).addTo(_map);
}
export function AddPolygone(polygone, options) {
    L.polygone(polygone, options).addTo(_map);
}