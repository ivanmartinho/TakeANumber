using System.ComponentModel.DataAnnotations;

namespace TakeANumberApi.ViewModels;
public class EditorTicketGroupViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo deve ter entre 3 e 100 caracteres")]
    public string Name { get; set; }
    [Required(ErrorMessage = "A descrição simplificada deve ser informada")]
    [StringLength(3, ErrorMessage = "Este campo deve ter entre 1 e 3 caracteres")]
    public string Acronym { get; set; }
    public bool Enabled { get; set; }

}
