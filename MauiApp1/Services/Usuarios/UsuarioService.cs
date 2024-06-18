using MauiApp1.Models;

namespace MauiApp1.Services.Usuarios
{
    public class UsuarioService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://rpgapi20241pam.azurewebsites.net/Usuarios";
        private static Usuario _usuarioAutenticado;

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
            _usuarioAutenticado = await _request.PostAsync(apiUrlBase + uriComplementar, usuario, string.Empty);
            return _usuarioAutenticado;
        }

        public Usuario GetUsuarioAutenticado()
        {
            return _usuarioAutenticado;
        }
    }
}
