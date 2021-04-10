using System.Collections.Generic;
using System.Threading.Tasks;
using contacts.Enums;
using Microsoft.AspNetCore.Mvc;

namespace contacts.Repositories.Contracts
{
    public interface IContactRepository
    {
        Task<ActionResult<List<Models.Contact>>> _getAllContacts(Data.DataContext _context);
        Task<EDbStatusReturn> _createContact(Data.DataContext _context);
    }
}