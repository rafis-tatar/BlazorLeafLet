using Microsoft.JSInterop;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeafletMap
{

    public record TileLayerOption(int maxZoom, int minZoom);
    public record CircleOption(string color, string fillColor, double fillOpacity, int radius);
    public record Location(double lon, double lat);

    public class ShapeOption
    {
        /// <summary>
        /// Whether to draw stroke along the path.Set it to false to disable borders on polygons or circles.
        /// </summary>
        public bool stroke { get; set; } = true;

        /// <summary>
        /// Stroke color
        /// </summary>
        public string color { get; set; } = "#3388ff";

        /// <summary>
        /// Stroke width in pixels
        /// </summary>
        public int weight { get; set; } = 3;

        [Range(0,1)]
        public double opacity { get; set; } = 1.0;

        /// <summary>
        /// Fill color. Defaults to the value of the color option
        /// </summary>
        public string fillColor { get; set; } 

        /// <summary>
        ///	Fill opacity.
        /// </summary>
        [Range(0, 1)]
        public double fillOpacity { get; set; } = 0.2;
    }

    public interface IShape
    {

    }

    public class Circle: IShape
    {
        /// <summary>
        /// Center
        /// </summary>
        public Location Center { get; set; }   

        /// <summary>
        /// 
        /// </summary>
        public double Radius { get; set; }
    }

    public class Polygone : IShape
    {
        /// <summary>
        /// Вершины
        /// </summary>
        public IEnumerable<Location> Points { get; set; }
    }

    public class Polyline: IShape
    {
        /// <summary>
        /// Вершины
        /// </summary>
        public IEnumerable<Location> Points { get; set; }
    }


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