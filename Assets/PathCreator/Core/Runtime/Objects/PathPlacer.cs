using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

namespace PathCreation.Examples {

    public class PathPlacer : PathSceneTool {

        public GameObject carPrefab;
        public GameObject holder;
        [HideInInspector] public Vector3 heightOffset = new Vector3(0, 0.15f, 0);
        [HideInInspector] public Quaternion orientation = Quaternion.Euler(0, 0, 90);
        public float spacing = 3;
        const float minSpacing = 1.1f;

        public void Generate () {
            if (pathCreator != null && carPrefab != null && holder != null) {
                DestroyObjects ();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing); // returns largest value
                float dst = 0f;

                while (dst < path.length) {
                    Vector3 point = path.GetPointAtDistance (dst);
                    Quaternion rot = path.GetRotationAtDistance (dst);
                    Instantiate(carPrefab, point + heightOffset, rot * orientation, holder.transform);
                    dst += spacing;
                }
            }
        }

        void DestroyObjects () {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) {
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
            }
        }

        protected override void PathUpdated () {
            if (pathCreator != null) {
                Generate ();
            }
        }
    }
}