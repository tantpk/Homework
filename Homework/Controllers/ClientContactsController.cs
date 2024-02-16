using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework.Models;
using Omu.ValueInjecter;

namespace Homework.Controllers
{
    public class ClientContactsController : Controller
    {
        客戶資料Repository repo;
        客戶聯絡人Repository repoContact;
        客戶銀行資訊Repository repoBankData;

        // GET: 客戶資料
        public ClientContactsController()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
            repoContact = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
            repoBankData = RepositoryHelper.Get客戶銀行資訊Repository(repo.UnitOfWork);
        }
        // GET: ClientContacts
        [Route("{controller}/{action}/{clientTitle}")]
        public ActionResult Index(string clientTitle)
        {            
            ViewBag.ClientTitle = new SelectList(repoContact.All().Select(a => a.職稱).Distinct());
            var 客戶聯絡人 = repoContact.All().Include(客 => 客.客戶資料);
            ViewData["selectedClientTitle"] = string.Empty;
            if (clientTitle != null)
            {
                ViewData["selectedClientTitle"] = clientTitle;
                return View(repoContact.FilterByClientTitle(clientTitle).ToList());
            }
            return View(客戶聯絡人.ToList());
        }

        // GET: ClientContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: ClientContacts/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: ClientContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            ViewBag.客戶Id = new SelectList(repo.All(), "Id", "客戶名稱");
            if (ModelState.IsValid)
            {
                //if (repoContact.IsEmailDuplicated(客戶聯絡人.Email, 客戶聯絡人.客戶Id))
                //{
                //    ModelState.AddModelError("Email", "The email is already exist in our system");
                //    return View(客戶聯絡人);
                //}
                客戶聯絡人.是否已刪除 = false;
                repoContact.Add(客戶聯絡人);
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: ClientContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }            
            ViewBag.客戶Id = new SelectList(repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: ClientContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                //if (repoContact.IsEmailDuplicated(客戶聯絡人.Email, 客戶聯絡人.客戶Id))
                //{
                //    ModelState.AddModelError("Email", "The email is already exist in our system");
                //    ViewBag.客戶Id = new SelectList(repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
                //    return View(客戶聯絡人);
                //}
                var clientContact = repoContact.Find(客戶聯絡人.Id);
                clientContact.InjectFrom(客戶聯絡人);
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: ClientContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: ClientContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id);
            repoContact.Delete(客戶聯絡人);
            repoContact.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
