using JobLogger.Services;

namespace JobLogger
{
    class Program
    {
        static void Main()
        {
            JobStatusService service = new JobStatusService();
            service.Initialize();
        }
    }
}
