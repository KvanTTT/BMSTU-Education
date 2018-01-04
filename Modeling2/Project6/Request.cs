using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _7sem_model_6
{
    class Request
    {
        public double T { get; set; }
        public bool IsToPayOffice { get; set; }

        public Request(double T, bool IsToPayOffice)
        {
            this.T = T;
            this.IsToPayOffice = IsToPayOffice;
        }
    }
}
