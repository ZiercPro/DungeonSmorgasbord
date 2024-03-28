using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.FeedBack
{
    
    public class FlashWhiteFeedBack : MonoBehaviour {
        [SerializeField] private Material whiteM;//被攻击 
        [SerializeField] private Material defualt;//默认
        [SerializeField] private SpriteRenderer characterS;

        [SerializeField] private float maintainTime = 0.1f;
        public void Flash() {
            characterS.material = whiteM;
            StartCoroutine(FlashTimer());
        }
        IEnumerator FlashTimer() {
            yield return new WaitForSeconds(maintainTime);
            characterS.material = defualt;
        }
    }

}