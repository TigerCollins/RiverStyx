using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxProjection : MonoBehaviour {

	public float scaleX = 1f;
    public float scaleY = 1f;

    void OnDrawGizmosSelected () {
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		Vector2[] uvs = new Vector2[vertices.Length];

		for (int i = 0; i < uvs.Length; i++) {
			if (Mathf.Abs (normals[i].x) > Mathf.Abs (normals[i].y) && Mathf.Abs (normals[i].x) > Mathf.Abs (normals[i].z)) {
				if (normals[i].x > 0) {
					uvs[i] = new Vector2 (transform.TransformPoint (vertices[i]).z * scaleX, transform.TransformPoint (vertices[i]).y * scaleY);
				} else {
					uvs[i] = new Vector2 (-transform.TransformPoint (vertices[i]).z * scaleX, transform.TransformPoint (vertices[i]).y * scaleY);
				}
			}

			if (Mathf.Abs (normals[i].y) > Mathf.Abs (normals[i].x) && Mathf.Abs (normals[i].y) > Mathf.Abs (normals[i].z)) {
				if (normals[i].y > 0) {
					uvs[i] = new Vector2 (-transform.TransformPoint (vertices[i]).x * scaleX, -transform.TransformPoint (vertices[i]).z * scaleY) ;
				} else {
					uvs[i] = new Vector2 (-transform.TransformPoint (vertices[i]).x * scaleX, transform.TransformPoint (vertices[i]).z * scaleY);
				}
			}

			if (Mathf.Abs (normals[i].z) > Mathf.Abs (normals[i].x) && Mathf.Abs (normals[i].z) > Mathf.Abs (normals[i].y)) {
				if (normals[i].z > 0) {
					uvs[i] = new Vector2 (-transform.TransformPoint (vertices[i]).x * scaleX, transform.TransformPoint (vertices[i]).y * scaleY);
				} else {
					uvs[i] = new Vector2 (-transform.TransformPoint (vertices[i]).x * scaleX, -transform.TransformPoint (vertices[i]).y * scaleY);
				}
			}
			mesh.uv = uvs;
			mesh.RecalculateNormals ();
		}
	}

	private void Awake () {
		DestroyImmediate (this);
	}
}
