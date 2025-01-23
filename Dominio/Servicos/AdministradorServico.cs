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
}