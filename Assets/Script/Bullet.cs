using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] [Range(0f, 840f)] float rotateSpeed = 340f;
    [SerializeField] float flySpeed = 2f;
    Transform bulletSpriteTransform;
    [SerializeField] int damage = 120;

    // Start is called before the first frame update
    void Start()
    {
        FindBulletSprite();
    }

    // Update is called once per frame
    void Update()
    {
        float rotateStep = Time.deltaTime * rotateSpeed;
        bulletSpriteTransform.Rotate(new Vector3(0,0,-rotateStep));
        float flyStep = Time.deltaTime * flySpeed * (-1);
        transform.Translate(Vector2.left  * flyStep);
    }

    private void FindBulletSprite()
    {
        bulletSpriteTransform = transform.Find("BulletSprite");
        if (bulletSpriteTransform == null)
        {
            Debug.LogError("Please, add child obj to shooter, and name it BulletSprite. This obj must contain SpriteRender component");
        }
    }

    public int GetDamege()
    {
        return damage;
    }

    public void DestroyBullet()
    {

        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject.Destroy(gameObject);
    }
}
