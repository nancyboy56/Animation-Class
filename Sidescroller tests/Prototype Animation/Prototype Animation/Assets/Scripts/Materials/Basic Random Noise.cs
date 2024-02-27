using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRandomNoise : MonoBehaviour
{
    [SerializeField]
    private float fps = 30;

    private float frameLength = 0;

    private float currentLength = 0;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Vector4 randomRange = new Vector4();

    private Vector4 newOffset = new Vector4();

    // Start is called before the first frame update
    void Start()
    {
        frameLength = 1 / fps;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLength += Time.deltaTime;

        //print("Frame Length: "+ currentLength);
        
        if(currentLength >= frameLength)
        {
            newOffset = new Vector4(Random.Range(randomRange.x, randomRange.z),
                Random.Range(randomRange.y, randomRange.w));
            spriteRenderer.material.SetVector("_NoiseOffset", newOffset);
            currentLength = 0;
            print("change noise! " + newOffset);
        }
        
    }
}
