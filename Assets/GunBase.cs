using System.Threading;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    [SerializeField]
    protected RayBullet bulletPrefab_;
    [SerializeField]
    protected Transform muzzleTransform_;
    [SerializeField]
    protected float fireRate_ = 0;
    protected float shotTimer_ = 0;
    [SerializeField]
    protected float power_ = 1;
    [SerializeField]
    private float itemRotateSpeedDeg_ = 90.0f;

    public abstract void OnTrigger();
    public abstract void OffTrigger();

    private void ItemRotate()
    {
        transform.RotateAround(
            transform.position,
            Vector3.up,
            itemRotateSpeedDeg_ * Time.deltaTime
        );
    }

    public bool GetIsAlone()
    {
        return transform.parent == null;
    }

    public virtual void Update()
    {
        if (GetIsAlone()) { ItemRotate(); }
        if (shotTimer_ <= 0) { return; }
        shotTimer_ -= Time.deltaTime;
    }
}
