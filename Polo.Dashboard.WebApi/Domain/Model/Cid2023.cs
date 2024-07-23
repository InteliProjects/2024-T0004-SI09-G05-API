using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polo.Dashboard.WebApi.Domain.Model
{
    [Table("cid_f_2023")]
    public class Cid2023
    {
        [Key] 
        public int n_pessoal { get; private set; }
        public string mes { get; private set; }
        public int atestados { get; private set; }
        public string dias { get; private set; }
        public string diretoria { get; private set; }
        public string unidade { get; private set; }
        public string cid { get; private set; }
        public string descricao { get; private set; }
        
        public Cid2023(int n_pessoal, string mes, int atestados, string dias, string diretoria, string unidade, string cid,
            string descricao)
        {
            this.n_pessoal = n_pessoal;
            this.mes = mes;
            this.atestados = atestados;
            this.dias = dias;
            this.diretoria = diretoria;
            this.unidade = unidade;
            this.cid = cid;
            this.descricao = descricao;
        }

    }
}
