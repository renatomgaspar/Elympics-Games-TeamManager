using Elympics_Games.API.Data.Entities;
using Elympics_Games.Mobile.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Elympics_Games.API.Data
{
    public class SeedDb
    {
        private readonly AppDbContext _context;
        private readonly PasswordService<User> _passwordService;

        public SeedDb(AppDbContext context, PasswordService<User> passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Users.Any())
            {
                AddUser("João Meireles", "joaomeimei@sapo.pt", "senhaSegura123", "User");
                AddUser("Maria Silva", "mariasilva@gmail.com", "senha456", "Admin");
                AddUser("Pedro Lopes", "pedrolopes@outlook.com", "senha789", "User");
                AddUser("Rita Amaral", "rita.amaral@gmail.com", "passwordRita123", "Admin");
                AddUser("Carlos Pinho", "carlos.pinho@outlook.pt", "pinhoSenha456", "User");
                AddUser("Sofia Andrade", "sofia.andrade@gmail.com", "sofia789@", "Moderator");
                AddUser("Miguel Faria", "miguel.faria@outlook.pt", "miguelPass321", "User");
                AddUser("Inês Nogueira", "ines.nogueira@gmail.com", "inesSenha#654", "Admin");
                AddUser("Tiago Costa", "tiago.costa@outlook.pt", "tiagoSegredo999", "User");
            }

            if (!_context.Teams.Any()) 
            {
                AddTeam("Herois do Mar", "Portugal", 16);
                AddTeam("Auf der ganzen Welt", "Germany", 24);
                AddTeam("Voler haut", "France", 24);
                AddTeam("何よりも謙虚さ", "Japan", 22);
                AddTeam("Sigamos Adelante", "Spain", 24);
                AddTeam("Sempre in profondità", "Italy", 20);
            }

            await _context.SaveChangesAsync();
        }

        private void AddUser(string name, string email, string password, string type)
        {
            var user = new User
            {
                Name = name,
                Email = email,
                Password = "",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = type
            };

            user.Password = _passwordService.HashPassword(user, password);

            _context.Users.Add(user);
        }

        private void AddTeam(string name, string country, int elementsNumber)
        {
            var team = new Team
            {
                Name = name,
                Country = country,
                ElementsNumber = elementsNumber
            };

            _context.Teams.Add(team);
        }
    }
}
