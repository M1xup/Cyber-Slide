using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour {
    private Spring spring;
    private List<Vector3> currentGrapplePositions;
    public DualH dh;
    public int quality;
    public float damper;
    public float strength;
    public float velocity;
    public float waveCount;
    public float waveHeight;
    public AnimationCurve affectCurve;
    
    void Awake() {
        dh = FindAnyObjectByType<DualH>();
        for (int i = 0; i < dh.amountOfSwingPoints; i++)
        {
            dh.lineRenderers[i] = GetComponent<LineRenderer>();
        }
        spring = new Spring();
        spring.SetTarget(0);
    }
    
    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    void DrawRope()
    {
        for (int grappleIndex = 0; grappleIndex < dh.amountOfSwingPoints; grappleIndex++)
        {
            //If not grappling, don't draw rope
            if (!dh.grapplesActive[grappleIndex] && !dh.swingsActive[grappleIndex])
            {
                // currentGrapplePositions[grappleIndex] = dh.gunTips[grappleIndex].position;
                spring.Reset();
                if (dh.lineRenderers[grappleIndex].positionCount > 0)
                    dh.lineRenderers[grappleIndex].positionCount = 0;
                return;
            }

            if (dh.lineRenderers[grappleIndex].positionCount == 0)
            {
                spring.SetVelocity(velocity);
                dh.lineRenderers[grappleIndex].positionCount = quality + 1;
            }

            spring.SetDamper(damper);
            spring.SetStrength(strength);
            spring.Update(Time.deltaTime);

            var grapplePoint = dh.swingPoints[grappleIndex];
            var gunTipPosition = dh.gunTips[grappleIndex].position;
            var up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

            dh.currentGrapplePositions[grappleIndex] = Vector3.Lerp(dh.currentGrapplePositions[grappleIndex], dh.swingPoints[grappleIndex], Time.deltaTime * 12f);

            for (var i = 0; i < quality + 1; i++)
            {
                var right = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.right;
                var delta = i / (float)quality;
                var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                             affectCurve.Evaluate(delta);
                // + 
                // right* waveHeight *Mathf.Cos(delta * waveCount * Mathf.PI) * spring.Value *
                // affectCurve.Evaluate(delta)

                if (grappleIndex > i || grappleIndex < i)
                {
                    continue;
                }
                dh.lineRenderers[grappleIndex].SetPosition(i, Vector3.Lerp(gunTipPosition, dh.currentGrapplePositions[grappleIndex], delta) + offset);
            }
        }
    }
}
