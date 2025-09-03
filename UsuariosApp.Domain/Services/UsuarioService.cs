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
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Validators;

namespace UsuariosApp.Domain.Services
{
    /// <summary>
    /// Classe para implementar os serviços de domínio de usuário
    /// </summary>
    public class UsuarioService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository) : IUsuarioService
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

            //Associar o usuário a um perfil chamado"operador"
            var perfil = perfilRepository.GetByNome("Operador");
            usuario.PerfilId = perfil.Id;

            //Salvar o usuário no banco de dados
            usuarioRepository.Add(usuario);

            return new CriarUsuarioResponse(
                Id: usuario.Id,
                Nome: usuario.Nome,
                Email: usuario.Email,
                DataHoraCriacao: DateTime.Now,
                Perfil: perfil.Nome
                );
        }
    }
}



