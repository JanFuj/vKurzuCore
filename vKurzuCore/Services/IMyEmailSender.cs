using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Services
{
    public interface IMyEmailSender
    {
        Task<bool> SendEmailFromForm(string from, string subject, string body);
    }
}
