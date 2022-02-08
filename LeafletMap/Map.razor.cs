using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafletMap
{
    public partial class Map
    {
        [Inject] public IJSRuntime js { get; set; }

        [Parameter] public string TileLayer { get; set; } = "http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png";
        [Parameter] public int MaxZoom { get; set; } = 18;
        [Parameter] public int MinZoom { get; set; } = 0;
        [Parameter] public int Zoom { get; set; } = 10;        
        [Parameter] public Location Centr { get; set; } = new Location(51.508, 0);
        
        //[Parameter] public List<IShape> Shapes { get; set; } 

        private LeafletWrwpper _leafletWr;
        protected override async Task OnInitializedAsync()
        {
            _leafletWr = new LeafletWrwpper(js);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _leafletWr.Initialize("map");
                await _leafletWr.SetTileLayer(TileLayer, new TileLayerOption(MaxZoom, MinZoom));
                await _leafletWr.SetView(Centr, Zoom);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        public  async Task SetCentr(Location centr, int zoom) => await _leafletWr.SetView(centr, zoom);

        public async Task AddShape(IShape shape, ShapeOption option) => _leafletWr.AddShape(shape, option);
        public async Task AddShapes(IEnumerable<(IShape shape,ShapeOption option)> shapes)
        {
            foreach (var shape in shapes)
            {
                await _leafletWr.AddShape(shape.shape, shape.option);
            }
        }

    }
}
