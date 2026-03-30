using System.ComponentModel.DataAnnotations;

namespace TakeANumberShared.Enums
{
    public enum TicketType
    {
        [Display(Name = "Prioritário")]
        Priority = 1,
        [Display(Name = "Normal")]
        Regular = 2
    }
}
