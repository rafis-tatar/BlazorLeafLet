namespace LeafletMap
{
    public class Polyline: IShape
    {
        /// <summary>
        /// Вершины
        /// </summary>
        public IEnumerable<Location> Points { get; set; }
    }

  
}