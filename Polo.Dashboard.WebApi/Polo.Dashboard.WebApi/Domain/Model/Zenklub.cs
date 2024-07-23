using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polo.Dashboard.WebApi.Domain.Model
{
    [Table("zenklub")]
    public class Zenklub
    {
        [Key]
        public int n_pessoal { get; private set; }
        public string periodo { get; private set; }
        public int mes { get; private set; }
        public long codigo_validacao { get; private set; }
        public string? departamento { get; private set; }
        public int total_sessoes { get; private set; }

        public Zenklub(int n_pessoal, string periodo, int mes, long codigo_validacao, string departamento,
            int total_sessoes)
        {
            this.n_pessoal = n_pessoal;
            this.periodo = periodo;
            this.mes = mes;
            this.codigo_validacao = codigo_validacao;
            this.departamento = departamento;
            this.total_sessoes = total_sessoes;
        }

    }
}
