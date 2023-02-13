using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        //dependencia para o db_context
        //usar o readonly para previnir que essa dependencia não seja alterada
        private readonly SalesWebMvcContext _context;

        //construtor
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Metodo para trazer a lista de departamentos
        //UTILIZAR METODO TASK ASINC PARA FICAR ASSINCRONA
        //TRANFORMADO EM ASINCRONA EM 13/02/2023 (task, async, await)
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
