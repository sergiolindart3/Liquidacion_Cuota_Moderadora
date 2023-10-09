using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LiquidacionDAL
    {
        string fileName = "LiquidacionCuotaModeradora.txt";

        /***************************************** METODO GUARDAR *****************************************/
        public string Guardar (Liquidacion liquidacion)
        {
            using (var escritor = new StreamWriter(fileName, true))
            {
                escritor.WriteLine(liquidacion.ToString());
            }
            return $"--> Se Agrego el Numero de Liquidacion {liquidacion.NumLiquidacion} <--";
        }

        /***************************************** METODO CONSULTAR TODOS *****************************************/
        public List<Liquidacion> ConsultarTodos ()
        {
            var listaLiquidacion = new List<Liquidacion>();
            try
            {
                var lector = new StreamReader(fileName);
                while (!lector.EndOfStream)
                {
                    listaLiquidacion.Add(Map(lector.ReadLine()));
                }
                lector.Close();
                return listaLiquidacion;
            }
            catch (Exception)
            {
                return null;
            } 
        }

        /***************************************** METODO ELIMINAR *****************************************/
        public bool Eliminar (int NumLiquidacion)
        {
            try
            {
                var listaLiquidacion = ConsultarTodos();
                if (listaLiquidacion != null)
                {
                    var LiquidacionAEliminar = listaLiquidacion.FirstOrDefault(e => e.NumLiquidacion == NumLiquidacion);
                    if (LiquidacionAEliminar != null)
                    {
                        listaLiquidacion.Remove(LiquidacionAEliminar);
                        using (var escritor = new StreamWriter(fileName, false))
                        {
                            foreach (var liquidacion in listaLiquidacion)
                            {
                                escritor.WriteLine(liquidacion.ToString());
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /***************************************** METODO MAP *****************************************/
        private Liquidacion Map (string linea)
        {
            var l = new Liquidacion
            {
                NumLiquidacion = int.Parse(linea.Split('-')[0]),
                FechaLiquidacion = DateTime.Parse(linea.Split('-')[1]),
                IdPaciente = int.Parse(linea.Split('-')[2]),
                TipoAfiliacion = linea.Split('-')[3],
                SalarioDevengado = double.Parse(linea.Split('-')[4]),
                ValorServicio = double.Parse(linea.Split('-')[5])
            };
            return l;
        }
    }
}