using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class ParametroDetalle : Entity
    {
        public int ID { get; set; }

        public int ID_PARAMETRO { get; set; }

        public string PARA_KEY { get; set; }

        public string SUB_PARA_KEY { get; set; }

        public string NOMBRE { get; set; }

        public string DESCRIPCION { get; set; }

        public string TIPOCAMPO { get; set; }

        public int? ORDEN { get; set; }

        public string? SVALOR1 { get; set; }

        public string? SVALOR2 { get; set; }

        public string? SVALOR3 { get; set; }

        public decimal? DVALOR1 { get; set; }

        public decimal? DVALOR2 { get; set; }

        public decimal? DVALOR3 { get; set; }

        public DateTime? FVALOR1 { get; set; }

        public DateTime? FVALOR2 { get; set; }

        public DateTime? FVALOR3 { get; set; }

        public bool? BVALOR1 { get; set; }

        public bool? BVALOR2 { get; set; }

        public bool? BVALOR3 { get; set; }

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }
    }
}
