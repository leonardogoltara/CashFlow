using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.DTOs
{
    public class CashInDTO
    {
        public CashInDTO()
        {

        }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
