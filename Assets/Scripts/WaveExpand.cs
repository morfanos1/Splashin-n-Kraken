using UnityEngine;
using System.Collections;

public class WaveExpand : MonoBehaviour {

    public ParticleSystem ps;
    public CircleCollider2D cc;

    public float startRadius;
    public float endRadius;
    public float expandSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ExpandAndKill(startRadius, endRadius));
    }

    IEnumerator ExpandAndKill(float startRad, float endRad)
    {
        for(float i = startRad; i < endRad; i = i + expandSpeed*Time.deltaTime)
        {
            ParticleSystem.ShapeModule shapeModule = ps.shape;
            shapeModule.radius = i;
            //ps.shape.radius = i;
            cc.radius = i;
            yield return null;
        }
        Destroy(gameObject);
    }
}
