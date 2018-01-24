using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenSize : MonoBehaviour {

	public float orthographicSize;
	public float aspect;
	void Start()
	{
		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,
			-orthographicSize, orthographicSize,
			Camera.main.nearClipPlane, Camera.main.farClipPlane);
	}
}
