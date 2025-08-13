export class ObtenerDetalleVentaRequest {
    destinationTimeZoneId: string;
    idUsuario: string;
    idVenta?: number;
    lstUsuario?: string[];
    numeroVenta?: string ;
    fechaVentaInicio: Date;
    fechaVentaFin: Date;
}
