using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    class SetUpClient
    {
        private static string _userName;
        private static string _password;
        private Pop3Client _client;
        public void SetupClient(string userName, string password)
        {
            _userName = userName;
            _password = password;
            SetupClient();
        }

        public void SetupClient()
        {
            _client = new Pop3Client();
            _client.Connect("pop.gmail.com", 995, true);
            //_client.Authenticate("csharptw5@gmail.com", "Csharp123", AuthenticationMethod.UsernameAndPassword);
            _client.Authenticate(_userName, _password, AuthenticationMethod.UsernameAndPassword);
        }

    }
}
