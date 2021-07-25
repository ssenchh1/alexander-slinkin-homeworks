using System.Security.Cryptography;
using System.Text;

namespace Practice17may
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new FileProvider();

            provider.Run();
        }
    }
}
