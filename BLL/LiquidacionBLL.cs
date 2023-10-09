using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LiquidacionBLL
    {
        LiquidacionDAL liquidacionDAL = null;
        public List<Liquidacion> liquidacionList = null;

        public LiquidacionBLL()
        {
            liquidacionDAL = new LiquidacionDAL();
            liquidacionList = liquidacionDAL.ConsultarTodos();
        }

        /***************************************** METODO GUARDAR *****************************************/
        public string Guardar (Liquidacion liquidacion)
        {
            if (liquidacion == null)
            {
                return "ERROR... No se Puede Agregar Personas Nulas o Sin informacion.";
            }
            var msg = (liquidacionDAL.Guardar(liquidacion));
            liquidacionList = liquidacionDAL.ConsultarTodos();
            return msg;
        }

        /***************************************** METODO CONSULTAR TODOS *****************************************/
        public List<Liquidacion> ConsultarTodos()
        {
            return liquidacionDAL.ConsultarTodos();
        }

        /***************************************** METODO CALCULAR POR TIPO DE AFILIACION *****************************************/
        public List<Liquidacion> ConsultarTipo(string TipoAfiliacion)
        {
            return ConsultarTodos().Where(l => l.TipoAfiliacion.Contains(TipoAfiliacion)).ToList();
        }

        /***************************************** METODO ELIMINAR *****************************************/
        public bool Eliminar(int NumLiquidacion)
        {
            return liquidacionDAL.Eliminar(NumLiquidacion);
        }

        /***************************************** METODO CALCULAR TARIFA *****************************************/
        public double CalcularTarifa (Liquidacion liquidacion)
        {
            double SalarioMinimo = 1160000;
            double Tarifa = 0;
            if (liquidacion.TipoAfiliacion == "Subsidiado")
            {
                Tarifa = 0.5;
            }
            else
            {
                if (liquidacion.SalarioDevengado <  (SalarioMinimo * 2))
                {
                    Tarifa = 0.15;
                }
                else if ((liquidacion.SalarioDevengado >= (SalarioMinimo * 2)) && (liquidacion.SalarioDevengado <= (SalarioMinimo * 5)))
                {
                    Tarifa = 0.20;
                }
                else
                {
                    Tarifa = 0.25;
                }
            }
            return Tarifa;
        }

        /***************************************** METODO CALCULAR CUOTA MODERADORA *****************************************/
        public double CalcularCuotaModeradora (Liquidacion liquidacion)
        {
            double CuotaModeradora = 0;
            var Tarifa = CalcularTarifa(liquidacion);
            if (liquidacion.TipoAfiliacion == "Subsidiado")
            {
                CuotaModeradora = liquidacion.ValorServicio * Tarifa;
                if (CuotaModeradora > 200000)
                {
                    CuotaModeradora = 200000;
                }
            }
            else
            {
                CuotaModeradora = liquidacion.ValorServicio * Tarifa;
                if (Tarifa == 0.15)
                {
                    CuotaModeradora = 250000;
                }
                else if (Tarifa == 0.20)
                {
                    CuotaModeradora = 900000;
                }
                else
                {
                    CuotaModeradora = 1500000;
                }
            }
            return CuotaModeradora;
        }
    }
}