using AutoMapper;
using DevWEB.Api.DTOs;
using DevWEB.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace DevWEB.Api.Controllers
{
    [Route("api/fornecedores")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<FornecedorDTO>>(await _fornecedorRepository.ObterTodos()));
        }
    }
}
