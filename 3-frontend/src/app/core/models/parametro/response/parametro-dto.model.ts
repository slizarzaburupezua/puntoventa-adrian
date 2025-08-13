export interface ParametroDTO {
    id: number;
    nombre: string;
    descripcion: string;
    activo: boolean;
    estado: boolean;
    fechaRegistro: string;
    fechaActualizacion?: string;
    fechaAnulacion?: string;
}
