using UnityEngine;
[RequireComponent (typeof(LineRenderer))]
public class RayBullet : MonoBehaviour
{
    [SerializeField]
    private float lifeTime_ = 0.5f;
    private float timer_;
    private LineRenderer line_;
    private Vector3 beginPosition_;
    private Vector3 endPosition_;
    private void Awake()
    {
        line_ = GetComponent<LineRenderer>();
        timer_ = lifeTime_;
    }
    public void SetPositions(Vector3 beginPosition, Vector3 endPosition)
    {
        beginPosition_ = beginPosition;
        endPosition_ = endPosition;
        line_.SetPositions(new Vector3[]{
            beginPosition_,
            endPosition_
        });
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer_ -= Time.deltaTime;
        if (timer_ <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
