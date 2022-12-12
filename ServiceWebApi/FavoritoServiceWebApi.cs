using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceWebApi
{
    public class FavoritoServiceWebApi
    {
        private WebApiAccess _webApiAccess;
        public FavoritoServiceWebApi(WebApiAccess webApiAccess)
        {
            _webApiAccess = webApiAccess;
        }

        public async Task<List<Favorito>> GetAll()
        {
            try
            {
                WebApiGet<List<Model.Favorito>> webApiGet = new WebApiGet<List<Model.Favorito>>(_webApiAccess);
                List<Favorito> lista = await webApiGet.GetAsync($"api/Favorito/GetAll");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ServiceWebApi.DTO.EstacionamientoDTO>> GetMisFavoritos()
        {
            try
            {
                WebApiGet<List<ServiceWebApi.DTO.EstacionamientoDTO>> webApiGet = new WebApiGet<List<ServiceWebApi.DTO.EstacionamientoDTO>>(_webApiAccess);
                List<ServiceWebApi.DTO.EstacionamientoDTO> lista = await webApiGet.GetAsync($"api/Favorito/GetMisFavoritos");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Add(int estaionamientoId)
        {
            try
            {
                WebApiPost<int> webApiPost = new WebApiPost<int>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Favorito/Add/", estaionamientoId);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int estaionamientoId)
        {
            try
            {
                WebApiDetele webApiDetele = new WebApiDetele(_webApiAccess);
                await webApiDetele.DeleteAsync($"/api/Favorito/Delete/{estaionamientoId}");
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
