namespace SpaceInvader.Game
{
    internal interface IDestroyable
    {
        bool HasBeenDestroyed();
        void DealDamage(IDamageDealer dealer);
    }
}