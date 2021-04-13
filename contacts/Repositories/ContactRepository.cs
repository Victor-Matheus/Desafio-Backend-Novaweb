using System.Collections.Generic;
using System.Threading.Tasks;
using contacts.Data;
using contacts.Enums;
using contacts.Models;
using contacts.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        public async Task<ActionResult<List<Contact>>> _getAllContacts(Data.DataContext _context)
        {
            var res = await _context.Contacts.ToListAsync();
            return res;
        }
        public async Task<EDbStatusReturn> _createContact(Data.DataContext _context)
        {
            try
            {
                await _context.SaveChangesAsync();
                return EDbStatusReturn.DB_SAVED_OK;
            }
            catch
            {
                return EDbStatusReturn.DB_GENERAL_EXCEPTION;
            }
        }

        public async Task<EDbStatusReturn> _deleteContact(DataContext _context, Contact _contact)
        {
            try
            {
                _context.Contacts.Remove(_contact);
                await _context.SaveChangesAsync();
                return EDbStatusReturn.DB_SAVED_OK;
            }
            catch
            {
                return EDbStatusReturn.DB_GENERAL_EXCEPTION;
            }
        }

        public async Task<Contact> _getContactById(DataContext _context, int id)
        {
            var _contact = await _context.Contacts.FindAsync(id);
            return _contact;
        }
    }
}