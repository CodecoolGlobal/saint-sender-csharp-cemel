using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    
    public class SetupClient
    {
        private Pop3Client _client;
        public Pop3Client Setup(string userName, string password)
        {
            _client = new Pop3Client();
            _client.Connect("pop.gmail.com", 995, true);
            _client.Authenticate(userName, password, AuthenticationMethod.UsernameAndPassword);
            return _client;
        }
    } 
}
