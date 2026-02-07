using UnityEngine;
[RequireComponent (typeof(Camera))]
public class Cursor : MonoBehaviour
{
    private const string FLOOR_LAYER_NAME = "Floor";
    private bool isHit_;
    private Ray ray_;
    private RaycastHit raycastHit_;
    private Camera camera_;
    private void Start()
    {
        camera_ = GetComponent<Camera>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        ray_ = camera_.ScreenPointToRay(Input.mousePosition);
        isHit_ = Physics.Raycast(ray_, out raycastHit_, 1000, LayerMask.GetMask(FLOOR_LAYER_NAME));
    }
    public bool GetIsHit() { return isHit_; }
    public Ray GetRay() { return ray_; }
    public RaycastHit GetRaycastHit() { return raycastHit_; }
}
