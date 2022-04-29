using Microsoft.AspNetCore.Mvc;
using ControleCar.Services;
using ControleCar.Models;
using System.Threading.Tasks;
using ControleCar.Services.Util;
using Microsoft.AspNetCore.Http;
namespace ControleCar.Services.Util
{
    public static class ValidaSessao
    {
        public static bool Validar(HttpContext httpContext)
        {
            if (httpContext.Session.GetInt32("USR_ID") == null)
            {
                return false;
            }
            if (httpContext.Session.GetInt32("USR_ATIVO") != 1)
            {
                return false;
            }
            return true;

        }


    }
}
