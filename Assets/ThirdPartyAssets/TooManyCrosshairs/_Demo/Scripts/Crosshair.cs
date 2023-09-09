using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TooManyCrosshairs
{
    public class Crosshair : MonoBehaviour
    {

        public RawImage dot;  //put the dot image here
        public RawImage inner;  //put the inner crosshair/ring here
        public RawImage expanding; //the image that is expanded when the gun shoots
        public Image reload; // the image for the reload meter

        //alt versions to make crosshair look animated
        public Texture altDot;
        public Texture altInner;
        public Texture altExpanding;

        float reloadSpeed;  // how fast the gun reloads in seconds
        float shrinkSpeed; // how fast the recoil settles
        float crosshairMaxScale; //maximum size of the expanding crosshair

        bool isReloading; //used to determine whether we are already reloading (so that we have to finish reloading before we reload again).
        bool isShrinking; //used to make sure crosshair returns to normal size at the right speed;

        Vector3 crosshairOriginalScale; //stores the default crosshair size
        Color defaultColor; //used to store the initial crosshair color

        //used to store the initial textures (so they can be returned after the alt crosshair is shown)
        Texture defaultDot;
        Texture defaultInner;
        Texture defaultExpanding;

        void Start()
        {
            //turn off the reload image at the start of the game
            reload.enabled = false;

            //remember the current size of the crosshair, current textures and current color, so we can expand/shrink it back to normal size, default textures and default tint color
            this.crosshairOriginalScale = expanding.rectTransform.localScale;
            this.defaultColor = dot.color;
            this.defaultDot = dot.texture;
            this.defaultInner = inner.texture;
            this.defaultExpanding = expanding.texture;
        }

        public void ShowAlternates() //switch to alt textures
        {
            dot.texture = altDot;
            inner.texture = altInner;
            expanding.texture = altExpanding;
        }

        public void HideAlternates() //switch back to original textures
        {
            dot.texture = defaultDot;
            inner.texture = defaultInner;
            expanding.texture = defaultExpanding;
        }

        public void EnableTint(Color newTint) //switch to another color (set in the GUN object)
        {
            dot.color = newTint;
        }

        public void DisableTint() //switch back to original tint/color
        {
            dot.color = this.defaultColor;
        }

        //used by the GUN script to begin reload
        public void DoReload()
        {
            //if we are not already reloading, do the reloading routine.
            if (!isReloading)
                StartCoroutine(ReloadTheGun());
        }

        //used by the GUN to expand the crosshair
        public void ExpandCrosshair(float addScale)
        {
            //if the crosshair is still under the maximum expandable size, then make it expand more
            if (expanding.rectTransform.localScale.x < crosshairMaxScale)
            {
                expanding.rectTransform.localScale += new Vector3(addScale, addScale, addScale);
            }
            else
                expanding.rectTransform.localScale = new Vector3(crosshairMaxScale, crosshairMaxScale, crosshairMaxScale);


            if (!isShrinking)  // slowly shrink the crosshair back to normal, if it's not already started shrinking
                StartCoroutine(ShrinkCrosshair());
        }


        // these public functions are used to set up the crosshair to function based on what the GUN object settings are (so that you can set different behaviour per gun)
        public void SetReloadSpeed(float ReloadSpeed)
        {
            this.reloadSpeed = ReloadSpeed;
        }

        public void SetShrinkSpeed(float ShrinkSpeed)
        {
            this.shrinkSpeed = ShrinkSpeed;
        }

        public void SetMaxScale(float MaxScale)
        {
            this.crosshairMaxScale = MaxScale;
        }

        //shrinks the crosshair progressively over many frames - called by the ExpandCrosshair() function 
        IEnumerator ShrinkCrosshair()
        {
            isShrinking = true; //make sure we don't shrink while shrinking


            //while the crosshair is bigger than default size, keep shrinking
            do
            {
                expanding.rectTransform.localScale = new Vector3(expanding.rectTransform.localScale.x - Time.deltaTime * shrinkSpeed,
                                                                 expanding.rectTransform.localScale.y - Time.deltaTime * shrinkSpeed,
                                                                 expanding.rectTransform.localScale.z - Time.deltaTime * shrinkSpeed);
                yield return new WaitForEndOfFrame();
            }
            while (crosshairOriginalScale.x < expanding.rectTransform.localScale.x);

            isShrinking = false;
            yield return new WaitForEndOfFrame();
        }

        //called by the DoReload() to smoothly animate the reloading animation by changing the Image.fillAmount property over many frames
        IEnumerator ReloadTheGun()
        {
            //make sure our script knows we are now reloading, so we can't reload while already reloading
            isReloading = true;

            // reset the reload meter to 0, show it on the screen, and hide the regular crosshairs
            reload.fillAmount = 0;
            reload.enabled = true;
            inner.enabled = false;
            dot.enabled = false;
            expanding.enabled = false;

            //if the  reload meter isn't filled/finished, then keep filling until it is
            do
            {
                reload.fillAmount += Time.deltaTime * reloadSpeed;
                yield return new WaitForEndOfFrame();
            }
            while (reload.fillAmount < 1f);

            //hide the reload meter now that we are done reloading, and enable the regular crosshairs
            reload.enabled = false;
            inner.enabled = true;
            dot.enabled = true;
            expanding.enabled = true;


            // we're done reloading, setting this back to false, so we can reload next time
            isReloading = false;

            expanding.rectTransform.localScale = crosshairOriginalScale; //return the expanding element to the default size

            yield return new WaitForEndOfFrame();
        }
    }
}
