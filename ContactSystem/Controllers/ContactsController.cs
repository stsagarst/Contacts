using ContactSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ContactSystem.Controllers
{
    public class ContactsController : Controller
    {
        HttpClient client = new HttpClient();
        public ContactsController()
        {
           
            client.BaseAddress = new Uri("http://localhost:49199/api/");
        }
        public ActionResult ShowContacts()
        {
            IEnumerable<Contact> contact = null;
            
            var postTask = client.GetAsync("ContactData");
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Contact>>();
                readTask.Wait();
                contact = readTask.Result;

            }
            else //web api sent error response 
            {
                contact = Enumerable.Empty<Contact>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(contact);
            
        }
        public ActionResult CreateContact()
       {
            return View();
        }
        [HttpPost]
        public ActionResult CreateContact(Contact contact)
        {
           
            //HTTP POST
            var postTask = client.PostAsJsonAsync<Contact>("ContactData", contact);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowContacts");
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("ShowContacts");
        }
        [HttpGet]
        public ActionResult EditContacts(int Id)
        {
            Contact EditedContact = null;
           
            //HTTP POST
            var postTask = client.GetAsync("ContactData?ID=" + Id);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Contact>();
                readTask.Wait();
                EditedContact = readTask.Result;

            }
            return View(EditedContact);
            
        }
        
        public ActionResult EditContacts(Contact contact)
        {
          
            //HTTP POST
            var postTask = client.PutAsJsonAsync<Contact>("ContactData", contact);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowContacts");
            }
           
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("ShowContacts");
        }
        public ActionResult DeleteContact(int Id)
        {
            //HTTP DELETE
            var deleteTask = client.DeleteAsync("ContactData?Id=" + Id);
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("ShowContacts");
            }
            return RedirectToAction("ShowContacts");
        }
    }
}
