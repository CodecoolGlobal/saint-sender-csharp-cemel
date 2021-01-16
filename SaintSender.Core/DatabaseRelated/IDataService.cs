﻿using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.DatabaseRelated
{
    public interface IDataService
    {
        User GetUser(User user);
        List<Email> GetAllEmails();
    }
}
