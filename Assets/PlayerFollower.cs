using UnityEngine;
[RequireComponent (typeof(Cursor))]
public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject target_;
    [SerializeField, Range(0.0f, 1.0f)]
    float interpolatedValue_ = 0.5f;
    private Cursor cursor_;
    private Vector3 offset_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cursor_ = GetComponent<Cursor>();
        offset_ = transform.position - target_.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = cursor_.GetRaycastHit().point;
        Vector3 targetPosition = target_.transform.position;
        Vector3 lookAt = Vector3.Lerp(targetPosition, mousePosition, interpolatedValue_);
        transform.position = lookAt + offset_;
    }
}
