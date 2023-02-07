using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //------inicio obrigatorio
        //dependencia do sellerservice
        private readonly SellerService _sellerService;
        //construtor
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        //------fim obrigatorio


        public IActionResult Index()
        {
            //retorna da base de dados uma lista
            var list = _sellerService.findAll();
            return View(list);
        }
    }
}
