using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        //dependencia para o db_context
        //usar o readonly para previnir que essa dependencia não seja alterada
        private readonly SalesWebMvcContext _context;

        //construtor
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //acessar minha fonte de dados
        //converte para uma lista
        //sincrona
        //TRANFORMADO EM ASINCRONA EM 13/02/2023 (task, async, await)
        public async Task<List<Seller>> findAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //metodo para inserir novo registro
        //TRANFORMADO EM ASINCRONA EM 13/02/2023 (task, async, await)
        public async Task InsertAsync(Seller obj)
        {
            //pegar o primeiro departamento que existir
            //para previnir um erro
            //usado antes de obter o valor da view
            //obj.Department = _context.Department.First();

            //adiciona na base de dados
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //TRANFORMADO EM ASINCRONA EM 13/02/2023 (task, async, await)
        public async Task<Seller> FindByIdAsync(int id)
        {
            //eager loading = carregar objetos associados ao principal - join
            //Para tela details é necessario fazer join (include) e trazer departamento
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        //TRANFORMADO EM ASINCRONA EM 13/02/2023 (task, async, await)
        public async Task RemoveAsync(int id)
        {
            //colocar dentro de um bloco try 14/02
            try
            {
                var x = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(x);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                //tratamento para enviar um erro personalizado
                //erro ao deletar um vendedor com vendas
                throw new IntegrityException("Erro, vendedor com vendas:" + e.Message);
            }

        }

        //TRANFORMADO EM ASINCRONA EM 13/02/2023 (task, async, await)
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(y => y.Id == obj.Id);
           
            if (!hasAny) //teste se não tem no banco de dados
            {
                throw new NotFoundException("Id not found");
            }

            //para tratar erro do banco de concorrencia
            try
            {
                _context.Update(obj); //update do entity
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e) //erro especifico do banco com entity
            {
                //lanço a excessão de acesso a banco a minha excessão personalizada
                //pego uma excessão de nivel de acesso a dados
                //e coloco a nivel de serviço
                //isso é um tratamento de camada
                //segregação de camadas é importante
                //não é bom propagar uma excessão de acesso a dados
                //é imporante lançar uma excessão personalizada
                throw new DbConcurrencyException(e.Message); 
            }

        }
    }
}
