using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TakeANumberApi.Extensions;
public static class ModelStateExtension
{
    public static List<string> GetErrros (this ModelStateDictionary modelState)
    {
        List<string> result = new();
        foreach (var erro in modelState.Values)
            result.AddRange(erro.Errors.Select(erro => erro.ErrorMessage));
        return result;
    }
}
