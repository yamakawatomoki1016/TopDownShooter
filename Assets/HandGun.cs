using UnityEngine;

public class HandGun : GunBase
{
    bool fired_ = false;
    public override void OffTrigger()
    {
        fired_ = false;
    }
    public override void OnTrigger()
    {
        if(shotTimer_ > 0) { return; }
        if (fired_) { return; }
        fired_ = true;
        shotTimer_ = fireRate_;
        Ray ray = new Ray(muzzleTransform_.position,
            muzzleTransform_.forward);
        RaycastHit raycastHit;
        int layerMask = ~LayerMask.GetMask(
            new string[] { "Item" });
        float rayLength = 100;
        Vector3 endPoint = muzzleTransform_.position + muzzleTransform_.forward * rayLength;
        if(Physics.Raycast(ray,out raycastHit,rayLength,layerMask)) {
            endPoint = raycastHit.point;
            Health healthComponent;
            bool hasHealth = raycastHit.collider.TryGetComponent(out healthComponent);
            if (hasHealth)
            {
                healthComponent.Damage(power_);
            }
        }
        GameObject bulletObject = Instantiate(bulletPrefab_.gameObject, muzzleTransform_.position, muzzleTransform_.rotation);
        RayBullet bullet = bulletObject.GetComponent<RayBullet>();
        bullet.SetPositions(muzzleTransform_.position,endPoint);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
}
