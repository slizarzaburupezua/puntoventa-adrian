using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class MedidaRepository : BaseRepository, IMedidaRepository
    {
        public MedidaRepository(VentasPlatformContext context) : base(context)
        {
        }

        public async Task<IList<Medida>?> SelectAllAsync()
        {
            return await All<Medida>().Select(m => new Medida()
            {
                ID = m.ID,
                NOMBRE = m.NOMBRE,
                DESCRIPCION = m.DESCRIPCION,
                ABREVIATURA = m.ABREVIATURA,
                EQUIVALENTE = m.EQUIVALENTE,
                VALOR = m.VALOR,
                ACTIVO = m.ACTIVO,
            }).AsNoTracking().ToListAsync();
        }
    }
}
