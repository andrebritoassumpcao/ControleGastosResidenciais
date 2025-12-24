using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string code, string message) : base(code, message) { }
    }
}
