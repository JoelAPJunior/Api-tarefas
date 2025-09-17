using aula2ApiServico.Models;
using Microsoft.EntityFrameworkCore;

namespace aula2ApiServico.DataContexts
{

    public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Chamado> Chamados { get; set; }
}
}
