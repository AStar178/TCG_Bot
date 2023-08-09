using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Roket : MonoBehaviour {

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float Speed;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float ForseAmount = 100;
    bool wo;
    float w;
    private async void Awake() {
        w = RotateSpeed;
        wo = false;
        RotateSpeed = 1;
        await Task.Delay(500);
        RotateSpeed = w;
        wo = true;
    }
    GameObject Target;
    private void Update() {
        if (wo == false)
        {
            rb.velocity = transform.forward * Speed;
            return;
        }
        rb.velocity = transform.forward * Speed;
        if (Target == null)
        {
           var targets = Physics.OverlapSphere(transform.position , 10000 , Player.Current.PlayerTargetSystem.EnemyLayer).OrderBy(s => Random.value).FirstOrDefault();
           if (targets != null)
                Target = targets.gameObject;
        }   
        if (Target == null)
            return;
        
        Vector3 dir = (Target.transform.position - transform.position).normalized;

        Quaternion wow = Quaternion.LookRotation(dir);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, wow, RotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.TryGetComponent<EnemyHp>(out var Hp))
        {
            Hp.rb.AddForce( transform.forward * ForseAmount/3 , ForceMode.Impulse );
            FireWork.Current.OnHit(Hp);
            Destroy(gameObject);
        }

    }

}