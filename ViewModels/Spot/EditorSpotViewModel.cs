using System.ComponentModel.DataAnnotations;

namespace TakeANumberApi.ViewModels;
public class EditorSpotViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength =3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
    public string Name { get; set; }
    public bool Enabled { get; set; }

}
