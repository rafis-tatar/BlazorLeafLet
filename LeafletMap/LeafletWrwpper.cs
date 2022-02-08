using Microsoft.JSInterop;

namespace LeafletMap
{
    internal class LeafletWrwpper : IAsyncDisposable
    {        
        
        private readonly Lazy<Task<IJSObjectReference>> leafletWrapper;
        public LeafletWrwpper(IJSRuntime jsRuntime)
        {            
            leafletWrapper = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/LeafletMap/leaflet-wrapper.js").AsTask());
        }
        
        public async Task Initialize(string id)
        {
            var module = await leafletWrapper.Value;
            await module.InvokeVoidAsync($"initialize", new object[] { id });
        }

        
        public async Task SetView(Location point, int zoom)
        {            
            var module = await leafletWrapper.Value;
            await module.InvokeVoidAsync($"setView", new object[] { point.lon, point.lat, zoom });            
        }

        
        public async Task SetTileLayer(string tileUrlTemplate, TileLayerOption option )
        {
            var module = await leafletWrapper.Value;
            await module.InvokeVoidAsync("setTileLayer", new object[] { tileUrlTemplate, option });
        }
        
        public async Task AddCircle(Circle circle, ShapeOption option)
        {
            var module = await leafletWrapper.Value;
            await module.InvokeVoidAsync("AddCircle", new object[] { circle.Center, option });
        }
        public async Task AddPolygone(Polygone polygone, ShapeOption option)
        {
            var module = await leafletWrapper.Value;
            await module.InvokeVoidAsync("AddPolygone", new object[] { polygone.Points, option });
        }
        public async Task AddPolyline(Polyline polyline, ShapeOption option)
        {
            var module = await leafletWrapper.Value;
            await module.InvokeVoidAsync("AddPolyline", new object[] { polyline.Points, option });
        }

        public async Task AddShape(IShape shape, ShapeOption option)
        {
            if (shape is Circle circle)
            {
                await AddCircle(circle, option);
            }
            else if (shape is Polyline polyline)
            {
                await AddPolyline(polyline, option);
            }
            else if (shape is Polygone polygone)
            {
                await AddPolygone(polygone, option);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (leafletWrapper.IsValueCreated)
            {
                var module = await leafletWrapper.Value;
                await module.DisposeAsync();
            }
        }

    }

  
}