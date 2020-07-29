
using AutoMapper;
using Full_RestfulAPI.Data;
using Full_RestfulAPI.DTOs;
using Full_RestfulAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Full_RestfulAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;  // assigne dependency injected value to _repository
            _mapper = mapper;
        }

        
        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            if (commandItems == null)
            {
               return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }
        

        //GET api/commands/{id}
        [HttpGet("{id}", Name= "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

       
        [HttpPost]
        public ActionResult<CommandCreateDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById ), new { Id = commandReadDto.Id }, commandReadDto);
           // return Ok(commandReadDto);
        }

        
        [HttpPut("{id}")]
        public ActionResult UpdateCommad(int id , CommandUpdateDto commandUpdateDto)
        {
            var command = _repository.GetCommandById(id);

            if (command == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, command);

            //_repository.UpdateCommand(command); // Useless in this case 

            _repository.SaveChanges();

            return NoContent();
        }
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDto> patchDocument)
        {
            var command = _repository.GetCommandById(id);
            if (command == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(command);

            patchDocument.ApplyTo(commandToPatch,ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, command);

            //_repository.UpdateCommand(command);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var command = _repository.GetCommandById(id);
            if (command == null)
            {
                NotFound();
            }
            _repository.DeleteCommand(command);

            _repository.SaveChanges();
            return NoContent();
        }
    }
}
