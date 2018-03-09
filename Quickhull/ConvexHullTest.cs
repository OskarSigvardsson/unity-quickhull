using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GK {
	public class ConvexHullTest : MonoBehaviour {

		public Text DebugText;

		IEnumerator Start() {
			var calc = new ConvexHullCalculator();
			var verts = new List<Vector3>();
			var tris = new List<int>();
			var normals = new List<Vector3>();
			var points = new List<Vector3>();
			var mf = GetComponent<MeshFilter>();
			var mesh = new Mesh();

			mf.sharedMesh = mesh;

			while(true) {
				points.Clear();

				for (int i = 0; i < 500; i++) {
					points.Add(new Vector3(
							Random.value,
							Random.value,
							Random.value));

				}

				var stopwatch = System.Diagnostics.Stopwatch.StartNew();

				calc.GenerateHull(points, true, ref verts, ref tris, ref normals);

				stopwatch.Stop();

				DebugText.text = stopwatch.Elapsed.TotalMilliseconds.ToString("n2");

				mesh.Clear();

				mesh.SetVertices(verts);
				mesh.SetTriangles(tris, 0);
				mesh.SetNormals(normals);

				yield return new WaitForSeconds(1.0f);

			}
		}
	}
}
