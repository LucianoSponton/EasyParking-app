using Model;
using ServiceWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceWebApi
{
    public class EstacionamientoServiceWebApi
    {
        private WebApiAccess _webApiAccess;
        public EstacionamientoServiceWebApi(WebApiAccess webApiAccess)
        {
            _webApiAccess = webApiAccess;
        }

        public async Task<List<Estacionamiento>> GetAll()
        {
            try
            {
                WebApiGet<List<Model.Estacionamiento>> webApiGet = new WebApiGet<List<Model.Estacionamiento>>(_webApiAccess);
                List<Estacionamiento> lista = await webApiGet.GetAsync($"api/Estacionamiento/GetAll");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<EstacionamientoDTO>> GetAllInclude()
        {
            try
            {
                WebApiGet<List<EstacionamientoDTO>> webApiGet = new WebApiGet<List<EstacionamientoDTO>>(_webApiAccess);
                List<EstacionamientoDTO> lista = await webApiGet.GetAsync($"api/Estacionamiento/GetAllInclude");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Estacionamiento>> GetOrderByReservas(string text)
        {
            try
            {
                WebApiGet<List<Estacionamiento>> webApiGet = new WebApiGet<List<Estacionamiento>>(_webApiAccess);
                List<Estacionamiento> lista = await webApiGet.GetAsync($"api/Estacionamiento/GetOrderByReservas");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Estacionamiento>> GetConsultaSimple(string text)
        {
            try
            {
                WebApiGet<List<Estacionamiento>> webApiGet = new WebApiGet<List<Estacionamiento>>(_webApiAccess);
                List<Estacionamiento> lista = await webApiGet.GetAsync($"api/Estacionamiento/GetConsultaSimple/{text}");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Jornada>> GetJornadas(int estacionamientoId)
        {
            try
            {
                WebApiGet<List<Model.Jornada>> webApiGet = new WebApiGet<List<Model.Jornada>>(_webApiAccess);
                List<Jornada> lista = await webApiGet.GetAsync($"api/Estacionamiento/GetJornadas/{estacionamientoId}");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Tag>> GetTags(string text)
        {
            try
            {
                WebApiGet<List<Tag>> webApiGet = new WebApiGet<List<Tag>>(_webApiAccess);
                List<Tag> lista = await webApiGet.GetAsync($"api/Estacionamiento/GetTags/{text}");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Estacionamiento>> GetMisEstacionamientos()
        {
            try
            {
                WebApiGet<List<Estacionamiento>> webApiGet = new WebApiGet<List<Estacionamiento>>(_webApiAccess);
                List<Estacionamiento> lista = await webApiGet.GetAsync($"api/estacionamiento/GetMisEstacionamientos");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Estacionamiento> Get(int estacionamientoId)
        {
            try
            {
                WebApiGet<Estacionamiento> webApiGet = new WebApiGet<Estacionamiento>(_webApiAccess);
                Estacionamiento estacionamiento = await webApiGet.GetAsync($"/api/Estacionamiento/Get/{estacionamientoId}");
                return estacionamiento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Estacionamiento>> GetByTiposDeLugar(string tipoDeLugar)
        {
            try
            {
                WebApiGet<List<Estacionamiento>> webApiGet = new WebApiGet<List<Estacionamiento>>(_webApiAccess);
                List<Estacionamiento> lista = await webApiGet.GetAsync($"/api/Estacionamiento/GetByTiposDeLugar/{tipoDeLugar}");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Estacionamiento>> GetByTiposDeVehiculosAdmitidos(string tipoDeLugar)
        {
            try
            {
                WebApiGet<List<Estacionamiento>> webApiGet = new WebApiGet<List<Estacionamiento>>(_webApiAccess);
                List<Estacionamiento> lista = await webApiGet.GetAsync($"/api/Estacionamiento/GetByTiposDeVehiculosAdmitidos/{tipoDeLugar}");
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Estacionamiento>> GetConsultaGenerica(DTO.ParametroBusquedaDTO parametroBusquedaDTO)
        {
            try
            {
                WebApiPost<DTO.ParametroBusquedaDTO, List<Estacionamiento>> webApiPost = new WebApiPost<DTO.ParametroBusquedaDTO, List<Estacionamiento>>(_webApiAccess);

                return await webApiPost.PostAsync($"/api/Estacionamiento/GetConsultaGenerica", parametroBusquedaDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetInactivo(int estacionamientoId)
        {
            try
            {
                WebApiPost<int> webApiPost = new WebApiPost<int>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Estacionamiento/SetInactivo", estacionamientoId);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetActivo(int estacionamientoId)
        {
            try
            {
                WebApiPost<int> webApiPost = new WebApiPost<int>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Estacionamiento/SetActivo", estacionamientoId);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetPublicacionPausada(int estacionamientoId)
        {
            try
            {
                WebApiPost<int> webApiPost = new WebApiPost<int>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Estacionamiento/SetPublicacionPausada", estacionamientoId);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetReanudarPublicacion(int estacionamientoId)
        {
            try
            {
                WebApiPost<int> webApiPost = new WebApiPost<int>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Estacionamiento/SetReanudarPublicacion", estacionamientoId);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task Add(Estacionamiento estacionamiento)
        {
            try
            {
                WebApiPost<Estacionamiento> webApiPost = new WebApiPost<Estacionamiento>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Estacionamiento/Add", estacionamiento);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SedImage(byte[] image)
        {
            try
            {
                var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                content.Add(new StreamContent(new MemoryStream(image)), "bilddatei", "upload.jpg");
                WebApiPost<MultipartFormDataContent> webApiPost = new WebApiPost<MultipartFormDataContent>(_webApiAccess);
                await webApiPost.PostAsync(content);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> AddAutonumeric(Estacionamiento estacionamiento)
        {
            try
            {
                WebApiPost<Estacionamiento, string> webApiPost = new WebApiPost<Estacionamiento, string>(_webApiAccess);
                string codigo = await webApiPost.PostAsync($"/api/Estacionamiento/AddAutonumeric", estacionamiento);
                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Estacionamiento estacionamiento)
        {
            try
            {
                WebApiPost<Estacionamiento> webApiPost = new WebApiPost<Estacionamiento>(_webApiAccess);
                await webApiPost.PostAsync($"/api/Estacionamiento/Update", estacionamiento);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Delete(int estacionamientoId)
        {
            try
            {
                WebApiDetele webApiDetele = new WebApiDetele(_webApiAccess);
                await webApiDetele.DeleteAsync($"/api/Estacionamiento/Delete/{estacionamientoId}");
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
