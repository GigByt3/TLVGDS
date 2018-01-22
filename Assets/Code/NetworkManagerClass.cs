public class CharecterMananager : NetworkBehavior
{
    int[][] playerInformation = new int[][]
    for (int players = 0; players < 8; players++)
    {
        playerInformation[0][players] = 100;
        CharecterIdentity id = new CharecterIdentity();
        playerInformation[1][players] = CharecterIdentity.createIdentity(players*100f, true);
        playerInformation[2][players] = CharecterIdentity.createIdentity(0f, false)
        ///<cont>
        /// Any further charecter information can be added in the same format here.
        ///</cont>
    }
}
