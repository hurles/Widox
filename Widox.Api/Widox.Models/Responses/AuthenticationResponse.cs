using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widox.Models.Responses
{
    public class WidoxAuthenticationResponse
    {
        public string? Token { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
