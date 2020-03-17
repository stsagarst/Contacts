using ContactSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ContactSystem.Implementation
{
    public class ContactRepository : IContactsRepository
    {
        private readonly ContactInfoEntities _dbContext;
        HttpClient client = new HttpClient();

        public ContactRepository()
        {
            
        }
        public ContactRepository(ContactInfoEntities context)
        {
            client.BaseAddress = new Uri("http://localhost:49199/api/");
            //_dbContext = new ContactInfoEntities();
            _dbContext = context;
        }

        public IEnumerable<Contact> GetContact()
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

               // ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            return contact;
        }

        public Contact GetContactById(int Id)
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
            return EditedContact;
           
        }

        public void NewContact(Contact contact)
        {
            //HTTP POST
            var postTask = client.PostAsJsonAsync<Contact>("ContactData", contact);
            postTask.Wait();

            var result = postTask.Result;
            //if (result.IsSuccessStatusCode)
            //{
            //   return RedirectToAction("ShowContacts");
            //}

            //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

        }

        public void UpdateContact(Contact contact)
        {
            //HTTP POST
            var postTask = client.PutAsJsonAsync<Contact>("ContactData", contact);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                //return RedirectToAction("ShowContacts");
            }

           // ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            //_dbContext.Entry(contact).State = EntityState.Modified;
        }

        public void DeleteContact(int id)
        {
            //HTTP DELETE
            var deleteTask = client.DeleteAsync("ContactData?Id=" + id);
            deleteTask.Wait();

            var result = deleteTask.Result;

            //if (result.IsSuccessStatusCode)
            //{

            //    return RedirectToAction("ShowContacts");
            //}

            //var contact = _dbContext.Contacts.Find(id);
            //if (contact != null) _dbContext.Contacts.Remove(contact);
        }
        
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}