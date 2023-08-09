using AutoMapper;
using DevWEB.Api.DTOs;
using DevWEB.Business.Intefaces;
using DevWEB.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevWEB.Api.Controllers
{
    [Route("api/v1/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        public ProdutosController(INotificador notificador, IProdutoRepository produtoRepository,
                IProdutoService produtoService, IMapper mapper) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IEnumerable<ProdutoDTO>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtoRepository.ObterProdutosFornecedores());
        }

        [HttpGet("{id:guid}", Name = "ObterPorId")]
        public async Task<ActionResult<ProdutoDTO>> ObterPorId(Guid id)
        {
            var produtoModel = await ObterProduto(id);

            if (produtoModel == null) return NotFound();

            return produtoModel;
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Adicionar(ProdutoDTO model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + model.Imagem;

            if (!UploadArquivo(model.ImagemUpload, imagemNome))
            {
                return CustomResponse();
            }

            model.Imagem = imagemNome;

            await _produtoService.Adicionar(_mapper.Map<Produto>(model));

            return CustomResponse(model);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoDTO>> Excluir(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null) return NotFound();

            await _produtoService.Remover(id);

            return CustomResponse(produto);
        }

        private async Task<ProdutoDTO> ObterProduto(Guid id)
        {
            return _mapper.Map<ProdutoDTO>(await _produtoRepository.ObterProdutoFornecedor(id));
        }

        private bool UploadArquivo(string arquivo, string imgNome)
        {
            var imageDataByteArray = Convert.FromBase64String(arquivo);

            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneça uma imagem para este produto!");
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com esse nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }
    }
}
