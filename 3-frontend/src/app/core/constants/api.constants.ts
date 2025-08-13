export const Auth = {
    ExistEmailAsync: '/api/Auth/existEmailAsync',
    IniciaSesionAsync: '/api/Auth/iniciaSesionAsync',
    VerifyOTPEmailAsync: '/api/Auth/verifyOTPEmailAsync',
    GenerateOTPEmailAsync: '/api/Auth/generateOTPEmailAsync',
    NotifyOlvideContraseniaAsync: '/api/Auth/notifyOlvideContraseniaAsync',
    RestablecerContraseniaAsync: '/api/Auth/restablecerContraseniaAsync',
    VerifyTokenRestablecerContraseniaAsync: '/api/Auth/verifyTokenRestablecerContraseniaAsync',
    GetParametersSignUpAsync: '/api/Auth/parametersSignUpAsync',
};

export const Cliente = {
    GetAllByFilterAsync: '/api/Cliente/allByFilterAsync',
    ExistCorreoAsync: '/api/Cliente/existCorreoAsync',
    ExistNumeroDocumentoAsync: '/api/Cliente/existNumeroDocumentoAsync',
    GetByNumDocumentoCorreoAsync: '/api/Cliente/byNumDocumentoCorreoAsync',
    InsertAsync: '/api/Cliente/insertAsync',
    UpdateAsync: '/api/Cliente/updateAsync',
    UpdateActivoAsync: '/api/Cliente/updateActivoAsync',
    DeleteAsync: '/api/Cliente/deleteAsync',
}

export const Inventario = {
    Medida: {
        GetAllMedidaAsync: '/api/Inventario/allMedidaAsync',
    },
    Producto: {
        GetProductoByCodeAsync: '/api/Inventario/productosByCodeAsync',
        GetAllProductoByFilterAsync: '/api/Inventario/allProductoByFilterAsync',
        GetCategoriesWithProductsCountAsync: '/api/Inventario/categoriesWithProductsCountAsync',
        GetAllProductsByCategoryAsync: '/api/Inventario/allProductsByCategoryAsync',
        GetAllProductoForComboBoxAsync: '/api/Inventario/allProductosForComboBoxAsync',
        InsertProductoAsync: '/api/Inventario/insertProductoAsync',
        UpdateProductoAsync: '/api/Inventario/updateProductoAsync',
        UpdateActivoProductoAsync: '/api/Inventario/updateActivoProductoAsync',
        DeleteProductoAsync: '/api/Inventario/deleteProductoAsync'
    },
    Categoria: {
        GetAllCategoriaByFilterAsync: '/api/Inventario/allCategoriaByFilterAsync',
        GetAllCategoriasForComboBoxAsync: '/api/Inventario/allCategoriasForComboBoxAsync',
        InsertCategoriaAsync: '/api/Inventario/insertCategoriaAsync',
        UpdateCategoriaAsync: '/api/Inventario/updateCategoriaAsync',
        DeleteCategoriaAsync: '/api/Inventario/deleteCategoriaAsync',
        UpdateActivoCategoriaAsync: '/api/Inventario/updateActivoCategoriaAsync',
    },
    Marca: {
        GetAllMarcaByFilterAsync: '/api/Inventario/allMarcaByFilterAsync',
        GetAllMarcasForComboBoxAsync: '/api/Inventario/allMarcasForComboBoxAsync',
        InsertMarcaAsync: '/api/Inventario/insertMarcaAsync',
        UpdateMarcaAsync: '/api/Inventario/updateMarcaAsync',
        UpdateActivoMarcaAsync: '/api/Inventario/updateActivoMarcaAsync',
        DeleteMarcaAsync: '/api/Inventario/deleteMarcaAsync',
    },
};

export const Venta = {
    GetAllByFilterAsync: '/api/Venta/allByFilterAsync',
    GetAllUsuariosAsync: '/api/Venta/allUsuariosAsync',
    InsertAsync: '/api/Venta/insertAsync',
    AnulaAsync: '/api/Venta/anulaAsync',
};

export const DetalleVenta = {
    GetDetalleAsync: '/api/DetalleVenta/detalleAsync',
    GetAnalisisProductosByFilterAsync: '/api/DetalleVenta/analisisProductosByFilterAsync',
    GetAnalisisCategoriasByFilterAsync: '/api/DetalleVenta/analisisCategoriasByFilterAsync',
    GetAnalisisMarcasByFilterAsync: '/api/DetalleVenta/analisisMarcasByFilterAsync',
    GetResumenReporteAsync: '/api/DetalleVenta/resumenReporteAsync',
    GetReportePorProductosAsync: '/api/DetalleVenta/reportePorProductosAsync',
    GetReportePorCategoriasAsync: '/api/DetalleVenta/reportePorCategoriasAsync',
    GetReportePorMarcasAsync: '/api/DetalleVenta/reportePorMarcasAsync',
};

export const Parametro = {
    GetValueBySubKeyAsync: '/api/Parametro/valueBySubKeyAsync',
    GetAllAsync: '/api/Parametro/allAsync',
    GetAllDetalleByIdAsync: '/api/Parametro/allDetalleByIdAsync',
    GetAllDetalleByKeyAsync: '/api/Parametro/allDetalleByKeyAsync',
    UpdateDetalleParametroAsync: '/api/Parametro/updateDetalleParametroAsync'
};

export const ParametrosGenerales = {
    GetAllRolAsync: '/api/ParametrosGenerales/allRolAsync',
    GetAllMetodoPagoAsync: '/api/ParametrosGenerales/allMetodoPagoAsync',
    GetAllMonedaAsync: '/api/ParametrosGenerales/allMonedaAsync',
    GetAllTipoDocumentoAsync: '/api/ParametrosGenerales/allTipoDocumentoAsync',
    UpdateNegocioAsync: '/api/ParametrosGenerales/updateNegocioAsync',
    GetAllGeneroAsync: '/api/ParametrosGenerales/allGeneroAsync',
    GetNegocioAsync: '/api/ParametrosGenerales/negocioAsync',
    VistaPreviaBoletaFacturaAsync: '/api/ParametrosGenerales/vistaPreviaBoletaFacturaAsync',
};

export const Tool = {
    GetExchangeRateAsync: '/api/Tool/exchangeRateAsync'
}

export const Usuario = {
    GetAllByFilterAsync: '/api/Usuario/allByFilterAsync',
    ExistCorreoAsync: '/api/Usuario/existCorreoAsync',
    ExistNumeroDocumentoAsync: '/api/Usuario/existNumeroDocumentoAsync',
    GetAllActivesAsync: '/api/Usuario/allActivesAsync',
    InsertAsync: '/api/Usuario/insertAsync',
    UpdateAsync: '/api/Usuario/updateAsync',
    UpdateActivoAsync: '/api/Usuario/updateActivoAsync',
    DeleteAsync: '/api/Usuario/deleteAsync',
    EnviarEnlacePagoAsync: '/api/Usuario/enviarEnlacePagoAsync',
    UpdateUsuarioContraseniaByIdAsync: '/api/Usuario/updateUsuarioContraseniaByIdAsync',
    UpdateNoEsPrimerLogueoAsync: '/api/Usuario/updateNoEsPrimerLogueoAsync',
    GetPersonalInfoAsync: '/api/Usuario/personalInfoAsync',
}
