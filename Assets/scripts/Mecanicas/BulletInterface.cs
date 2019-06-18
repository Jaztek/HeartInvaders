using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BulletInterface {
    // Start is called before the first frame update
    void destroy();

    void onTriggerEnter(Collider2D other);
}
