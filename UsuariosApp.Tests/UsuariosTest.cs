using Azure;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Dtos.Requests;

namespace UsuariosApp.Tests
{
    /// <summary>
    /// Classe para desenvolvimento dos testes de integração
    /// dos endpoints de usuário da API REST
    /// </summary>
    public class UsuariosTest
    {
        //Atributos
        private readonly HttpClient _client;
        private readonly Faker _faker;

        //Método construtor
        public UsuariosTest()
        {
            //Inicializando o atributo HttpClient apontando para o projeto da API
            _client = new WebApplicationFactory<Program>().CreateClient();
            //Inicializando a biblioteca do Bogus
            _faker = new Faker("pt_BR");
        }

        [Fact(
            DisplayName = "Deve criar um usuário com sucesso."
        )]
        public void DeveCriarUsuarioComSucesso()
        {
            //Arrange
            //Criando os dados da requisição para cadastro de usuário
            var request = new CriarUsuarioRequest(
                    Nome: _faker.Person.FullName,
                    Email: _faker.Internet.Email(),
                    Senha: "@Teste2025"
                );

            //Act
            //Enviando os dados para o endpoint de cadastro de usuário
            var response = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Assert
            //Verificar se a API retorna HTTP 201
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact(
            DisplayName = "Deve retornar erros de validação ao criar usuário com campos inválidos."
        )]
        public void DeveRetornarErroDeValidacaoAoCriarUsuarioComCamposInvalidos()
        {
            //Arrange
            //Criando os dados da requisição para cadastro de usuário
            var request = new CriarUsuarioRequest(
                    Nome: string.Empty,     //Vazio
                    Email: string.Empty,    //Vazio
                    Senha: string.Empty     //Vazio
                );

            //Act
            //Enviando os dados para o endpoint de cadastro de usuário
            var response = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Asserts
            //Verificar se a API retorna HTTP 400
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            //Capturar o conteudo da resposta
            var json = response.Content.ReadAsStringAsync().Result;
            //Verificando se contem as mensagens de erro
            json.Should().Contain("O nome do usuário é obrigatório.");
            json.Should().Contain("O nome deve ter de 6 a 100 caracteres.");
            json.Should().Contain("O endereço de email do usuário é obrigatório.");
            json.Should().Contain("Informe um endereço de email válido.");
            json.Should().Contain("A senha do usuário é obrigatória.");
            json.Should().Contain("A senha deve ter pelo menos uma letra minúscula, uma letra maiúscula, um número, um símbolo e pelo menos 8 caracteres.");
        }

        [Fact(
            DisplayName = "Deve retornar erro ao tentar criar usuário com e-mail já cadastrado."
        )]
        public void DeveRetornarErroAoCriarUsuarioComEmailJaCadastrado()
        {
            //Arrange
            //Criando os dados da requisição para cadastro de usuário
            var request = new CriarUsuarioRequest(
                    Nome: _faker.Person.FullName,
                    Email: _faker.Internet.Email(),
                    Senha: "@Teste2025"
                );

            //Act
            //Enviando os dados para o endpoint de cadastro de usuário
            var response1 = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;
            //Enviando os dados para cadastrar o mesmo usuário mais de 1 vez
            var response2 = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Asserts
            //Verificar se a API retorna HTTP 201 para o primeiro cadastro
            response1.StatusCode.Should().Be(HttpStatusCode.Created);
            //Verificar se a API retorna HTTP 422 para o psegundo cadastro
            response2.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            //Capturar o conteudo da segunda resposta
            var json = response2.Content.ReadAsStringAsync().Result;
            //Verificando se contem as mensagens de erro
            json.Should().Contain("O email informado já está cadastrado para um usuário. Tente outro.");
        }

        [Fact(
            DisplayName = "Deve autenticar usuário com sucesso."
        )]
        public void DeveAutenticarUsuarioComSucesso()
        {
            //Arrange
            //Criando os dados da requisição para cadastro de usuário
            var requestCriarUsuario = new CriarUsuarioRequest(
                    Nome: _faker.Person.FullName,
                    Email: _faker.Internet.Email(),
                    Senha: "@Teste2025"
                );

            //Criando os dados da requisição para autenticação de usuário
            var requestAutenticarUsuario = new AutenticarUsuarioRequest(
                    Email: requestCriarUsuario.Email,
                    Senha: requestCriarUsuario.Senha
                );

            //Act
            //Enviando os dados para o endpoint de cadastro de usuário
            var response1 = _client.PostAsJsonAsync("/api/usuario/criar", requestCriarUsuario).Result;
            //Enviando os dados para o endpoint de autenticação de usuário
            var response2 = _client.PostAsJsonAsync("/api/usuario/autenticar", requestAutenticarUsuario).Result;

            //Asserts
            //Verificar se a API retorna HTTP 201 para o primeiro cadastro
            response1.StatusCode.Should().Be(HttpStatusCode.Created);
            //Verificar se a API retorna HTTP 200 para o psegundo cadastro
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(
            DisplayName = "Deve retornar acesso negado ao tentar autenticar usuário inválido."
        )]
        public void DeveRetornarAcessoNegadoAoAutenticarUsuarioInvalido()
        {
            //Arrange           
            //Criando os dados da requisição para autenticação de usuário
            var request = new AutenticarUsuarioRequest(
                    Email: _faker.Internet.Email(),
                    Senha: "@Teste1234"
                );

            //Act            
            //Enviando os dados para o endpoint de autenticação de usuário
            var response = _client.PostAsJsonAsync("/api/usuario/autenticar", request).Result;

            //Asserts
            //Verificar se a API retorna HTTP 401
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            //Capturar o conteudo da segunda resposta
            var json = response.Content.ReadAsStringAsync().Result;
            //Verificando se contem as mensagens de erro
            json.Should().Contain("Acesso negado. Email não encontrado.");
        }
    }
}



