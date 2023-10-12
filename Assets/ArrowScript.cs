using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public Image arrowImage;

    void FixedUpdate()
    {
        if (target != null && player != null)
        {
            Vector3 direction = target.position - player.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
