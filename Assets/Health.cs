using UnityEngine;
[RequireComponent (typeof(Collider))]
public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth_ = 3;
    private float currentHealth_;
    protected Collider collider_;
    private void Awake()
    {
        currentHealth_ = maxHealth_;
        collider_ = GetComponent<Collider>();
    }

    public void Damage(float point)
    {
        currentHealth_ -= point;
        if(currentHealth_ > 0) { return; }
        Death();
    }
    private void Death()
    {
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
