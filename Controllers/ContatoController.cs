using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_DIO.Models;
using MVC_DIO.Contexto;  

namespace MVC_DIO.Controllers;

public class ContatoController : Controller
{
    private readonly AgendaContext _agendaContext;

     private readonly ILogger<ContatoController> _logger;

    public ContatoController(AgendaContext agendaContext, ILogger<ContatoController> logger)
    {
    _agendaContext = agendaContext;
    _logger = logger;
    }

    public IActionResult Index()
    {
        var contatos = _agendaContext.Contatos.ToList();
        return View(contatos);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Contato contato)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _agendaContext.Contatos.Add(contato);
                _agendaContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar contato no banco de dados");
                ModelState.AddModelError("", "Ocorreu um erro ao salvar o contato. Tente novamente.");
            }
        }
        return View(contato);
    }   
    public IActionResult Editar(int id)
    {
        var contato = _agendaContext.Contatos.Find(id);
        if (contato == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(contato);
    }

    [HttpPost]
     public IActionResult Editar(Contato contato)
    {
        var contatoBanco = _agendaContext.Contatos.Find(contato.Id);
        contatoBanco.Nome = contato.Nome;
        contatoBanco.Telefone = contato.Telefone;
        contatoBanco.Ativo = contato.Ativo;

        _agendaContext.Contatos.Update(contatoBanco);
        _agendaContext.SaveChanges();

        return RedirectToAction(nameof(Index));

    }

    public IActionResult Detalhes(int id)
    {
        var contato = _agendaContext.Contatos.Find(id);
        if(contato == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(contato);
    }

    public IActionResult Deletar(int id)
    {
        var contato = _agendaContext.Contatos.Find(id);
        if(contato == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(contato);
    }


    [HttpPost]
    public IActionResult Deletar(Contato contato)
    {
        var contatoBanco = _agendaContext.Contatos.Find(contato.Id);
        if(contatoBanco == null)
        {
            return RedirectToAction(nameof(Index));
        }
       
        _agendaContext.Contatos.Remove(contatoBanco);
        _agendaContext.SaveChanges();

        return RedirectToAction(nameof(Index));

    }

}
