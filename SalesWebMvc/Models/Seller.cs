using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        //Atributos basicos
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} obrigatório")] //difinindo que o campo é obrigatorio
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve ter entre {2} e {1}")] //definido tamnaho personalizado
        public string Name { get; set; }

        //data anotation para modificar o elemento no html
        [DataType(DataType.EmailAddress)] //formatar campo email
        [Required(ErrorMessage = "{0} obrigatório")] //difinindo que o campo é obrigatorio
        [EmailAddress(ErrorMessage = "Entre com {0} válido")]
        public string Email { get; set; }

        //data anotation para modificar o elemento no html
        [Display(Name = "Data Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} obrigatório")] //difinindo que o campo é obrigatorio
        public DateTime Birthdate { get; set; }

        [Display(Name = "Salario Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //formatar boleano
        [Required(ErrorMessage = "{0} obrigatório")] //difinindo que o campo é obrigatorio
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }

        //associação do vendedor com 1 departamento
        public Department Department { get; set; }
        //garantir que o Id do departamento tem que existir no entity
        public int DepartmentId { get; set; }
        //associação do vendedor com varias vendas já iniciando uma lista
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        //construtores vazios (necessário para o framework)
        public Seller()
        {
        }

        //construtores com argumento sem as coleções (necessário para o framework)
        public Seller(int id, string name, string email, DateTime birthdate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            Birthdate = birthdate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //Metodos customizados

        //add vendas
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        //remover vendas
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        //total de vendas em linq
        public double TotalSales(DateTime inicial, DateTime final)
        {
            return Sales.Where(x => x.Date >= inicial && x.Date <= final).Sum(y => y.Amount);
        }
    }
}
