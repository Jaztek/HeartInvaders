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
        yield return new WaitForSeconds(3f);
        for (float alpha = 1f; alpha > 0.4f; alpha = alpha - 0.03f)
        {
            GetComponent<CanvasGroup>().alpha = alpha;
            yield return new WaitForSeconds(0.08f);
        }
    }

}
