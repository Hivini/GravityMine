using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeh : MonoBehaviour
{
    public float radius, maxPower,x,y,r;
    public float acceleration;
    float ti,w, dt, theta;
    Transform t;
    Vector3 origin, relative;
    bool accelerator, inicio;
    Coroutine coroutine;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()

    {
        inicio = false;
        rb = GetComponent<Rigidbody>();
        accelerator = false;
        ti = 0;
        t = this.transform;
        radius = 3;
        maxPower = 35;
        acceleration = 0.05f;
        origin = new Vector3(0, radius, 0);
        w= 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        inicio = Input.GetKeyDown(KeyCode.Space);

            if (accelerator) // EL ACELERADOR ESTA PRENDIDO
            {
                if (inicio) // APAGA EL ACELERADOR
                {
                rb.WakeUp();
                r = Mathf.Sqrt(x * x + y * y);
                rb.velocity = 7 * (w * r) * (new Vector3(-y / r, x / r, 0));
                accelerator = false;
                inicio = false;
                }
                else // MANTIENE ACELERADOR PRENDIDO.
                {
                    ti += Time.deltaTime;
                    x = Mathf.Cos(ti * w + theta);
                    y = Mathf.Sin(ti * w + theta);
                    t.position = radius * (new Vector3(x, y, 0)) + origin;
                    w += acceleration;
                    w = (w > maxPower ? maxPower : w);
                }
            }
            else
            {//EL ACELERADOR ESTA APAGADO.
                if (inicio) //ENCIENDE ACELERADOR.
                {
                    origin = (rb.velocity.magnitude < 0.001) ?
                        (new Vector3(t.position.x, t.position.y + radius, 0))
                        : (new Vector3(t.position.x + (-radius * rb.velocity.normalized.y),
                                    t.position.y + (radius * rb.velocity.normalized.x), 0));
                    theta = (rb.velocity.magnitude < 0.001) ?
                        (-Mathf.PI / 2)
                        : ((float)(System.Math.Atan2(x, y)));
                    if (theta < 0) theta += 2 * Mathf.PI;

                    ti = 0;
                    rb.velocity = Vector3.zero;
                    rb.Sleep();
                    inicio = false;
                    accelerator = true;
                }
            }   
    }
}

