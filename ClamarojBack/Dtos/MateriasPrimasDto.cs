namespace ClamarojBack.Dtos
{
    public class MateriasPrimasDto
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int Perecedero { get; set; }
        public int Stock { get; set; }
        public int CantMinima { get; set; }
        public int CantMaxima { get; set; }
        public int IdUnidadMedida { get; set; }
        public decimal Precio { get; set; }
        public string? Foto { get; set; }
        public int IdProveedor { get; set; }
        public int IdStatus { get; set; }
    }
}
