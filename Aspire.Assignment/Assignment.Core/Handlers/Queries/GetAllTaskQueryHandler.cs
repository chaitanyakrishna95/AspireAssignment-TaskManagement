using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.Handlers.Queries
{

    public class GetAllTaskQuery : IRequest<IEnumerable<TaskDetailDTO>>
    {
    }

    public class GetAllTaskQueryHandler : IRequestHandler<GetAllTaskQuery, IEnumerable<TaskDetailDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllTaskQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

      

        public async Task<IEnumerable<TaskDetailDTO>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.TaskDetail.GetAll());
            return _mapper.Map<IEnumerable<TaskDetailDTO>>(entities);
        }
    }
    
}
