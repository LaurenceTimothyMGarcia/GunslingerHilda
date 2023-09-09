using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnknownsCrosshairs
{
    public class Crosshair : MonoBehaviour
    {
        //the images that make up the sides of the crosshair
        public RawImage up;
        public RawImage down;
        public RawImage left;
        public RawImage right;

        //remember the default positions so crosshair can return to the center
        Vector3 upDefaultPos;
        Vector3 downDefaultPos;
        Vector3 leftDefaultPos;
        Vector3 rightDefaultPos;

        float returnToCenterSpeed;

        private void Start()
        {
            //remember the default positions so crosshair can return to the center
            upDefaultPos = up.transform.position;
            downDefaultPos = down.transform.position;
            leftDefaultPos = left.transform.position;
            rightDefaultPos = right.transform.position;
        }

        void LateUpdate()
        {
            ShrinkCrosshairToNormal();
        }

        void ShrinkCrosshairToNormal() //every frame do the opposide of Expand to return the crosshair back to the middle
        {
            if (up.transform.position.y > upDefaultPos.y)
                up.transform.position = new Vector3(up.transform.position.x, up.transform.position.y - returnToCenterSpeed, up.transform.position.z);

            if (down.transform.position.y < downDefaultPos.y)
                down.transform.position = new Vector3(down.transform.position.x, down.transform.position.y + returnToCenterSpeed, down.transform.position.z);

            if (left.transform.position.x < leftDefaultPos.x)
                left.transform.position = new Vector3(left.transform.position.x + returnToCenterSpeed, left.transform.position.y, left.transform.position.z);

            if (right.transform.position.x > rightDefaultPos.x)
                right.transform.position = new Vector3(right.transform.position.x - returnToCenterSpeed, right.transform.position.y, right.transform.position.z);
        }

        public void Expand(float expandAmount) // take each side of the crosshair and move it outwards by adding or subtracting "expandAmount" to it's position
        {
            up.transform.position = new Vector3(up.transform.position.x, up.transform.position.y + expandAmount, up.transform.position.z);
            down.transform.position = new Vector3(down.transform.position.x, down.transform.position.y - expandAmount, down.transform.position.z);
            left.transform.position = new Vector3(left.transform.position.x - expandAmount, left.transform.position.y, left.transform.position.z);
            right.transform.position = new Vector3(right.transform.position.x + expandAmount, right.transform.position.y, right.transform.position.z);
        }


        public void SetShrinkSpeed(float shrinkSpeed) //this allows the gun script to tell us how fast we want to return crosshair sides to center
        {
            returnToCenterSpeed = shrinkSpeed;
        }


    }
}
