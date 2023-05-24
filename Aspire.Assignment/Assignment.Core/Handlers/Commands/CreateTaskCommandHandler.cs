using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.Handlers.Commands
{
    public class CreateTaskCommand : IRequest<int>
    {
        public CreateTaskDTO Model { get; }
        public CreateTaskCommand(CreateTaskDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateTaskDTO> _validator;

        public CreateTaskCommandHandler(IUnitOfWork repository, IValidator<CreateTaskDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            CreateTaskDTO model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = new TaskDetail
            {
                TaskId = model.TaskId,
                Username = model.Username,
                Title = model.Title,
                TaskDescription = model.TaskDescription,
                FileName = model.File.FileName.ToString(),
                Status=model.Status
            };

            _repository.TaskDetail.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }

}





