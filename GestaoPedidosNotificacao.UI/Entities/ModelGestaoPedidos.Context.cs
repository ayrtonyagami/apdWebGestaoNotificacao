//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestaoPedidosNotificacao.UI.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GestaoPedidosNotificacaoDBEntities : DbContext
    {
        public GestaoPedidosNotificacaoDBEntities()
            : base("name=GestaoPedidosNotificacaoDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EntidadeTipo> EntidadeTipoes { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<PedidosHistorico> PedidosHistoricoes { get; set; }
        public virtual DbSet<ServicoProduto> ServicoProdutoes { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Utilizador> Utilizadors { get; set; }
        public virtual DbSet<Entidade> Entidades { get; set; }
    }
}
