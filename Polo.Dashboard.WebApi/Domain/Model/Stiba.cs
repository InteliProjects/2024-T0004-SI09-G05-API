using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polo.Dashboard.WebApi.Domain.Model
{
    [Table("stiba_2023")]
    public class Stiba
    {
        [Key]
        [Column("Descrição UO")]
        public string? descricaoUO { get; private set; }

        [Column("Elegíveis")]
        public double? elegiveis { get; private set; } 

        [Column("% particip")]
        public int? particip { get; private set; }

        [Column("Nota Stiba")]
        public string? notaStiba { get; private set; }

        public Stiba(string? descricaoUO, double? elegiveis, int? particip, string? notaStiba) 
        {
            this.descricaoUO = descricaoUO;
            this.elegiveis = elegiveis;
            this.particip = particip;
            this.notaStiba = notaStiba;
        }
    }
}
