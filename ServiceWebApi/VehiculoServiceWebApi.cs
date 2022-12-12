using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceWebApi
{
    public class VehiculoServiceWebApi
    {
        private WebApiAccess _webApiAccess;
        public VehiculoServiceWebApi(WebApiAccess webApiAccess)
        {
            _webApiAccess = webApiAccess;
        }

        public async Task<List<Vehiculo>> GetAll()
        {
            try
            {
                WebApiGet<List<Model.Vehiculo>> webApiGet = new WebApiGet<List<Model.Vehiculo>>(_webApiAccess);
                List<Vehiculo> lista = await webApiGet.GetAsync($"api/Vehiculo/GetAll");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Vehiculo>> GetMisVehiculos()
        {
            try
            {
                WebApiGet<List<Vehiculo>> webApiGet = new WebApiGet<List<Vehiculo>>(_webApiAccess);
                List<Vehiculo> lista = await webApiGet.GetAsync($"api/Vehiculo/GetMisVehiculos");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Vehiculo> GetFirst()
        {
            try
            {
                WebApiGet<Vehiculo> webApiGet = new WebApiGet<Vehiculo>(_webApiAccess);
                Vehiculo vehiculo = await webApiGet.GetAsync($"api/Vehiculo/GetFirst");
                return vehiculo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Add(Vehiculo vehiculo)
        {
            try
            {
                WebApiPost<Vehiculo> webApiPost = new WebApiPost<Vehiculo>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Vehiculo/Add/", vehiculo);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int vehiculoId)
        {
            try
            {
                WebApiDetele webApiDetele = new WebApiDetele(_webApiAccess);
                await webApiDetele.DeleteAsync($"/api/Vehiculo/Delete/{vehiculoId}");
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
