using ReceiverApplication.Process;
using ReceiverApplication.Services;

namespace ReceiverApplication
{
    class Program
    {
        static void Main()
        {
            Printer defaultPrinter = Printer.GetInstance();
            PrintingJobsService service = new PrintingJobsService(defaultPrinter);
            service.Initialize();
        }
    }
}
