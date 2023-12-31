﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClamarojBack.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime Fecha { get; set; }
        //public DateTime FechaPedido { get; set; }
        public int IdProveedor { get; set; }
        public int IdPedido { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        public Pedido? Pedido { get; set; }
        public Proveedor? Proveedor { get; set; }
        //public decimal Iva { get; set;}
        //public decimal Subtotal { get; set;}
        //public decimal Descuento { get; set;}
        //public int IdUsuario { get; set; }
        //public Usuario? Usuario { get; set; }
    }
}
