namespace ClamarojBack.Dtos
{
    public class RecetasDto
    {
        public int IdReceta { get; set; }
        public string? Codigo { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Costo { get; set; }
        public int IdProducto { get; set; }
        public int IdStatus { get; set; }
    }
}
