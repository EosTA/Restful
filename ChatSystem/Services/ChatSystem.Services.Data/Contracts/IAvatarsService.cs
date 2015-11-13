using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Services.Data.Contracts
{
    public interface IAvatarsService
    {
        void Post(string avatarUrl, string username);

        void Delete(string username);
    }
}
