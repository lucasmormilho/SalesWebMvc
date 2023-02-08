using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        //anotação POST
        [HttpPost]
        //anotação contra ataque
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            //usar nameof para se um dia mudar o nome do index
            //não precisa mudar nada aqui
            return RedirectToAction(nameof(Index));
        }
    }
}
