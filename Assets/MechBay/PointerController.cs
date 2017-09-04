using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour {

	public GameObject clickPointerSprite;

	private float animationLength = 2.0f;

	public void showPointer(Vector2 position) {
		clickPointerSprite.transform.position = position;
		StartCoroutine(ActivationRoutine());
	}

	private IEnumerator ActivationRoutine()	{
		clickPointerSprite.SetActive(true);

		yield return new WaitForSeconds(animationLength);

		clickPointerSprite.SetActive(false);
	}
}
