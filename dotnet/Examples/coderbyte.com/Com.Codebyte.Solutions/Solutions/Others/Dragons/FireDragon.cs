namespace Com.Coderbyte.Solutions.Solutions.Others.Dragons
{
    public class FireDragon : IReptile
    {
        public FireDragon() { }
        public ReptileEgg Lay()
        {
            var a = new ReptileEgg(() => new FireDragon());
            return a;
        }
    }
}