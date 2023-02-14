using SalesWebMvc.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        //Atributos basicos
        public int Id { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }
        //associação das vendas com 1 vendedor
        public Seller Seller { get; set; }

        //construtores vazios (necessário para o framework)
        public SalesRecord()
        {
        }

        //construtores com argumento sem as coleções (necessário para o framework)
        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }

    }
}
