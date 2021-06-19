using BD;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace WBL
{
    public interface IMarcaVehiculoService
    {
        Task<DbEntity> Create(MarcaVehiculoEntity entity);
        Task<DbEntity> Delete(MarcaVehiculoEntity entity);
        Task<IEnumerable<MarcaVehiculoEntity>> Get();
        Task<MarcaVehiculoEntity> GetById(MarcaVehiculoEntity entity);
        Task<DbEntity> Update(MarcaVehiculoEntity entity);
    }

    public class MarcaVehiculoService : IMarcaVehiculoService
    {
        private readonly IDataAccess sql;

        public MarcaVehiculoService(IDataAccess _sql)
        {
            sql = _sql;
        }

        public async Task<IEnumerable<MarcaVehiculoEntity>> Get()
        {
            try
            {
                var result = sql.QueryAsync<MarcaVehiculoEntity>("MarcaVehiculoObtener");

                return await result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<MarcaVehiculoEntity> GetById(MarcaVehiculoEntity entity)
        {
            try
            {
                var result = sql.QueryFirstAsync<MarcaVehiculoEntity>("MarcaVehiculoObtener", new
                {
                    entity.MarcaVehiculoId
                }
                );

                return await result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DbEntity> Create(MarcaVehiculoEntity entity)
        {
            try
            {
                var result = sql.ExecuteAsync("MarcaVehiculoInsertar", new
                {
                    entity.Descripcion,
                    entity.Estado
                }
                );

                return await result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DbEntity> Update(MarcaVehiculoEntity entity)
        {
            try
            {
                var result = sql.ExecuteAsync("MarcaVehiculoActualizar", new
                {
                    entity.MarcaVehiculoId,
                    entity.Descripcion,
                    entity.Estado
                });

                return await result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DbEntity> Delete(MarcaVehiculoEntity entity)
        {
            try
            {
                var result = sql.ExecuteAsync("MarcaVehiculoEliminar", new
                {
                    entity.MarcaVehiculoId
                });
                return await result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
