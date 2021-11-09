namespace lab_code_
{
    class Program
    {
        static void Main(string[] args)
        {
            View view = new View();
            Controller controller = new Controller();
            controller.CommandProcessing(view.Input());
        }
    }
}