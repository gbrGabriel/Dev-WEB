using DevWEB.Business.Intefaces;
using DevWEB.Business.Models;
using DevWEB.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevWEB.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ContextDB context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }
    }
}