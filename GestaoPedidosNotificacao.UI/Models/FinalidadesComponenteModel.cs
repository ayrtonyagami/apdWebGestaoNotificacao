using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class FinalidadesProgressoModel
    {
        public string Nome { get; set; }
        public decimal Prec { get; set; }
        public string Css { get; set; }

        public void FillStyle()
        {

            if (Prec >= 80)
                this.Css = "success";
            else if (Prec >= 60)
                this.Css = "primary";
            else if (Prec >= 40)
                this.Css = "info";
            else if (Prec >= 20)
                this.Css = "warning";
            else
                this.Css = "danger";
        }

        public void Calcular(decimal total)
        {
            this.Prec = (this.Prec * 100) / total;
        }
    }

    public class ListFinalidadesProgressoModel
    {
        public List<FinalidadesProgressoModel> Finalidades { get; set; }

        public void CalcularPercentange()
        {

            decimal total = this.Finalidades.Sum(x => x.Prec);

            this.Finalidades.ForEach(x =>
            {
                //x.Prec = (x.Prec * 100) / total;
                x.Calcular(total);
                x.FillStyle();
            });

        }

    }
}