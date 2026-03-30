using System.ComponentModel.DataAnnotations;

namespace TakeANumberApi.Enums
{
    public enum TicketType
    {
        [Display(Name = "Prioritário")]
        Priority = 1,
        [Display(Name = "Normal")]
        Regular = 2
    }
}
