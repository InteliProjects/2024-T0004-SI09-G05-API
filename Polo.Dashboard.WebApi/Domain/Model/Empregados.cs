using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polo.Dashboard.WebApi.Domain.Model
{
    [Table("empregados")]
    public class Empregados
    {
        [Key]
        public int n_pessoal { get; private set; }
        public string sg_emp { get; private set; }
        public string texto_rh { get; private set; }
        public int centro_cst { get; private set; }
        public string centro_custo { get; private set; }
        public string cargo { get; private set; }
        public string data_nascimento { get; private set; }

        public Empregados(int n_pessoal, string sg_emp, string texto_rh, int centro_cst, string centro_custo,
            string cargo, string data_nascimento)
        {
            this.n_pessoal = n_pessoal;
            this.sg_emp = sg_emp;
            this.texto_rh = texto_rh;
            this.centro_cst = centro_cst;
            this.centro_custo = centro_custo;
            this.cargo = cargo;
            this.data_nascimento = data_nascimento;
        }

    }
}
