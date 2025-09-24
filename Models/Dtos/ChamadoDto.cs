using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace aula2ApiServico.Models.Dtos
{
    public class ChamadoDto
    {
        
        internal string titulo;

        [Required(ErrorMessage = "O titulo e obrigatorio")]
        //[MinLength(10)]
        [Length(10, 100, ErrorMessage = "O titulo tem que ter no minimo 10 e no maximo 100 caracteres")]
        public required string Titulo { get; set; }
        [Required(ErrorMessage = "A descricao e obrigatoria")]
        public required string Descricao { get; set; }
    }
}
