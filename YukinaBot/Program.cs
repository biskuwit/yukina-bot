namespace YukinaBot
{
    public class Program
    {
        public static void Main(string[] args) => new Client().MainAsync().GetAwaiter().GetResult();
    }
}