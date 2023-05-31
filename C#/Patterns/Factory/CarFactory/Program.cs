namespace CarFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Building two cars");

            CarFactory carFactory = new();

            Car firstCar = carFactory.GetCarType("Cruise");

            Car secondCar = carFactory.GetCarType("Mustang");

            firstCar.Drive();

            secondCar.Drive();
        }
    }
}