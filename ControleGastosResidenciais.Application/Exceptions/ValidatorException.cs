using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Exceptions
{
    public class ValidatorException : BaseException
    {
        public ValidatorException(string code, string message) : base(code, message) { }
    }
}
