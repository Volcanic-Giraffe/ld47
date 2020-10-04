using UnityEngine;

public class LaserProjectile : Projectile
{
    private const float MaxDistance = 20f;

    public Collider mainCollider;

    public float fadeTime = 1f;
    private float fadeTimer = 1f;
    public float ttlTimer = 0.2f;
    private bool fading;

    void Start()
    {
        fadeTimer = 0;
        EnlargeLaser();
    }

    private void Update()
    {
        if (fading)
        {
            fadeTimer -= Time.deltaTime;
            var scaleXY = transform.localScale.x; // the same as Y
            var reduced = fadeTimer / fadeTime * scaleXY;
            transform.localScale = new Vector3(reduced, reduced, transform.localScale.z);
            if (fadeTimer < 0)
            {
                base.DestroyProjectile();
            }
        }
        else
        {
            ttlTimer -= Time.deltaTime;
            if (ttlTimer <= 0) this.DestroyProjectile();
        }
    }

    void EnlargeLaser()
    {
        Ray ray = new Ray(transform.position, (transform.forward).normalized * MaxDistance);
        RaycastHit hit;

        int mask = LayerMask.GetMask("Wall", "Default");

        if (Physics.Raycast(ray, out hit, MaxDistance, mask))
        {
            var size = hit.distance;

            var scaleChange = Vector3.forward * size;

            transform.localScale += scaleChange;
        }
    }

    protected override void DoDamage(GameObject target)
    {
        base.DoDamage(target);
    }

    protected override void DestroyProjectile()
    {
        fading = true;
        mainCollider.enabled = false;
        fadeTimer = fadeTime;
    }
}
