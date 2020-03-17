using ContactSystem.Contracts;
using ContactSystem.Implementation;
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
        private readonly IContactsRepository _icontactsRepository;
        
        public ContactsController(IContactsRepository icontactsRepository)
        {
            _icontactsRepository = icontactsRepository;
        }
        public ContactsController()
        {
            _icontactsRepository = new ContactRepository(new ContactInfoEntities());
            
        }
        public ActionResult ShowContacts()
        {
            IEnumerable<Contact> contact = _icontactsRepository.GetContact();
            return View(contact);
            
        }
        public ActionResult CreateContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateContact(Contact contact)
        {
            _icontactsRepository.NewContact(contact);
            return RedirectToAction("ShowContacts");
        }
        [HttpGet]
        public ActionResult EditContacts(int Id)
        {
            Contact EditedContact = _icontactsRepository.GetContactById(Id);
            return View(EditedContact);
            
        }
        
        public ActionResult EditContacts(Contact contact)
        {
            _icontactsRepository.UpdateContact(contact);
            return RedirectToAction("ShowContacts");
        }
        public ActionResult DeleteContact(int Id)
        {
            _icontactsRepository.DeleteContact(Id);
            return RedirectToAction("ShowContacts");
        }
    }
}
