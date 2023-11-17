namespace Enemies {
    public interface IEnemy {
        void setHealth(float health);
        void addDamage(float dmg);
        void TakeDamage(float damage);

        string getname();
    }
}