namespace ParticleSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new Models.WaterWindow())
            {
                window.Run();
            }
        }
    }
}
