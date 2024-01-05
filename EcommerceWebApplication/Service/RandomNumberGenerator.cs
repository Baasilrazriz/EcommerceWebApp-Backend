namespace EcommerceWebApplication.Service
{
    public class RandomNumberGenerator
    {
        private Random _random = new Random();

        public int GenerateSixDigitNumber()
        {
            return _random.Next(100000, 999999);
        }
    }
}
