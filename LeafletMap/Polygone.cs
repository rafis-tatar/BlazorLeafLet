namespace LeafletMap
{
    public class Polygone : IShape
    {
        /// <summary>
        /// Вершины
        /// </summary>
        public IEnumerable<Location> Points { get; set; }
    }

  
}