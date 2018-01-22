public class charecterIdentity : NetworkBehavior
{
    public void createIdentity()
    {
        Random rnd = new Random(seed, type);
        int id = rnd.Next(10000, 100000);
        if(type == true) {return(id)};
        int preveived_id = rnd.Next(12);
        return(preveived_id);    
    }
}