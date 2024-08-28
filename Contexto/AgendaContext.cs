using Microsoft.EntityFrameworkCore;
using MVC_DIO.Models;

namespace MVC_DIO.Contexto;


public class AgendaContext : DbContext
{
    public AgendaContext(DbContextOptions<AgendaContext>options) : base(options)
    {
        
    }

    public DbSet<Contato> Contatos { get; set; }
}