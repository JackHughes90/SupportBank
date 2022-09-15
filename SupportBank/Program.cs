namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SupportBank, making people pay up since 2022!");
            Console.WriteLine("========================================================");
            FileReader cvsReader = new FileReader();

            FileReader.ReadFile();

        }
    }
}