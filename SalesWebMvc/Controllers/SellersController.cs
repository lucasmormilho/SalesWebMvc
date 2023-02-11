using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
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
        //dependencia do departmentservie
        private readonly DepartmentService _departmentService;

        //construtor
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
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
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            //passando a lista de departamentos para o index do seller
            return View(viewModel);
        }

        //notação POST
        [HttpPost]
        //notação contra ataque
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            //usar nameof para se um dia mudar o nome do index
            //não precisa mudar nada aqui
            return RedirectToAction(nameof(Index));
        }

        //Tela de confirmação de delete GET
        public IActionResult Delete(int? id) //opcional
        {
            if (id == null)//tratamento caso id incorreto
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //efetiva a confirmação
        //notação POST
        [HttpPost]
        //notação contra ataque
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        //Tela de confirmação de detalhes GET
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
    }
}
