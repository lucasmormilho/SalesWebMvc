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
            obj.Department = _context.Department.First();
            //adiciona na base de dados
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
