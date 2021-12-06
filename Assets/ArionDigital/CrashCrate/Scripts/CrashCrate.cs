namespace ArionDigital
{
    using UnityEngine;

    public class CrashCrate : MonoBehaviour
    {
        [Header("Whole Create")]
        public MeshRenderer wholeCrate;
        public BoxCollider boxCollider;
        [Header("Fractured Create")]
        public GameObject littlePieces;

        public Rigidbody[] cratePieces;

        

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.gameObject.name);
            if (!other.gameObject.CompareTag("biteScanner"))
            {
                


                littlePieces.SetActive(true);
                foreach (Rigidbody r in cratePieces)
                {
                    r.AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 10f), Random.Range(15f, 30f)), ForceMode.Impulse);
                }

                boxCollider.enabled = false;
                wholeCrate.enabled = false;
                //crashAudioClip.Play();
            }

        }

    }
}