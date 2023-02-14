using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordsService
    {
        //injeção de dependencia
        private readonly SalesWebMvcContext _context;

        public SalesRecordsService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async  Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //pesquisa via linq
            var resultado = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                resultado = resultado.Where(y => y.Date <= maxDate.Value);
            }
            return await resultado
                .Include(x => x.Seller) //join
                .Include(y => y.Seller.Department) //join
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            //pesquisa via linq
            var resultado = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                resultado = resultado.Where(y => y.Date <= maxDate.Value);
            }
            return await resultado
                .Include(x => x.Seller) //join
                .Include(y => y.Seller.Department) //join
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
