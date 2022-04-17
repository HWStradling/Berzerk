using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;
        private bool allowTeleport = false;
        [SerializeField] private int destinationBuildIndex;

        private Color curColor;
        private Color targetColor;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                targetColor = new Color(1, 1, 1, 1);
                allowTeleport = true;
                StartCoroutine(Teleport());  
            }
            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                targetColor = new Color(1, 1, 1, 0);
                allowTeleport = false;
            }
        }

        IEnumerator Teleport()
        {
            yield return new WaitForSeconds(3);
            if (allowTeleport)
            {
                SceneManager.LoadScene(destinationBuildIndex);
            }
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }
    }
}
