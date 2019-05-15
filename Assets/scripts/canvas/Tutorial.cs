using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	void OnEnable()
    {
        StartCoroutine("destroySelf");
    }

	 IEnumerator destroySelf()
    {
        for (; ; )
        {
            //este yield indica  cada cuando se va a llamar a la corrutina, en este caso cada 0.1 segundos.
            yield return new WaitForSeconds(3f);
			this.gameObject.SetActive(false);
			break;
        }

    }
}
