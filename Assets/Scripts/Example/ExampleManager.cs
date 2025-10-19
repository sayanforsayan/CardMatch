using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

namespace Sayan.CardGame
{
    public class ExampleManager : MonoBehaviour
    {
        [SerializeField] private float xDistanceForObstacle, xDistanceForCollectable;
        private float yDistance;
        [SerializeField] private GameObject obstacle, collectable;
        [SerializeField] private Transform placeHolderObstacle, placeHolderCollectable, createPos;

        void Start()
        {
            InvokeRepeating(nameof(CreateObstable), 1, 2);
            InvokeRepeating(nameof(CreateCollectable), 1, 2);
        }

        void CreateObstable()
        {
            float xRandomDistance = UnityEngine.Random.Range(1, 3);
            yDistance = UnityEngine.Random.Range(1, 3);
            Vector3 pos = new Vector3(createPos.position.x + xDistanceForObstacle, createPos.position.y + yDistance);
            Instantiate(obstacle, pos, quaternion.identity);
            xDistanceForObstacle += xRandomDistance;
        }

        void CreateCollectable()
        {
            float xRandomDistance = UnityEngine.Random.Range(5, 8);
            yDistance = UnityEngine.Random.Range(1, 3);
            Vector3 pos = new Vector3(createPos.position.x + xDistanceForCollectable, createPos.position.y + yDistance);
            Instantiate(collectable, pos, quaternion.identity);
            xDistanceForCollectable += xRandomDistance;
        }
    }
}
