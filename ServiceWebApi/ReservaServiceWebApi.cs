using Model;
using ServiceWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ServiceWebApi
{
    public class ReservaServiceWebApi
    {
        private WebApiAccess _webApiAccess;
        public ReservaServiceWebApi(WebApiAccess webApiAccess)
        {
            _webApiAccess = webApiAccess;
        }

        public async Task<List<Reserva>> GetAll()
        {
            try
            {
                WebApiGet<List<Model.Reserva>> webApiGet = new WebApiGet<List<Model.Reserva>>(_webApiAccess);
                List<Reserva> lista = await webApiGet.GetAsync($"api/Reserva/GetAll");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ReservaDTO>> GetMisReservas()
        {
            try
            {
                WebApiGet<List<ReservaDTO>> webApiGet = new WebApiGet<List<ReservaDTO>>(_webApiAccess);
                List<ReservaDTO> lista = await webApiGet.GetAsync($"api/Reserva/GetMisReservas");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ReservaDTO>> GetReservasModalidadDueño()
        {
            try
            {
                WebApiGet<List<ReservaDTO>> webApiGet = new WebApiGet<List<ReservaDTO>>(_webApiAccess);
                List<ReservaDTO> lista = await webApiGet.GetAsync($"api/Reserva/GetReservasModalidadDueño");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<UserInfo>> GetReservasModalidadDueño2()
        {
            try
            {
                WebApiGet<List<UserInfo>> webApiGet = new WebApiGet<List<UserInfo>>(_webApiAccess);
                List<UserInfo> lista = await webApiGet.GetAsync($"api/Reserva2/GetReservasModalidadDueño");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Add(Reserva reserva)
        {
            try
            {
                WebApiPost<Reserva> webApiPost = new WebApiPost<Reserva>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Reserva/Add/", reserva);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int reservaId)
        {
            try
            {
                WebApiDetele webApiDetele = new WebApiDetele(_webApiAccess);
                await webApiDetele.DeleteAsync($"/api/Reserva/Delete/{reservaId}");
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
