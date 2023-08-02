using AutoMapper;
using DevWEB.Api.DTOs;
using DevWEB.Business.Intefaces;
using DevWEB.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevWEB.Api.Controllers
{
    [Route("api/fornecedores")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;
        public FornecedoresController(IFornecedorRepository fornecedorRepository, IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<FornecedorDTO>>(await _fornecedorRepository.ObterTodos()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>> ObterPorId(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            if (fornecedor == null) NotFound();

            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorDTO>> Adicionar(FornecedorDTO model)
        {
            if (!ModelState.IsValid) BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(model);

            await _fornecedorService.Adicionar(fornecedor);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>> Adicionar(Guid id, FornecedorDTO model)
        {
            if (id != model.Id) BadRequest();

            if (!ModelState.IsValid) BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(model);

            await _fornecedorService.Atualizar(fornecedor);

            return Ok(fornecedor);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>> Excluir(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor != null) BadRequest();

            await _fornecedorService.Remover(id);

            return Ok();
        }

        private async Task<FornecedorDTO> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorDTO>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
        private async Task<FornecedorDTO> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorDTO>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }
    }
}
