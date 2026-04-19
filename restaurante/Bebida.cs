namespace restaurante
{
    public class Bebida
    {
        public int BebidaId { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Capacidad { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }
    }
}