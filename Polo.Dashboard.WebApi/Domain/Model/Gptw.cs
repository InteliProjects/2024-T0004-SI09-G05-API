using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polo.Dashboard.WebApi.Domain.Model
{
    [Table("gptw")]
    public class Gptw
    {
        [Key]
        public int? nniveis { get; private set; }
        public string? uo_abrev { get; private set; }
        public int npess { get; private set; }
        public string? rrh { get; private set; }
        public string? local { get; private set; }
        public int? empr { get; private set; }
        public int? gremp { get; private set; }
        public string? grpempregados { get; private set; }
        public string? sgemp { get; private set; }
        public int? centrocst { get; private set; }
        public int? unidorg { get; private set; }
        public string? descr_uo { get; private set; }
        public int? idadedoempregado { get; private set; }
        public int? gn { get; private set; }
        public string? tpausp { get; private set; }
        public string? txttipopresenaausn { get; private set; }
        public int? cargo { get; private set; }
        public string? data_adm { get; private set; }

        public Gptw(int? nniveis, string? uo_abrev,
                    int npess, string? rrh, string? local, int? empr, int? gremp, string? grpempregados, string? sgemp, int? centrocst, int? unidorg, string? descr_uo,
                    int? idadedoempregado, int? gn, string? tpausp, string? txttipopresenaausn, int? cargo,
                    string? data_adm)
        {
            this.nniveis = nniveis;
            this.uo_abrev = uo_abrev;
            this.npess = npess;
            this.rrh = rrh;
            this.local = local;
            this.empr = empr;
            this.gremp = gremp;
            this.grpempregados = grpempregados;
            this.sgemp = sgemp;
            this.centrocst = centrocst;
            this.unidorg = unidorg;
            this.descr_uo = descr_uo;
            this.idadedoempregado = idadedoempregado;
            this.gn = gn;
            this.tpausp = tpausp;
            this.txttipopresenaausn = txttipopresenaausn;
            this.cargo = cargo;
            this.data_adm = data_adm;
        }
    }
}
