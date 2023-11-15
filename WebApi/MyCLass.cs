namespace WebApi
{
    public class MyCLass
    {
        public void Abcd(AbstractTest tEst)
        {
            var a = new Test();
            a.CreateName();

            var car = new Car();

            car.IncreaseSpeed();
            car.DecreaseSpeed();

        }
    }


    public interface ITEst
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public static class Extensions { 
        public static string CreateName(this ITEst tEst)
        {
            return $"asdasd";
        }

        public static void DecreaseSpeed(this Car car)
        {
            //tood implent
        }
    }

    public interface IEngine
    {
        void IncreaseSpeed();
    }

    public class Car {

        private IEngine engine;


        public Car()
        {
            //engine = new IEngine();
        }

        public void IncreaseSpeed()
        {
            engine.IncreaseSpeed();
        }
    }





    public abstract class AbstractTest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MiddleName { get; set; }
        public int Age { get; set; }

        public virtual string CreateName(){

            Console.WriteLine("Log abcd");
            return CreateFullName();
        }

        protected abstract string CreateFullName();
    }


    public class Test : AbstractTest
    {
        protected override string CreateFullName() => $"{FirstName} {MiddleName} {LastName} with age {Age}";
    }

}

