using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
using Microsoft.AspNetCore.Mvc;

namespace Repositories.Contracts
{
    public interface IContactRepository
    {
        Task<ActionResult<List<Models.Contact>>> _getAllContacts(Data.DataContext _context);
        Task<Models.Contact> _getContactById(Data.DataContext _context,int id);
        Task<EDbStatusReturn> _createContact(Data.DataContext _context);
        Task<EDbStatusReturn> _deleteContact(Data.DataContext _context, Models.Contact _contact);
    }
}