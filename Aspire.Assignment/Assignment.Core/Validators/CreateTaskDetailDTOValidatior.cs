using Assignment.Contracts.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.Validators
{
    public class CreateTaskDetailDTOValidatior: AbstractValidator<CreateTaskDTO>
    {
        public CreateTaskDetailDTOValidatior()
        {
            RuleFor(x => x.Status).NotEmpty().WithMessage("status is required");
            RuleFor(x => x.TaskDescription).NotEmpty().WithMessage("Provide a brief description about the task");
          
        }
    }
}
