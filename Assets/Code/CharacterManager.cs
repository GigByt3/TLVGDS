using UnityEngine;

public class CharecterMananager : NetworkBehavior
{
    int[][] playerInformation = new int[][]

    public CharecterMananager()
    {
        for (int players = 0; players < 8; players++)
        {
            playerInformation[0][players] = 100;
            CharecterIdentity id = new CharecterClass();
            playerInformation[1][players] = CharecterIdentity.createIdentity(true);
            playerInformation[2][players] = CharecterIdentity.createIdentity(false)
            ///<cont>
            /// Any further charecter information can be added in the same format here.
            ///</cont>
        }
    }
}