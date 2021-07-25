using Practice11june.BLL.Services;

namespace Practice11june
{
    class Program
    {
        static void Main(string[] args)
        {
            var hidingService = new HidingDataService();

            hidingService.HideInfo("a");
        }
    }
}
