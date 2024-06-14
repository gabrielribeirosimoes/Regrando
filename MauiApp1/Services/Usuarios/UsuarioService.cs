using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.Usuarios
{
    public class UsuarioService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://rpgapi20241pam.azurewebsites.net/Usuarios";

        public UsuarioService()
        {
            _request = new Request();
        }
        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario usuario)
        {
            string uriComplementar = "/Registrar";
            usuario.Id = await _request.PostReturnIntAsync(apiUrlBase + uriComplementar, usuario, string.Empty);
            return usuario;
        }
        public async Task<TipoMetas> PostRegistrarTipoMetasAsync(TipoMetas tipoMetas)
        {
            string uriComplementar = "/Registrar";
            tipoMetas.Id = await _request.PostReturnIntAsync(apiUrlBase + uriComplementar, tipoMetas, string.Empty);
            return tipoMetas;
        }
        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario usuario)
        {
            string uriComplementar = "/Autenticar";
            usuario = await _request.PostAsync(apiUrlBase + uriComplementar, usuario, string.Empty);
            return usuario;
        }
    }
}
