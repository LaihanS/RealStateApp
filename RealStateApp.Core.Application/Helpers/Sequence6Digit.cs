using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Application.Helpers
{
    public class Sequence6Digit
    {
        public string Secuencia(List<string> sequences)
        {
            Random random = new Random();
            string secuencia;
            do
            {
                secuencia = random.Next(100000, 999999).ToString().PadLeft(6, '0');
            } while (sequences.Contains(secuencia));

            return secuencia;
        }

    }
}
