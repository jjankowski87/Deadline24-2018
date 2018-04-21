using System.Collections.Generic;

namespace Deadline24.Core.Visualization
{
    public class GameState
    {
        public IList<CarState> Cars { get; set; }


    }

    public class CarState
    {
        public int Id { get; set; }

        public int Fuel { get; set; }

        //public 
    }

    //public class LimitedQueue<T> : Qi
}
