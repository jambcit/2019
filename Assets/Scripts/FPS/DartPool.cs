using System.Collections.Generic;
using UnityEngine;

namespace Home.Fps
{
    [System.Serializable]
    public class DartPool
    {
        private GameObject dartPrefab;
        private int dartCount;
        private List<GameObject> dartPool = new List<GameObject>();
        private int dartIndex = 0;

        public DartPool(GameObject dartPrefab, int dartCount)
        {
            this.dartPrefab = dartPrefab;
            this.dartCount = dartCount;
        }

        public void Start()
        {
            for (int i = 0; i < dartCount; i++)
            {
                GameObject dart = Object.Instantiate(dartPrefab);
                dart.SetActive(false);
                dartPool.Add(dart);
            }
        }

        public void ShootNextDart(int viewId, Vector3 position, Quaternion rotation)
        {
            GameObject dart = dartPool[dartIndex];
            dartIndex++;
            dartIndex = dartIndex % dartCount;
            dart.SetActive(true);
            dart.transform.position = position;
            dart.transform.rotation = rotation;
            Dart d = dart.GetComponent<Dart>();
            d.Shoot(viewId);
        }
    }
}
