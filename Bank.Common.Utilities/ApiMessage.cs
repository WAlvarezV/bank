using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bank.Common.Utilities
{
    public static class ApiMessage
    {
        public static string ModelErrors(ModelStateDictionary ModelState, string ApiName)
        {
            var msj = string.Empty;
            foreach (var item in ModelState)
            {
                msj = $" * {item.Key}: ";
                foreach (var e in item.Value.Errors)
                {
                    msj += $"{e.ErrorMessage}";
                }
            }
            return $"{ApiName}: Error en Modelo =>{msj}";
        }
    }
}