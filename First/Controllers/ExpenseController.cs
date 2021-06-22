﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using First.Data;
using First.Models;

namespace First.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AplicationDBContext _db;

        public ExpenseController(AplicationDBContext context) {
            this._db = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> expenses = this._db.Expenses;
            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
     
        public IActionResult Create(Expense attributes)
        {
            if (ModelState.IsValid)
            {
                this._db.Expenses.Add(attributes);
                this._db.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                return View(attributes);
            }
        }

        //Get single expense
        [HttpGet]

        public IActionResult Show(int? Id)
        {
            if (Id != null || Id != 0) {
                var expense = this._db.Expenses.Find(Id);
                if (expense != null)
                {
                    return View(expense);
                }
            }
            return NotFound();
        }


        [HttpGet]
        public IActionResult Update(int? Id) {

            var expense = this._db.Expenses.Find(Id);
            if (expense == null) {
                return NotFound();
            }

            return View(expense);
        }

        [HttpPost]
        public IActionResult Update(Expense attributes)
        {

            this._db.Expenses.Update(attributes);
            this._db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Post delete
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            //return Ok("ID: "+Id);

            var expense=this._db.Expenses.Find(Id);

            if (expense == null) {
                return NotFound();
            }

         

            return View(expense);
        }

        [HttpPost]
        public IActionResult DeleteAction(int? Id)
        {
            //return Ok("ID: "+Id);

            var expense = this._db.Expenses.Find(Id);

            if (expense == null)
            {
                return NotFound();
            }

            this._db.Remove(expense);
            this._db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
