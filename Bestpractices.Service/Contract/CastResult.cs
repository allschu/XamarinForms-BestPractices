using System;
using System.Collections.Generic;
using System.Text;

namespace Bestpractices.Service.Contract
{
    public class CastResult
    {
        public int Id { get; set; }
        public ICollection<CastDTO> Cast { get; set; }
    }
}
