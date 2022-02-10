using GestaoPedidosNotificacao.UI.Entities;
using GestaoPedidosNotificacao.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;

namespace GestaoPedidosNotificacao.UI.Repositories
{
    public class EntidadesRepository
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        public List<Entidade> Buscar(SearchEntidadeModel search, bool all)
        {
            if (search == null || search.IsEmpty() || all)
            {
                var entidadesFull = db.Entidades.Include(e => e.EntidadeTipo);
                return (entidadesFull.OrderByDescending(x => x.Denoninacao).ToList());
            }

            var query = db.Entidades.Where(x => x.Id > 0);

            if (search.Nome.HasValue()) query = query.Where(x => x.Denoninacao.Contains(search.Nome));
            if (search.EntidadeTipoId.isID()) query = query.Where(x => x.EntidadeTipoId == search.EntidadeTipoId);
            if (search.NIF.HasValue()) query = query.Where(x => x.NIF == search.NIF);

            //Data de cadastro
            if (search.DataCadastroStart != null && search.DataCadastroEnd != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataEntidade.Value) >= DbFunctions.TruncateTime(search.DataCadastroStart.Value)
                && DbFunctions.TruncateTime(x.DataEntidade.Value) <= DbFunctions.TruncateTime(search.DataCadastroEnd.Value));
            else if (search.DataCadastroStart != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataEntidade.Value) == DbFunctions.TruncateTime(search.DataCadastroStart.Value));
            

            var pedidos = query.Include(p => p.EntidadeTipo);
            return (pedidos.OrderByDescending(x => x.Denoninacao).ToList());
        }


    }
}