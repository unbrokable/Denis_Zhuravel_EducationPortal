using EducationPortal.Interfaces;
using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationPortal.Managers.Commands
{
    class MaterialCreateCommands : ICommand<MaterialViewModel>
    {
        private readonly List<ICommandMaterial> commands;

        public MaterialCreateCommands(IEnumerable<ICommandMaterial> commands)
        {
            this.commands = commands.ToList();
        }
        public MaterialViewModel Execute(int idUser)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                Console.WriteLine($"Command {i}  {commands[i].ToString()}");
            }

            string answer = Console.ReadLine();
            ICommandMaterial command = commands[int.Parse(answer)];

            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Location:");
            string location = Console.ReadLine();
            return command.Execute(name, location, idUser);

        }
    }
}
