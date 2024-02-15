using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Bibliography;
using Homework.Models;
using Omu.ValueInjecter;

namespace Homework.Controllers
{
    public class ClientDataController : Controller
    {
        客戶資料Repository repo;
        客戶聯絡人Repository repoContact;
        客戶銀行資訊Repository repoBankData;
        ClientViewRepository repoView;
        ClientTypeRepository repoClientType;



        // GET: 客戶資料
        public ClientDataController()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
            repoContact = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
            repoBankData = RepositoryHelper.Get客戶銀行資訊Repository(repo.UnitOfWork);
            repoView = RepositoryHelper.GetClientViewRepository(repo.UnitOfWork);
            repoClientType = RepositoryHelper.GetClientTypeRepository(repo.UnitOfWork);
        }
        // GET: ClientData
        [Route("{controller}/{action}/{clientType}")]
        public ActionResult Index(string clientType)
        {
            //ViewBag.ClientType = new SelectList(repo.All().Select(a=>a.客戶分類).Distinct());
            ViewBag.ClientType = new SelectList(repoClientType.All(), "Name", "Name");
            ViewData["selectedClientType"] = string.Empty;

            if (clientType != null)
            {
                ViewData["selectedClientType"] = clientType;
                return View(repo.FilterByClientType(clientType).ToList());
            }
            return View(repo.All().ToList());
        }
        // GET: ClientView
        public ActionResult ClientView()
        {            
            return View(repoView.All().ToList());
        }

        // GET: ClientData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: ClientData/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類 = new SelectList(repoClientType.All(), "Name", "Name");

            return View();
        }

        // POST: ClientData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,是否已刪除")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.是否已刪除 = false;
                repo.Add(客戶資料);
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: ClientData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶分類 = new SelectList(repoClientType.All(), "Name", "Name", 客戶資料.客戶分類);

            return View(客戶資料);
        }

        // POST: ClientData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,是否已刪除")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var clientData = repo.Find(客戶資料.Id);
                clientData.InjectFrom(客戶資料);
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: ClientData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: ClientData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            repo.Delete(客戶資料);
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
