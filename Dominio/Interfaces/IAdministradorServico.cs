using MinimalAPI.DTOs;
using MinimalAPI.Dominio.Entidades;

namespace MinimalAPI.Dominio.Interfaces;

public interface IAdministradorServico
{
    List<Administrador> Login(LoginDTO loginDTO);
}