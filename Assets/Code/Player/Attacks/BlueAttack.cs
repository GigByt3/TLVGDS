public class BlueAttack : BasePlayerAttack
{
    //Snare

    public override void ServerAttackMethod(Vector3 aimingVector)
    {
        Instantiate(snare, aimingVector, Quaternion.identity);      
    }
}
