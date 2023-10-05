namespace Weapons {
    public interface IWeapon
    {
        void Fire(int layer);
        void Reload();
        string GetWeaponName();
    }
}