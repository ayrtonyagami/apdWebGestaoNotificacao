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
    public class PedidosRepository
    {
        private GestaoPedidosNotificacaoDBEntities db = new GestaoPedidosNotificacaoDBEntities();

        public List<Pedido> Buscar(SearchPedidosModel search, bool all)
        {
            if (search == null || search.IsEmpty() || all)
            {
                var pedidosFull = db.Pedidos.Include(p => p.Entidade).Include(p => p.Status).Include(p => p.Utilizador);
                return (pedidosFull.OrderByDescending(x => x.DataFactura).ToList());
            }

            var query = db.Pedidos.Where(x => x.EntidadeId != null);

            if (search.EntidadeNome.HasValue()) query = query.Where(x => x.Entidade.Denoninacao.Contains(search.EntidadeNome));
            if (search.EstadoId.isID()) query = query.Where(x => x.EstadoId == search.EstadoId);
            if (search.NumProcesso.HasValue()) query = query.Where(x => x.NumProcesso == search.NumProcesso);

            //Data de facturação
            if (search.DataFacStart != null && search.DataFacEnd != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataFactura.Value) >= DbFunctions.TruncateTime(search.DataFacStart.Value)
                && DbFunctions.TruncateTime(x.DataFactura.Value) <= DbFunctions.TruncateTime(search.DataFacEnd.Value));
            else if (search.DataFacStart != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataFactura.Value) == DbFunctions.TruncateTime(search.DataFacStart.Value));

            //Data de pagamento
            if (search.DataPagStart != null && search.DataPagEnd != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataPagamento.Value) >= DbFunctions.TruncateTime(search.DataPagStart.Value)
                && DbFunctions.TruncateTime(x.DataPagamento.Value) <= DbFunctions.TruncateTime(search.DataPagEnd.Value));
            else if (search.DataPagStart != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.DataFactura.Value) == DbFunctions.TruncateTime(search.DataPagStart.Value));



            var pedidos = query.Include(p => p.Entidade).Include(p => p.Status).Include(p => p.Utilizador);
            return (pedidos.OrderByDescending(x => x.DataFactura).ToList());
        }


    }
}