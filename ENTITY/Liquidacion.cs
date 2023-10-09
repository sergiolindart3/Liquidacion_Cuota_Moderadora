using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Liquidacion
    {
        public int NumLiquidacion { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public int IdPaciente { get; set; }
        public string TipoAfiliacion { get; set; }
        public double SalarioDevengado { get; set; }
        public double ValorServicio { get; set; }

        public Liquidacion()
        {
        }

        public Liquidacion(int numLiquidacion, DateTime fechaLiquidacion, int idPaciente, string tipoAfiliacion, double salarioDevengado, double valorServicio)
        {
            NumLiquidacion = numLiquidacion;
            FechaLiquidacion = fechaLiquidacion;
            IdPaciente = idPaciente;
            TipoAfiliacion = tipoAfiliacion;
            SalarioDevengado = salarioDevengado;
            ValorServicio = valorServicio;
        }

        public override string ToString()
        {
            return $"{NumLiquidacion} - {FechaLiquidacion.Date.ToString("dd/MM/yyyy")} - {IdPaciente} - {TipoAfiliacion} - {SalarioDevengado} - {ValorServicio}";
        }
    }
}