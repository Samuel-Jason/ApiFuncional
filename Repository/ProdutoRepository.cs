using ApiTesta.Data;
using ApiTesta.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTesta.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly BancoContext _context;

        public ProdutoRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<Produto> Create(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }


        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.Include(x => x.Categoria).ToListAsync();
        }

        public async Task<Produto> GetById(int id)
        {
            return await _context.Produtos
                .Include(c => c.Categoria)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        //public async Task<Produto> Update(Produto produto)
        //{
        //    var produtoExistente = await _context.Produtos.FindAsync(produto.Id);

        //    if (produtoExistente == null)
        //        throw new Exception("Produto não encontrado.");

        //    produtoExistente.Nome = produto.Nome;
        //    produtoExistente.Preco = produto.Preco;
        //    produtoExistente.Descricao = produto.Descricao;
        //    produtoExistente.Estoque = produto.Estoque;
        //    produtoExistente.ImageURL = produto.ImageURL;
        //    produtoExistente.CategoriaId = produto.CategoriaId;

        //    await _context.SaveChangesAsync();

        //    return produtoExistente;
        //}
        public async Task<Produto> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return null;
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> Update(Produto produto)
        {
           _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }
    }
}
