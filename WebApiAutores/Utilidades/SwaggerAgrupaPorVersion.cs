using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebApiAutores.Utilidades
{
    public class SwaggerAgrupaPorVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            //controllers.v1
            var namespaceControlador = controller.ControllerType.Namespace;
            //v1
            var versionApi = namespaceControlador.Split(".").Last().ToLower();
            controller.ApiExplorer.GroupName = versionApi;
        }
    }
}
