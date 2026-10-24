
using UnityEngine;

public class EndlessPlatform : MonoBehaviour
{
    public Transform player;
    public float platformLength = 10f;
    public int platformCount = 3;

    private Transform[] platforms;
    private float nextZ;

    void Start()
    {
        platforms = new Transform[platformCount];
        platforms[0] = transform;

        for (int i = 1; i < platformCount; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );

            cube.transform.localScale = transform.localScale;
            platforms[i] = cube.transform;
        }

        for (int i = 0; i < platformCount; i++)
        {
            platforms[i].position =
                new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z + i * platformLength
                );
        }

        nextZ = transform.position.z
                + platformCount * platformLength;
    }

    void Update()
    {
        if (player == null) return;

        for (int i = 0; i < platformCount; i++)
        {
            if (player.position.z >
                platforms[i].position.z + platformLength)
            {
                platforms[i].position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    nextZ
                );

                nextZ += platformLength;
            }
        }
    }
}
