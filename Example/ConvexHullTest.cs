using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GK {
	public class ConvexHullTest : MonoBehaviour {

		public GameObject RockPrefab;
		
		IEnumerator Start() {
			var calc = new ConvexHullCalculator();
			var verts = new List<Vector3>();
			var tris = new List<int>();
			var normals = new List<Vector3>();
			var points = new List<Vector3>();

			while(true) {
				points.Clear();

				for (int i = 0; i < 200; i++) {
					points.Add(Random.insideUnitSphere);
				}

				calc.GenerateHull(points, true, ref verts, ref tris, ref normals);

				var rock = Instantiate(RockPrefab);

				rock.transform.SetParent(transform, false);
				rock.transform.localPosition = Vector3.zero;
				rock.transform.localRotation = Quaternion.identity;
				rock.transform.localScale = Vector3.one;
				
				var mesh = new Mesh();
				mesh.SetVertices(verts);
				mesh.SetTriangles(tris, 0);
				mesh.SetNormals(normals);

				rock.GetComponent<MeshFilter>().sharedMesh = mesh;
				rock.GetComponent<MeshCollider>().sharedMesh = mesh;
				
				yield return new WaitForSeconds(0.5f);
			}
		}
	}
}
