public class charecterIdentity(seed) : NetworkBehavior
{
    Random rnd = new Random(seed);
    int id = rnd.Next(10000, 100000);
    int preveived_id = rnd.Next(12);
}