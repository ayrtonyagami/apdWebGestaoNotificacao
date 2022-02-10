using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Models
{
    public class SearchEntidadeModel
    {
        public SearchEntidadeModel()
        {
        }

        public SearchEntidadeModel(object model)
        {
            if (model == null) return;
            SearchEntidadeModel search = (SearchEntidadeModel)model;

            this.DataCadastroEnd = search.DataCadastroEnd;
            this.DataCadastroStart = search.DataCadastroStart;
            this.EntidadeTipoId = search.EntidadeTipoId;
            this.NIF = search.NIF;
            this.Nome = search.Nome;
            this.Telefone = search.Telefone;
        }

        public SearchEntidadeModel(SearchEntidadeModel search)
        {
            if (search == null) return;
            this.DataCadastroEnd = search.DataCadastroEnd;
            this.DataCadastroStart = search.DataCadastroStart;
            this.EntidadeTipoId = search.EntidadeTipoId;
            this.NIF = search.NIF;
            this.Nome = search.Nome;
            this.Telefone = search.Telefone;
        }


        [Display(Name = "Entidade")]
        public string Nome { get; set; }

        [Display(Name = "Tipo")]
        public Nullable<int> EntidadeTipoId { get; set; }

        [Display(Name = "Data Inicio")]
        public Nullable<System.DateTime> DataCadastroStart { get; set; }

        [Display(Name = "Data Fim")]
        public Nullable<System.DateTime> DataCadastroEnd { get; set; }

        [Display(Name = "Telefones")]
        public string Telefone { get; set; }

        [Display(Name = "NIF")]
        public string NIF { get; set; }

        public bool IsEmpty()
        {
            if (Nome != null) return false;
            if (EntidadeTipoId.isID()) return false;
            if (DataCadastroStart != null) return false;
            if (Telefone != null) return false;
            if (NIF != null) return false;

            return true;
        }
    }
}