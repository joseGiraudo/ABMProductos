using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABMProductos
{
    public class Producto
    {
        private int codigo;
        private string detalle;
        private double precio;
        private int tipo;
        private int marca;
        private DateTime fecha;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Detalle
        {
            get { return detalle; }
            set { detalle = value; }
        }
        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public int Marca
        {
            get { return marca; }
            set { marca = value; }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        override public string ToString()
        {
            return codigo + " - " + detalle;
        }
      
    }
}
