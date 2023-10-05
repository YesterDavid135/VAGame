using UnityEngine;
using Weapons;

public class Shotgun : MonoBehaviour, IWeapon {
    public GameObject bullet;

    public Transform firePoint;

    public float fireForce = 20;
    public int numberOfBullets = 5;
    public float spreadAngle = 10;

    // Start is called before the first frame update

    public void Fire(int layer) {
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject projectile = ObjectPooler.current.GetPooledObject();
            if (projectile == null) continue;

            // Calculate the spread angle for this bullet
            float bulletAngle = spreadAngle * ((i + 0.5f) / numberOfBullets - 0.5f);

            // Calculate the rotation based on the spread angle
            Quaternion rotation = Quaternion.Euler(0, 0, bulletAngle) * firePoint.rotation;

            // Set the layer, position, and rotation
            projectile.layer = layer;
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = rotation;

            // Activate the projectile and apply force
            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody2D>().AddForce(rotation * Vector2.up * fireForce, ForceMode2D.Impulse);
        }
    }

    public void Reload()
    {
        // Implement shotgun reloading logic here
    }

    public string GetWeaponName()
    {
        return "Shotgun";
    }
}