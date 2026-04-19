namespace restaurante
{
    public class Platillo
    {
        public int PlatilloId { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool EsVegetariano { get; set; }
        public bool Disponible { get; set; }
        public string Tipo { get; set; }
        public string ImagenUrl { get; set; }
    }
}