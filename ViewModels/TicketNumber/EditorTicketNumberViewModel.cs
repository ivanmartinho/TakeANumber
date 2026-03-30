using System.ComponentModel.DataAnnotations;
using TakeANumberApi.Enums;
using TakeANumberApi.Models;

namespace TakeANumberApi.ViewModels;
public class EditorTicketNumberViewModel
{
    public int Id { get; set; }
    public string Ticket { get; set; }
    [Required(ErrorMessage = "É obrigatório informar o grupo do ticket")]
    public TicketGroup TicketGroup { get; set; }
    [Required(ErrorMessage = "É obrigatório informar o local")]
    public Spot Spot { get; set; }
    [Required(ErrorMessage = "É obrigatório informar a empresa")]
    public Company Company { get; set; }
    [Required(ErrorMessage = "É obrigatório informar a prioridade")]
    public TicketType TicketType { get; set; }
    public bool Called { get; set; } = false;
    public bool Serviced { get; set; } = false;
    public DateTime GenerateDate { get; private set; }  = DateTime.UtcNow;
    public DateTime? CalledDate { get; set; }
    public DateTime? ServicedDate { get; set; }
}

