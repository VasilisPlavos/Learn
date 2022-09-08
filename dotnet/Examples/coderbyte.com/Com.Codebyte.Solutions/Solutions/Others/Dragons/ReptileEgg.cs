using System;
using Com.Coderbyte.Solutions.Solutions.Codility;

namespace Com.Coderbyte.Solutions.Solutions.Others.Dragons
{
    public class ReptileEgg
    {
        public ReptileEgg(Func<IReptile> createReptile)
        {
            var monster = createReptile;
        }

        public IReptile Hatch()
        {
            var egg = this.GetType();
            return new FireDragon();
        }
    }
}