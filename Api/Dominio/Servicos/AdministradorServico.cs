using MinimalAPI.Infraestrutura.Db;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Entidades;

namespace MinimalAPI.Dominio.Servico;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;
    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;
    }
    public Administrador? Login(LoginDTO loginDTO)
    {
        var adm = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Password).FirstOrDefault();
        return adm;
    }
    public Administrador Incluir(Administrador administrador)
    {
        _contexto.Administradores.Add(administrador);
        _contexto.SaveChanges();
        return administrador;
    }

    public List<Administrador> Todos(int? pagina)
    {
        var query =_contexto.Administradores.AsQueryable();
        
        int intensPorPagina = 10;

        if(pagina != null)
        {
            query = query.Skip(((int)pagina - 1) * intensPorPagina).Take(intensPorPagina);
        }
        return query.ToList();
    }

    public Administrador? BuscaPorId(int id)
    {
        return _contexto.Administradores.Where(a => a.Id == id).FirstOrDefault();
    }
}