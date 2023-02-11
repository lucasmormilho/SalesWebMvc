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
        public List<Seller> findAll()
        {
            return _context.Seller.ToList();
        }

        //metodo para inserir novo registro
        public void Insert(Seller obj)
        {
            //pegar o primeiro departamento que existir
            //para previnir um erro
            //usado antes de obter o valor da view
            //obj.Department = _context.Department.First();

            //adiciona na base de dados
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            //eager loading = carregar objetos associados ao principal - join
            //Para tela details é necessario fazer join (include) e trazer departamento
            return _context.Seller.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var x = _context.Seller.Find(id);
            _context.Seller.Remove(x);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(y => y.Id == obj.Id)) //teste se não tem no banco de dados
            {
                throw new NotFoundException("Id not found");
            }

            //para tratar erro do banco de concorrencia
            try
            {
                _context.Update(obj); //update do entity
                _context.SaveChanges();
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
