﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //----------------------------------------------------------------------

        public IActionResult Index()
        {
            //retorna da base de dados uma lista
            var list = _sellerService.findAll();
            return View(list);
        }

        //----------------------------------------------------------------------

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            //passando a lista de departamentos para o index do seller
            return View(viewModel);
        }

        //----------------------------------------------------------------------

        //notação POST
        [HttpPost]
        //notação contra ataque
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            //tratamento para verificar se o javascript foi desabilitado
            //se for desabilitado não tera controle de envio de dados
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            _sellerService.Insert(seller);
            //usar nameof para se um dia mudar o nome do index
            //não precisa mudar nada aqui
            return RedirectToAction(nameof(Index));
        }

        //----------------------------------------------------------------------

        //Tela de confirmação de delete GET
        public IActionResult Delete(int? id) //opcional
        {
            if (id == null)//tratamento caso id incorreto
            {
                return RedirectToAction(nameof(Error), new { message = "Id null" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        //----------------------------------------------------------------------

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

        //----------------------------------------------------------------------

        //Tela de confirmação de detalhes GET
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id null" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        //----------------------------------------------------------------------

        //tela de editar GET
        public IActionResult Edit(int? id) //opcional só para evitar erro de execução
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id null" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            //Carregar departamentos na lista de departamentos
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel
            {
                Seller = obj, //preencher os campos com os dados do obj
                Departments = departments //preencher caixa de seleção com os departamentos
            };

            return View(viewModel);
        }

        //----------------------------------------------------------------------

        //efetiva a confirmação
        //notação POST
        [HttpPost]
        //notação contra ataque
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            //tratamento para verificar se o javascript foi desabilitado
            //se for desabilitado não tera controle de envio de dados
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            //verifica se o id é igual
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            //verificar se não da erro
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e) //upcasting de erros
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        //----------------------------------------------------------------------

        //ação para retornar erros personalizados
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //macete para pegar o id interno da requisição
            };
            return View(viewModel);
        }
    }
}
