using SaintSender.Core.Entities;
using SaintSender.DesktopUI;
using SaintSender.DesktopUI.DatabaseRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.DatabaseRelated
{
    public class SQLUserRepository : IDataService
    {
        private readonly AppDbContext _context;

        public SQLUserRepository(AppDbContext context)
        {
            _context = context;
        }


        //public void AddEmails()
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddUser()
        //{
            
        //}

        //public List<Email> GetAllEmails()
        //{
        //    throw new NotImplementedException();
        //}

        //public User GetUser(User user)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateEmails()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
