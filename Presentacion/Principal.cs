using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class Principal
    {
        static void Main(string[] args)
        {
            LiquidacionBLL liquidacionBLL = new LiquidacionBLL();
            int opcion;
            do
            {
                Console.Clear();
                int VentanaAncho = Console.WindowWidth;
                string Titulo = "SISTEMA DE LIQUIDACION CUOTA MODERADORA";
                int x = (VentanaAncho - Titulo.Length) / 2;
                Console.SetCursorPosition(x, 2); Console.Write(Titulo);
                Console.SetCursorPosition(55, 5); Console.Write("1. Registrar Liquidacion");
                Console.SetCursorPosition(55, 6); Console.Write("2. Consultar Todos");
                Console.SetCursorPosition(55, 7); Console.Write("3. Consultar por Tipo de Afiliacion");
                Console.SetCursorPosition(55, 8); Console.Write("4. Consultar Valores Totales de CML");
                Console.SetCursorPosition(55, 9); Console.Write("5. Consultar por Fechas");
                Console.SetCursorPosition(55, 10); Console.Write("6. Consultar por Id Paciente");
                Console.SetCursorPosition(55, 11); Console.Write("7. Modificar Liquidacion");
                Console.SetCursorPosition(55, 12); Console.Write("8. Eliminar Liquidacion");
                Console.SetCursorPosition(55, 13); Console.Write("9. Salir");
                Console.SetCursorPosition(56, 14); Console.Write("-> Seleccione una opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.SetCursorPosition(64, 16); Console.Write("--> ERROR... Digite una Opción Valida <--");
                    Console.ReadKey();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        RegistrarLiquidacion();
                        break;
                    case 2:
                        ConsultarTodos();
                        break;
                    case 3:
                        ConsultarTipo();
                        break;
                    case 4:
                        ConsultarValorTotal();
                        break;
                    case 5:
                        ConsultarFecha();
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;

                    default:
                        Console.SetCursorPosition(64, 16); Console.Write("--> ERROR... Digite una Opción Valida <--");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 9);

            /***************************************** FUNCION REGISTRAR *****************************************/
            void RegistrarLiquidacion()
            {
                Console.Clear();
                int VentanaAncho = Console.WindowWidth;
                string Titulo = "REGISTRAR LIQUIDACION";
                int x = (VentanaAncho - Titulo.Length) / 2;
                Console.SetCursorPosition(x, 2); Console.Write(Titulo);
                Console.SetCursorPosition(55, 5); Console.Write("Numero Liquidacion: ");
                if (!int.TryParse(Console.ReadLine(), out int numLiquidacion))
                {
                    Console.SetCursorPosition(64, 12); Console.Write("ERROR... Numero de Liquidacion no valido");
                    Console.ReadKey();
                    return;
                }
                var listaLiquidacion = liquidacionBLL.ConsultarTodos();
                if (listaLiquidacion != null && listaLiquidacion.Any(l => l.NumLiquidacion == numLiquidacion))
                {
                    Console.SetCursorPosition(42, 12); Console.Write($"--> ERROR... El Numero de Liquidacion {numLiquidacion} ya existe y no se puede agregar nuevamente <--");
                    Console.ReadKey();
                    return;
                }

                Console.SetCursorPosition(55, 6); Console.Write("Fecha Liquidacion (dd-mm-yyyy): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime fechaLiquidacion))
                {
                    Console.SetCursorPosition(64, 12); Console.Write("ERROR... Fecha de Liquidacion no valida");
                    Console.ReadKey();
                    return;
                }
                Console.SetCursorPosition(55, 7); Console.Write("Id Paciente: ");
                if (!int.TryParse(Console.ReadLine(), out int idPaciente))
                {
                    Console.SetCursorPosition(64, 12); Console.Write("ERROR... Identificacion no valida");
                    Console.ReadKey();
                    return;
                }
                
                Console.SetCursorPosition(55, 8); Console.Write("¿Tipo de Afiliacion? (S - Subsidiado / C - Contributivo): ");
                string tipoAfiliacion = Console.ReadLine().ToUpper();
                if (tipoAfiliacion == "S")
                {
                    tipoAfiliacion = "Subsidiado";
                }
                else if (tipoAfiliacion == "C")
                {
                    tipoAfiliacion = "Contributivo";
                }
                else
                {
                    Console.SetCursorPosition(64, 12); Console.Write("ERROR... Tipo de Afiliacion no valido");
                    Console.ReadKey();
                    return;
                }

                Console.SetCursorPosition(55, 9); Console.Write("Salario Devengado: ");
                if (!double.TryParse(Console.ReadLine(), out double salarioDevengado))
                {
                    Console.SetCursorPosition(64, 12); Console.Write("ERROR... Salario Devengado no valido");
                    Console.ReadKey();
                    return;
                }

                Console.SetCursorPosition(55, 10); Console.Write("Valor Servicio: ");
                if (!double.TryParse(Console.ReadLine(), out double valorServicio))
                {
                    Console.SetCursorPosition(64, 12); Console.Write("ERROR... Valor de Servicio no valido");
                    Console.ReadKey();
                    return;
                }

                Liquidacion nuevaLiquidacion = new Liquidacion
                {
                    NumLiquidacion = numLiquidacion,
                    FechaLiquidacion = fechaLiquidacion.Date,
                    IdPaciente = idPaciente,
                    TipoAfiliacion = tipoAfiliacion,
                    SalarioDevengado = salarioDevengado,
                    ValorServicio = valorServicio,
                };

                string resultadoGuardar = liquidacionBLL.Guardar(nuevaLiquidacion);
                Console.SetCursorPosition(60, 12); Console.Write(resultadoGuardar);
                Console.ReadKey();
            }

            /***************************************** FUNCION CONSULTAR *****************************************/
            void ConsultarTodos()
            {
                Console.Clear();
                int VentanaAncho = Console.WindowWidth;
                string Titulo = "CONSULTAR TODOS";
                int x = (VentanaAncho - Titulo.Length) / 2;
                Console.SetCursorPosition(x, 2); Console.Write(Titulo);
                List<Liquidacion> listLiquidacion = liquidacionBLL.ConsultarTodos();
                if (listLiquidacion != null && listLiquidacion.Count > 0)
                {
                    Console.SetCursorPosition(1, 5); Console.Write("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    Console.SetCursorPosition(1, 6); Console.Write("|   Id Paciente   | Tipo de Afiliacion | Salario Devengado |  Valor Servicio  |   Tarifa Aplicada   | Valor Liquidado | Aplico Tope Maximo |   Valor Cuota Moderadora  |");
                    Console.SetCursorPosition(1, 7); Console.Write("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    int i = 8;
                    foreach (var liquidacion in listLiquidacion)
                    {
                        var Tarifa = liquidacionBLL.CalcularTarifa(liquidacion);
                        int TarifaAplicada = 0;
                        if (Tarifa == 0.5)
                        {
                            TarifaAplicada = 5;
                        }
                        else if (Tarifa == 0.15)
                        {
                            TarifaAplicada = 15;
                        }
                        else if (Tarifa == 0.20)
                        {
                            TarifaAplicada = 20;
                        }
                        else
                        {
                            TarifaAplicada = 25;
                        }
                        var ValorLiquidado = liquidacionBLL.CalcularCuotaModeradora(liquidacion);
                        double CuotaModeradora = liquidacion.ValorServicio * Tarifa;
                        string AplicaTope = "NO";
                        if ((liquidacion.TipoAfiliacion == "Subsidiado") && (CuotaModeradora > 200000))
                        {
                            AplicaTope = "SI";
                        }
                        else
                        {
                            if ((Tarifa == 0.15) && (CuotaModeradora > 250000))
                            {
                                AplicaTope = "SI";
                            }
                            else if ((Tarifa == 0.20) && (CuotaModeradora > 900000))
                            {
                                AplicaTope = "SI";
                            }
                            else if ((Tarifa == 0.25) && (CuotaModeradora > 1500000))
                            {
                                AplicaTope = "SI";
                            }
                        }
                        Console.SetCursorPosition(1, i); Console.Write("|");
                        Console.SetCursorPosition(5, i); Console.Write(liquidacion.IdPaciente);
                        Console.SetCursorPosition(23, i); Console.Write(liquidacion.TipoAfiliacion);
                        Console.SetCursorPosition(47, i); Console.Write(liquidacion.SalarioDevengado);
                        Console.SetCursorPosition(67, i); Console.Write(liquidacion.ValorServicio);
                        Console.SetCursorPosition(88, i); Console.Write($"{TarifaAplicada} %");
                        Console.SetCursorPosition(107, i); Console.Write(ValorLiquidado);
                        Console.SetCursorPosition(129, i); Console.Write(AplicaTope);
                        Console.SetCursorPosition(153, i); Console.Write(CuotaModeradora);
                        Console.SetCursorPosition(168, i); Console.Write("|");
                        i++;
                    }
                    Console.SetCursorPosition(1, i); Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }
                else
                {
                    Console.SetCursorPosition(55, 5); Console.Write("--> ERROR... No hay liquidaciones registradas <--");
                }
                Console.ReadKey();
            }

            /***************************************** FUNCION CONSULTAR POR TIPO DE AFILIACION *****************************************/
            void ConsultarTipo()
            {
                Console.Clear();
                int VentanaAncho = Console.WindowWidth;
                string Titulo = "CONSULTAR POR TIPO DE AFILIACION";
                int x = (VentanaAncho - Titulo.Length) / 2;
                Console.SetCursorPosition(x, 2); Console.Write(Titulo);
                Console.SetCursorPosition(55, 5); Console.Write("Tipo de Afiliacion (S - Subsidiado / C - Contributivo): ");
                string tipoAfiliacion = null;
                tipoAfiliacion = Console.ReadLine().ToUpper();
                if (tipoAfiliacion == "S")
                {
                    tipoAfiliacion = "Subsidiado";
                }
                else if (tipoAfiliacion == "C")
                {
                    tipoAfiliacion = "Contributivo";
                }
                else
                {
                    Console.SetCursorPosition(66, 8); Console.Write("ERROR... Tipo de Afiliacion no valido");
                    Console.ReadKey();
                    return;
                }
                List<Liquidacion> listTipo = liquidacionBLL.ConsultarTipo(tipoAfiliacion);
                if (listTipo != null && listTipo.Count > 0)
                {
                    int TotalLiquidaciones = liquidacionBLL.ConsultarTodos().Count;
                    int TotalTipo = listTipo.Count;
                    Console.SetCursorPosition(1, 7); Console.Write($"Total de Liquidaciones: {TotalLiquidaciones}");
                    Console.SetCursorPosition(1, 8); Console.Write($"Total de Liquidaciones del Regimen {tipoAfiliacion}: {TotalTipo}");
                    Console.SetCursorPosition(1, 10); Console.Write("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    Console.SetCursorPosition(1, 11); Console.Write("|   Id Paciente   | Tipo de Afiliacion | Salario Devengado |  Valor Servicio  |   Tarifa Aplicada   | Valor Liquidado | Aplico Tope Maximo |   Valor Cuota Moderadora  |");
                    Console.SetCursorPosition(1, 12); Console.Write("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    int i = 13;
                    foreach (var liquidacion in listTipo)
                    {
                        var Tarifa = liquidacionBLL.CalcularTarifa(liquidacion);
                        int TarifaAplicada = 0;
                        if (Tarifa == 0.5)
                        {
                            TarifaAplicada = 5;
                        }
                        else if (Tarifa == 0.15)
                        {
                            TarifaAplicada = 15;
                        }
                        else if (Tarifa == 0.20)
                        {
                            TarifaAplicada = 20;
                        }
                        else
                        {
                            TarifaAplicada = 25;
                        }
                        var ValorLiquidado = liquidacionBLL.CalcularCuotaModeradora(liquidacion);
                        double CuotaModeradora = liquidacion.ValorServicio * Tarifa;
                        string AplicaTope = "NO";
                        if ((liquidacion.TipoAfiliacion == "Subsidiado") && (CuotaModeradora > 200000))
                        {
                            AplicaTope = "SI";
                        }
                        else
                        {
                            if ((Tarifa == 0.15) && (CuotaModeradora > 250000))
                            {
                                AplicaTope = "SI";
                            }
                            else if ((Tarifa == 0.20) && (CuotaModeradora > 900000))
                            {
                                AplicaTope = "SI";
                            }
                            else if ((Tarifa == 0.25) && (CuotaModeradora > 1500000))
                            {
                                AplicaTope = "SI";
                            }
                        }
                        Console.SetCursorPosition(1, i); Console.Write("|");
                        Console.SetCursorPosition(5, i); Console.Write(liquidacion.IdPaciente);
                        Console.SetCursorPosition(23, i); Console.Write(liquidacion.TipoAfiliacion);
                        Console.SetCursorPosition(47, i); Console.Write(liquidacion.SalarioDevengado);
                        Console.SetCursorPosition(67, i); Console.Write(liquidacion.ValorServicio);
                        Console.SetCursorPosition(88, i); Console.Write($"{TarifaAplicada} %");
                        Console.SetCursorPosition(107, i); Console.Write(ValorLiquidado);
                        Console.SetCursorPosition(129, i); Console.Write(AplicaTope);
                        Console.SetCursorPosition(153, i); Console.Write(CuotaModeradora);
                        Console.SetCursorPosition(168, i); Console.Write("|");
                        i++;
                    }
                    Console.SetCursorPosition(1, i); Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }
                else
                {
                    Console.SetCursorPosition(60, 8); Console.Write("--> ERROR... No hay liquidaciones registradas <--");
                }
                Console.ReadKey();
            }
            /***************************************** FUNCION CONSULTAR VALORES TOTALES DE CML *****************************************/
            void ConsultarValorTotal()
            {
                Console.Clear();
                int VentanaAncho = Console.WindowWidth;
                string Titulo = "CONSULTAR VALORES TOTALES DE CUOTAS MODERADORAS";
                int x = (VentanaAncho - Titulo.Length) / 2;
                Console.SetCursorPosition(x, 2); Console.Write(Titulo);
                var listLiquidacion = liquidacionBLL.ConsultarTodos();
                if (listLiquidacion.Count > 0)
                {
                    double ValorTotalCML = listLiquidacion.Sum(l => liquidacionBLL.CalcularCuotaModeradora(l));
                    double valorTotalSubsidiado = listLiquidacion.Where(l => l.TipoAfiliacion.Contains("Subsidiado")).Sum(l => liquidacionBLL.CalcularCuotaModeradora(l));
                    double valorTotalContributivo = listLiquidacion.Where(l => l.TipoAfiliacion.Contains("Contributivo")).Sum(l => liquidacionBLL.CalcularCuotaModeradora(l));
                    Console.SetCursorPosition(57, 5); Console.Write($"Valor Total de Cuotas Moderadoras Liquidadas: {ValorTotalCML}");
                    Console.SetCursorPosition(57, 6); Console.Write($"Valor Total Liquidado por Regimen Subsidiado: {valorTotalSubsidiado}");
                    Console.SetCursorPosition(57, 7); Console.Write($"Valor Total Liquidado por Regimen Contributivo: {valorTotalContributivo}");
                }
                else
                {
                    Console.SetCursorPosition(60, 5); Console.Write("--> ERROR... No hay liquidaciones registradas <--");
                }
                Console.ReadKey();
            }

            /***************************************** FUNCION CONSULTAR POR FECHAS *****************************************/
            void ConsultarFecha()
            {

            }
        }
    }
}