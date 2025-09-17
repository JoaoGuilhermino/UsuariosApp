using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Dtos.Requests;
using UsuariosApp.Domain.Dtos.Responses;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Security;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Validators;

namespace UsuariosApp.Domain.Services
{
    /// <summary>
    /// Classe para implementar os serviços de domínio de usuário
    /// </summary>
    public class UsuarioService(
                IUsuarioRepository usuarioRepository,   //injeção de dependência
                IPerfilRepository perfilRepository,     //injeção de dependência
                IJwtBearerSecurity jwtBearerSecurity    //injeção de dependência
            )
        : IUsuarioService
    {
        public CriarUsuarioResponse CriarUsuario(CriarUsuarioRequest request)
        {
            //capturando os dados do usuário (request)
            var usuario = new Usuario
            {
                Nome = request.Nome ?? string.Empty,
                Email = request.Email ?? string.Empty,
                Senha = request.Senha ?? string.Empty
            };

            //Executar as regras de validação
            var usuarioValidator = new UsuarioValidator();
            var result = usuarioValidator.Validate(usuario);

            //Verificar se o usuário não passou nas regras de validação
            if (!result.IsValid)
            {
                //retornar os erros de validação através de uma exceção
                throw new ValidationException(result.Errors);
            }

            //Verificar se o email do usuário já está cadastrado no banco de dados
            if (usuarioRepository.VerifyEmailExists(usuario.Email))
            {
                //retornar um erro de email já existente.
                throw new ApplicationException("O email informado já está cadastrado para um usuário. Tente outro.");
            }

            //Criptografar a senha do usuário
            usuario.Senha = CryptoHelper.GetSHA256(usuario.Senha);

            //Associar o usuário a um perfil chamado "Operador".
            var perfil = perfilRepository.GetByNome("Operador");
            usuario.PerfilId = perfil.Id;

            //Salvar os dados do usuário no banco de dados
            usuarioRepository.Add(usuario);

            //Retornar os dados do usuário
            return new CriarUsuarioResponse(
                    Id: usuario.Id,
                    Nome: usuario.Nome,
                    Email: usuario.Email,
                    DataHoraCriacao: DateTime.Now,
                    Perfil: perfil.Nome
                );
        }

        public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            //Verificar se o email não está cadastrado no banco de dados
            if (!usuarioRepository.VerifyEmailExists(request.Email))
                throw new ApplicationException("Acesso negado. Email não encontrado.");

            //Buscar o usuário no banco através do email e da senha
            var usuario = usuarioRepository.GetByEmailAndSenha(request.Email, CryptoHelper.GetSHA256(request.Senha));

            //Verificar se o usuário não foi encontrado
            if (usuario == null)
                throw new ApplicationException("Acesso negado. Email e senha inválidos.");

            //Retornar os dados do usuário autenticado
            return new AutenticarUsuarioResponse(
                    Id: usuario.Id,
                    Nome: usuario.Nome,
                    Email: usuario.Email,
                    Perfil: usuario.Perfil.Nome,
                    DataHoraAcesso: DateTime.Now,
                    AccessToken: jwtBearerSecurity.GenerateToken(usuario.Email, usuario.Perfil.Nome)
                );
        }

        public ObterDadosUsuarioResponse ObterDadosUsuario(string email)
        {
            var usuario = usuarioRepository.GetByEmail(email);

            if (usuario == null)
                throw new ApplicationException("Usuário não encontrado.");

            return new ObterDadosUsuarioResponse(
                    Id: usuario.Id,
                    Nome: usuario.Nome,
                    Email: usuario.Email,
                    Perfil: usuario.Perfil?.Nome,
                    Permissoes: usuario.Perfil?.Permissoes?
                        .Select(p => new PermissaoResponse
                        (
                            Nome: p.Permissao.Nome,
                            Servico: p.Permissao.Servico,
                            Tipo: p.Permissao.Tipo
                        ))
                        .ToList() ?? new List<PermissaoResponse>()
                );
        }
    }
}



