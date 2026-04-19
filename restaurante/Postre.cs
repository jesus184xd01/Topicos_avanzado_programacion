namespace restaurante
{
    public class Postre
    {
        public int PostreId { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
        public string ImagenUrl { get; set; }
    }
}