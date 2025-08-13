export class DetalleVentaDTO {
    idProducto: number;
    idVenta: number;
    numeroVenta: string;
    idCliente?: number;
    nombreCliente?: string;
    direccion?: string;
    urlBoletaFactura: string;
    fechaVenta: Date;
    urlFotoProducto: string;
    nombreProducto: string;
    colorProducto: string;
    nombreCategoria: string;
    colorCategoria: string;
    nombreMarca: string;
    colorMarca: string;
    cantidad: number;
    precioProducto: number;
    precioTotal: number;
    notaAdicional: string;
}
