using ContactSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactSystem.Controllers
{
    public class ContactDataController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllContacts()
        {
            IList<Contact> allContact = null;
            ContactInfoEntities entities = new ContactInfoEntities();
            allContact = entities.Contacts.ToList<Contact>();
            if (allContact.Count == 0)
            {
                return NotFound();
            }
            return Ok(allContact);
        }
        [HttpGet]
        public IHttpActionResult GetContacts(int Id)
        {
            ContactInfoEntities entities = new ContactInfoEntities();
            Contact FilteredContact = entities.Contacts.Where(c => c.Id == Id).FirstOrDefault();
            return Ok(FilteredContact);
        }
        [HttpPost]
        public IHttpActionResult Post(Contact contact)
        {
            using (ContactInfoEntities entities = new ContactInfoEntities())
            {
                entities.Contacts.Add(contact);
                entities.SaveChanges();
            }

            return Ok(contact);
        }

        [HttpPut]
        public bool UpdateContact(Contact contact)
        {
            using (ContactInfoEntities entities = new ContactInfoEntities())
            {
                Contact updatedContact = (from c in entities.Contacts
                                          where c.Id == contact.Id
                                          select c).FirstOrDefault();

                updatedContact.FirstName = contact.FirstName;
                updatedContact.LastName = contact.LastName;
                updatedContact.PhoneNo = contact.PhoneNo;
                updatedContact.Email = contact.Email;
                updatedContact.Status = contact.Status;


                entities.SaveChanges();
            }

            return true;
        }

        [HttpDelete]
        public IHttpActionResult DeleteContact(int Id)
        {
            if (Id <= 0)
                return BadRequest("Not a valid contact id");

            using (ContactInfoEntities entities = new ContactInfoEntities())
            {
                var student = entities.Contacts
                    .Where(s => s.Id == Id)
                    .FirstOrDefault();

                entities.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                entities.SaveChanges();
            }

            return Ok();
        }
    }
}
