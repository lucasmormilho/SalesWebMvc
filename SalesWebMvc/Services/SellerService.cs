using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return _context.Seller.FirstOrDefault(x => x.Id == id);
            //return _context.Seller.Include(s => s.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var x = _context.Seller.Find(id);
            _context.Seller.Remove(x);
            _context.SaveChanges();
        }
    }
}
