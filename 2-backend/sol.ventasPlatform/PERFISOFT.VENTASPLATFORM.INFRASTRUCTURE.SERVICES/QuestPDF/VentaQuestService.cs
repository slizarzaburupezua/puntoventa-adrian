using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using static PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure.Parametros;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.QuestPDFLibrary
{
    public class VentaQuestService : IVentaQuestService
    {
        public async Task<(string FileName, string Base64File)> GenerarBoletaFacturaAsync(FiltroGenerarBoletaFactura filtro)
        {
            if (filtro.InformacionNegocio.FORMATO_IMPRESION == FormatoImpresion.TICKETERA)
                return await GenerarBoletaFacturaTicketeraAsync(filtro);

            return await GenerarBoletaFacturaPDFAsync(filtro);
        }

        private async Task<(string FileName, string Base64File)> GenerarBoletaFacturaPDFAsync(FiltroGenerarBoletaFactura filtro)
        {
            using var stream = new MemoryStream();

            string cultureCode = filtro.InformacionNegocio.MONEDA.CULTUREINFO;
            CultureInfo cultureInfo = new CultureInfo(cultureCode);

            byte[] imageData = null;

            if (!string.IsNullOrEmpty(filtro.InformacionNegocio.URLFOTO))
                imageData = await DescargarImagenAsync(filtro.InformacionNegocio?.URLFOTO);

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            string fechaVentaFormateada = filtro.Venta.FECHA_VENTA.ToString("dd 'de' MMMM 'del' yyyy 'a las' HH:mm", cultureInfo);
            fechaVentaFormateada = char.ToUpper(fechaVentaFormateada[0]) + fechaVentaFormateada.Substring(1);

            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {
                        row.ConstantItem(25);

                        if (imageData is not null)
                            row.ConstantItem(45).Image(imageData);

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text(filtro.InformacionNegocio?.RAZON_SOCIAL).Bold().FontSize(10);
                            col.Item().AlignCenter().Width(180) .Text(filtro.InformacionNegocio?.DIRECCION).FontSize(9);   
                            col.Item().AlignCenter().Text("Contacto: " + filtro.InformacionNegocio?.CELULAR).FontSize(9);
                            col.Item().AlignCenter().Text("Correo: " + filtro.InformacionNegocio?.CORREO_ELECTRONICO).FontSize(9);
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor(filtro.InformacionNegocio?.COLOR_BOLETA_FACTURA)
                            .AlignCenter().Text("RUC " + filtro.InformacionNegocio.RUC);

                            col.Item().Background(filtro.InformacionNegocio?.COLOR_BOLETA_FACTURA).Border(1)
                            .BorderColor(filtro.InformacionNegocio?.COLOR_BOLETA_FACTURA).AlignCenter()
                            .Text("Boleta de venta").FontColor("#fff");

                            col.Item().Border(1).BorderColor(filtro.InformacionNegocio?.COLOR_BOLETA_FACTURA).
                            AlignCenter().Text(filtro.Venta.NUMERO_VENTA);

                        });
                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Column(col2 =>
                        {

                            col2.Item().Row(row =>
                            {
                                row.RelativeItem().Text(txt =>
                                {
                                    txt.Span("Nombre: ").SemiBold().FontSize(10);
                                    txt.Span(filtro.InformacionCliente?.NOMBRES + " " + filtro.InformacionCliente?.APELLIDOS).FontSize(10);
                                });

                                row.RelativeItem().Text(txt =>
                                {
                                    txt.Span("Número de Documento: ").SemiBold().FontSize(10);
                                    txt.Span(filtro.InformacionCliente?.NUMERO_DOCUMENTO).FontSize(10);
                                });
                            });

                            col2.Item().Row(row =>
                            {
                                row.RelativeItem().Text(txt =>
                                {
                                    txt.Span("Dirección: ").SemiBold().FontSize(10);
                                    txt.Span(filtro.InformacionCliente?.DIRECCION).FontSize(10);
                                });

                                row.RelativeItem().Text(txt =>
                                {
                                    txt.Span("Número de Contacto: ").SemiBold().FontSize(10);
                                    txt.Span(filtro.InformacionCliente?.CELULAR).FontSize(10);
                                });
                            });
                        });


                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background(filtro.InformacionNegocio?.COLOR_BOLETA_FACTURA)
                                .Padding(2).Text("CANT.").FontColor("#fff");

                                header.Cell().Background(filtro.InformacionNegocio?.COLOR_BOLETA_FACTURA)
                                .Padding(2).Text("PRODUCTO").FontColor("#fff");

                                header.Cell().Background(filtro.InformacionNegocio?.COLOR_BOLETA_FACTURA)
                               .Padding(2).Text("P.UNITARIO").FontColor("#fff");

                                header.Cell().Background(filtro.InformacionNegocio?.COLOR_BOLETA_FACTURA)
                               .Padding(2).Text("TOTAL").FontColor("#fff");
                            });

                            foreach (var detalleVenta in filtro.LstDetalleVenta)
                            {
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(detalleVenta.CANTIDAD.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(detalleVenta.NOMBRE_PRODUCTO.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(detalleVenta.PRECIO_VENTA.ToString("C", cultureInfo)).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignRight().Text(detalleVenta.PRECIO_TOTAL.ToString("C", cultureInfo)).FontSize(10);
                            }
                        });

                        col1.Item().AlignRight().Text(filtro.Venta.PRECIO_TOTAL.ToString("C", cultureInfo)).FontSize(12);

                        if (!string.IsNullOrWhiteSpace(filtro.Venta.NOTA_ADICIONAL))
                        {
                            col1.Item().Background(Colors.Grey.Lighten3).Padding(10)
                            .Column(column =>
                            {
                                column.Item().Text("Nota Adicional").FontSize(14).Bold();
                                column.Item().Text(filtro.Venta.NOTA_ADICIONAL).FontSize(10);
                            });
                        }

                        // Fecha formateada
                        string fechaVentaFormateada = filtro.Venta.FECHA_VENTA.ToString("dd 'de' MMMM 'del' yyyy", new CultureInfo("es-PE"));
                        fechaVentaFormateada = char.ToUpper(fechaVentaFormateada[0]) + fechaVentaFormateada.Substring(1);
                         
                        col1.Item().AlignRight().Text($"{fechaVentaFormateada}").FontSize(10);
                        col1.Spacing(10);
                    });


                    page.Footer()
                    .AlignRight()
                    .Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            });

            data.GeneratePdf(stream);
            byte[] pdfBytes = stream.ToArray();
            string base64Pdf = Convert.ToBase64String(pdfBytes);
            string fileName = $"Boleta_{filtro.Venta.NUMERO_VENTA}.pdf";

            return (fileName, base64Pdf);
        }

        private async Task<(string FileName, string Base64File)> GenerarBoletaFacturaTicketeraAsync(FiltroGenerarBoletaFactura filtro)
        {
            using var stream = new MemoryStream();

            string cultureCode = filtro.InformacionNegocio.MONEDA.CULTUREINFO;
            CultureInfo cultureInfo = new CultureInfo(cultureCode);

            byte[] imageData = null;

            if (!string.IsNullOrEmpty(filtro.InformacionNegocio.URLFOTO))
                imageData = await DescargarImagenAsync(filtro.InformacionNegocio.URLFOTO);

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.ContinuousSize(Numeracion.Tres, Unit.Inch);
                    page.MarginVertical(Numeracion.Cinco);
                    page.MarginHorizontal(Numeracion.Diez);

                    page.Content().AlignCenter().Column(column =>
                    {
                        if (imageData != null)
                        {
                            column.Item()
                                .AlignCenter()
                                .PaddingTop(Numeracion.Veinte)
                                .PaddingBottom(Numeracion.Veinte)
                                .Height(Numeracion.Cincuenta)
                                .Width(Numeracion.Cincuenta)
                                .Image(imageData)
                                .FitArea();
                        }

                        column.Item()
                              .AlignCenter()
                              .Text(text =>
                              {
                                  text.Line(filtro.InformacionNegocio?.RAZON_SOCIAL.ToUpper()).Bold().FontSize(Numeracion.Nueve);
                                  text.Line($"N° de Documento: {filtro.InformacionNegocio.RUC}").FontSize(Numeracion.Siete);
                                  text.Line($"Dirección: {filtro.InformacionNegocio.DIRECCION}").FontSize(Numeracion.Siete);
                                  text.Line($"Contacto: {filtro.InformacionNegocio.CELULAR}").FontSize(Numeracion.Siete);
                                  text.Line($"Correo:  {filtro.InformacionNegocio.CORREO_ELECTRONICO}").FontSize(Numeracion.Siete);
                              });

                        if (filtro.InformacionCliente != null)
                        {
                            column.Item()

                        .PaddingBottom(Numeracion.Cinco)
                        .PaddingTop(Numeracion.Cinco)
                        .LineHorizontal(Numeracion.Uno)
                        .LineColor(Colors.Grey.Medium);

                            column.Item()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Line($"Cliente: {filtro.InformacionCliente?.NOMBRES} {filtro.InformacionCliente?.APELLIDOS}").FontSize(Numeracion.Siete);
                                text.Line($"N° de Identidad: {filtro.InformacionCliente?.NUMERO_DOCUMENTO}").FontSize(Numeracion.Siete);
                                text.Line($"Dirección: {filtro.InformacionCliente?.DIRECCION}").FontSize(Numeracion.Siete);
                            });
                        }

                        column.Item()
                        .PaddingBottom(-Numeracion.Dos)
                        .PaddingBottom(Numeracion.Cinco)
                        .PaddingTop(Numeracion.Cinco)
                        .LineHorizontal(Numeracion.Uno)
                        .LineColor(Colors.Grey.Medium);

                        column.Item()
                              .AlignCenter()
                              .Text(text =>
                              {
                                  text.Line($"NOTA DE VENTA").Bold().FontSize(Numeracion.Nueve);
                                  text.Line($"N°: {filtro.Venta.NUMERO_VENTA}").FontSize(Numeracion.Siete);
                                  text.Line($"F. Emisión: {filtro.Venta.FECHA_VENTA:dd/MM/yyyy HH:mm}").FontSize(Numeracion.Siete);
                                  if (!string.IsNullOrEmpty(filtro.Venta.NOTA_ADICIONAL))
                                      text.Line($"Nota adicional: {filtro.Venta.NOTA_ADICIONAL}").FontSize(Numeracion.Siete);
                              });

                        column.Item()
                              .LineHorizontal(Numeracion.Uno)
                              .LineColor(Colors.Grey.Medium);

                        column.Item()

                              .Table(table =>
                              {
                                  table.ColumnsDefinition(columns =>
                                  {
                                      columns.RelativeColumn(Numeracion.Cuatro);
                                      columns.RelativeColumn(Numeracion.Uno);
                                      columns.RelativeColumn(Numeracion.Dos);
                                      columns.RelativeColumn(Numeracion.Dos);
                                  });

                                  table.Cell().Text("Producto").Bold().FontSize(Numeracion.Siete);
                                  table.Cell().AlignCenter().Text("Cant.").Bold().FontSize(Numeracion.Siete);
                                  table.Cell().AlignRight().Text("Precio").Bold().FontSize(Numeracion.Siete);
                                  table.Cell().AlignRight().Text("Total").Bold().FontSize(Numeracion.Siete);

                                  table.Cell().ColumnSpan(Numeracion.Cuatro).PaddingVertical(Numeracion.Cinco).LineHorizontal(1).LineColor(Colors.Grey.Medium);

                                  foreach (var item in filtro.LstDetalleVenta)
                                  {
                                      table.Cell().PaddingBottom(Numeracion.Dos).Text(item.NOMBRE_PRODUCTO.Length > Numeracion.Veinte ? item.NOMBRE_PRODUCTO.Substring(Numeracion.Cero, Numeracion.Veinte) : item.NOMBRE_PRODUCTO).FontSize(Numeracion.Siete);
                                      table.Cell().PaddingBottom(Numeracion.Dos).AlignCenter().Text(item.CANTIDAD.ToString()).FontSize(Numeracion.Siete);
                                      table.Cell().PaddingBottom(Numeracion.Dos).AlignRight().Text(item.PRECIO_VENTA.ToString("C", cultureInfo)).FontSize(Numeracion.Siete);
                                      table.Cell().PaddingBottom(Numeracion.Dos).AlignRight().Text(item.PRECIO_TOTAL.ToString("C", cultureInfo)).FontSize(Numeracion.Siete);
                                  }

                                  table.Cell().ColumnSpan(Numeracion.Cuatro).PaddingVertical(Numeracion.Cinco).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                                  table.Cell().ColumnSpan(Numeracion.Tres).AlignRight().Text("TOTAL A PAGAR").Bold().FontSize(Numeracion.Siete);
                                  table.Cell().AlignRight().Text(filtro.Venta.PRECIO_TOTAL.ToString("C", cultureInfo)).Bold().FontSize(Numeracion.Siete);
                              });

                        column.Item()
                        .PaddingTop(Numeracion.Veinte)
                        .PaddingBottom(Numeracion.Cincuenta)
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Line("¡Gracias por su compra!").FontSize(Numeracion.Siete);
                            text.Line("Este es un comprobante generado automáticamente.").FontSize(Numeracion.Siete);
                        });
                    });
                });
            });

            document.WithMetadata(new DocumentMetadata
            {
                Title = $"Venta {filtro.Venta.NUMERO_VENTA}",
                Author = filtro.InformacionNegocio.RAZON_SOCIAL,
                Creator = filtro.InformacionNegocio.RAZON_SOCIAL,
                PdfA = true
            });

            document.GeneratePdf(stream);
            byte[] pdfBytes = stream.ToArray();
            string base64Pdf = Convert.ToBase64String(pdfBytes);
            string fileName = $"Boleta_{filtro.Venta.NUMERO_VENTA}.pdf";

            return (fileName, base64Pdf);
        }

        public async Task<byte[]> DescargarImagenAsync(string url)
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetByteArrayAsync(url);
        }

    }
}
