using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services
{
    public class ConsoleOutputService : IOutputService
    {
        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }
    }
}
