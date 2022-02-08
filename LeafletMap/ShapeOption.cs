using System.ComponentModel.DataAnnotations;

namespace LeafletMap
{
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

  
}