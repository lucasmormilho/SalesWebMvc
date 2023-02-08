using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

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
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
