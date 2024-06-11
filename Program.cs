using ParticleSystem.Models;

namespace ParticleSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WaterWindow window = new WaterWindow(800, 600, "Water Simulation"))
            {
                window.Run();
            }
        }
    }
}
