using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Domain.ValueObjects
{
    public class PasswordVo
    {
        public string Old { get; set; }

        public string New { get; set; }

        public string Repeat { get; set; }
    }
}
