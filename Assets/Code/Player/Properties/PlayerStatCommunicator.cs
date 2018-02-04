public class Player 
{
    Random rnd = new Random();
    int x = rnd(-10, 10);
    int y = rnd(-10, 10);
    int class = rnd(12);
    int precivedId = rnd(4);
    public delegate int statsToServer(stats);
    public event EventHandler Player(stats);
    int[] playerInfo = new int[x, y, NetworkPlayer.ipAddress, 100, class, precivedId];
    /// Structure: [x, y, id, hp, class#, precievedId#]
    /// Class# and precievedId# corespond to classes on a server based array

    void Update() {
        protected virtual void UpdatePlayer(playerInfo)
        {
            EventHandler handler = Player;
            handler(playerInfo);
        };
    };
};