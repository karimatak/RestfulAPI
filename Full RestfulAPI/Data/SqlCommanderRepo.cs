﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Full_RestfulAPI.Models;

namespace Full_RestfulAPI.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context; 

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            _context.Commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            _context.Remove(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command command)
        {
            
        }
    }
}
