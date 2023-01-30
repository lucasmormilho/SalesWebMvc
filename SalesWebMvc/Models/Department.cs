using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //coleção de vendedores
        //associação do departamento com varios seller já iniciando uma lista
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        //construtores vazios (necessário para o framework)
        public Department()
        {
        }

        //construtores com argumento sem as coleções (necessário para o framework)
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //Metodos customizados
        //add vendedor
        public void AddSeller (Seller seller)
        {
            Sellers.Add(seller);
        }

        //total de vendas
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(x => x.TotalSales(initial, final)); //ESTUDAR
        }
    }
}
