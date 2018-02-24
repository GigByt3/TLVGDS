public class BlueAttack : BasePlayerAttack
{
    //Snare

    public override ServerAttackMethod(Vector3 aimingVector)
    {
        Instantiate(snare, aimingVector, Quaternion.identity)        
    }
}
