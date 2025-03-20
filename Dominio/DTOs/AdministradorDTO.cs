namespace MinimalAPI.Dominio.DTOs;

public class AdministradorDTO
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
    public string Perfil { get; set; } = default!;
}