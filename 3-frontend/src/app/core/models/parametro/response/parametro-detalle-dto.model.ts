export interface ParametroDetalleDTO {
    id: number;
    idParametro: number;
    paraKey: string;
    subParaKey: string;
    nombre: string;
    descripcion: string;
    tipoCampo: string;
    orden?: number;
    svalor1: string;
    svalor2: string;
    svalor3: string;
    dvalor1?: number;
    dvalor2?: number;
    dvalor3?: number;
    fvalor1?: string;
    fvalor2?: string;
    fvalor3?: string;
    bvalor1?: boolean;
    bvalor2?: boolean;
    bvalor3?: boolean;
    activo: boolean;
    estado: boolean;
    fechaRegistro: string;
    fechaActualizacion?: string;
    fechaAnulacion?: string;
}
