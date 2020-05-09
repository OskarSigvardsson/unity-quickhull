/**
 * Copyright 2019 Oskar Sigvardsson
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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
