namespace Backend.services
{
    public class RandomService : IRandomService
    {
        private readonly int _value;

        public int Value => _value;
        
        public RandomService()
        {
            _value = new Random().Next(1000);
        }
    }
}

