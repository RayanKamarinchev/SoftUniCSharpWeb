using Coffee.Models;

namespace Coffee.Services
{
    public class Orderservice
    {
        private readonly string[] status = new string[]
        {
            "Grinding beans",
            "Steaming milk",
            "Quality control",
            "delivering...",
            "Picked up"
        };

        private readonly Random random;
        private readonly List<int> indexes;

        public Orderservice()
        {
            random = new Random();
            indexes = new List<int>();
        }

        public int NewOrder()
        {
            indexes.Add(0);
            return indexes.Count;
        }

        public CheckResult GetUpdate(int orderId)
        {
            Thread.Sleep(1000);
            var index = indexes[orderId - 1];
            if (random.Next(0,4) == 2)
            {
                if (status.Length > this.indexes[orderId-1])
                {
                    var result = new CheckResult()
                    {
                        New = true,
                        Update = status[index],
                        Finished = status.Length - 1 == index
                    };
                    indexes[orderId - 1]++;
                    return result;
                }
            }
            return new CheckResult(){New = false};
        }
    }
}
