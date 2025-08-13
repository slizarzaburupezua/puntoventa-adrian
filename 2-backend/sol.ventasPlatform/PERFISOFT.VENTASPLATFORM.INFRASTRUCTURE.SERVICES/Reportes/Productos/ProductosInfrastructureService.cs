using ClosedXML.Excel;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using Serilog;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Productos
{
    public class ProductosInfrastructureService : IProductosInfrastructureService
    {
        public async Task<byte[]> GenerarReportePorProductosAsync(FiltroReportePorProductos filtro)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "templates", "reportes", "ventas", "porProductos.xlsx");

            if (!File.Exists(filePath))
                Log.Error(LogMessages.Venta.Reporte.TemplateNoEncontradoError, filePath);

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet("Productos");

                //// Titulo Resumen Desde
                worksheet.Range("B4:B5").Merge(); // Fusiona B4 y B5
                worksheet.Range("B4:B5").Value = filtro.FechaDesde?.Date;

                ////// Titulo Resumen Hasta
                worksheet.Range("B6:B7").Merge(); // Fusiona B6 y B7
                worksheet.Range("b6:B7").Value = filtro.FechaHasta?.Date;

                // Llenar ventas de los productos en A27 y montos en B27
                int rowInitial = 27;
                int rowNext = 30;
                foreach (var detalleVentaProducto in filtro.LstDetalleTotalProductos)
                {
                    if (rowInitial > rowNext) // Si la fila actual excede la fila 30 
                    {
                        rowNext = rowInitial;
                        worksheet.Row(rowNext).InsertRowsBelow(1); // Insertar una fila adicional
                    }

                    worksheet.Cell(rowInitial, 1).Value = detalleVentaProducto.NombreProducto; // Columna A
                    worksheet.Cell(rowInitial, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left; // Columna A
                    worksheet.Cell(rowInitial, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Columna A

                    worksheet.Cell(rowInitial, 2).Value = detalleVentaProducto.TotalVenta; // Columna B
                    worksheet.Cell(rowInitial, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left; // Columna B
                    worksheet.Cell(rowInitial, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Columna B
                    worksheet.Cell(rowInitial, 2).Style.NumberFormat.Format = $"\"{filtro.CodigoMoneda}\" #,##0.00"; // Columna B
                    rowInitial++;
                }

                // Ordenar las productos por TotalVentas de mayor a menor
                var productosOrdenados = filtro.LstDetalleTotalProductos
                    .OrderBy(c => c.TotalVenta)
                    .ToList();

                // Llenar ventas de los productos en A27 y montos en B27
                rowInitial = 27;
                foreach (var producto in productosOrdenados)
                {
                    if (rowInitial > rowNext) // Si la fila actual excede la fila 30 
                    {
                        rowNext = rowInitial;
                        worksheet.Row(rowNext).InsertRowsBelow(1); // Insertar una fila adicional
                    }

                    worksheet.Cell(rowInitial, 4).Value = producto.NombreProducto; // Columna D
                    worksheet.Cell(rowInitial, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left; // Columna D
                    worksheet.Cell(rowInitial, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Columna D

                    worksheet.Cell(rowInitial, 5).Value = producto.TotalVenta; // Columna E
                    worksheet.Cell(rowInitial, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left; // Columna E
                    worksheet.Cell(rowInitial, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Columna E
                    worksheet.Cell(rowInitial, 5).Style.NumberFormat.Format = $"\"{filtro.CodigoMoneda}\" #,##0.00"; // Columna E
                    rowInitial++;
                }

                // Agrupar los productos por Fecha y sumar los totales
                var ventasAgrupadosPorFecha = filtro.LstDetalleVenta
                    .GroupBy(producto => producto.FECHA_REGISTRO.Date) // Agrupa por fecha
                    .Select(grupoProducto => new
                    {
                        Fecha = grupoProducto.Key,
                        TotalVenta = grupoProducto.Sum(x => x.PRECIO_TOTAL)
                    })
                    .OrderBy(grupo => grupo.Fecha) // Ordenar por fecha ascendente
                    .ToList();


                // Llenar los datos agrupados en las filas del Excel
                rowInitial = 27;
                foreach (var detalleVenta in ventasAgrupadosPorFecha)
                {
                    if (rowInitial > rowNext) // Si la fila actual excede la fila 30 
                    {
                        rowNext = rowInitial;
                        worksheet.Row(rowNext).InsertRowsBelow(1); // Insertar una fila adicional
                    }

                    worksheet.Cell(rowInitial, 7).Value = detalleVenta.Fecha.ToString("dd/MM/yyyy"); // Columna A
                    worksheet.Cell(rowInitial, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left; // Columna A
                    worksheet.Cell(rowInitial, 7).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Columna A

                    worksheet.Cell(rowInitial, 8).Value = detalleVenta.TotalVenta; // Columna B
                    worksheet.Cell(rowInitial, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right; // Columna B
                    worksheet.Cell(rowInitial, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Columna B
                    worksheet.Cell(rowInitial, 8).Style.NumberFormat.Format = $"\"{filtro.CodigoMoneda}\" #,##0.00"; // Columna B
                    rowInitial++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    return await Task.FromResult(stream.ToArray());
                }
            }
        }
    }
}
