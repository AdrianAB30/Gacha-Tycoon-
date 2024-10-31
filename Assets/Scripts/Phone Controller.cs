using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    public AudioSource phoneRingtone;
    public AudioSource[] voicePhone;
    public float vibrationIntensity = 0.05f;
    public float ringDuration = 10f; 
    private Vector3 originalPosition;
    private bool isRinging = false; 
    private bool isHeld = false;

    [SerializeField] private GameObject handle;
    [SerializeField] private HandController handController;

    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(RingCoroutine());
    }

    void Update()
    {
        if (isHeld && handController != null)
        {
            handle.transform.position = handController.transform.position + new Vector3(0, 0f, 0.7f);
        }
    }

    private void Vibrate()
    {
        transform.position = originalPosition + (Vector3)Random.insideUnitCircle * vibrationIntensity;
    }

    public void OnMouseDown()
    {
        if (isRinging && handle != null && handle.CompareTag("Handle") && !isHeld)
        {
            phoneRingtone.Stop();
            isRinging = false;
            isHeld = true; 
            Interact();
        }
        else if (isHeld)
        {
            ReleasePhone();
        }
    }

    private void Interact()
    {
        int randomIndex = Random.Range(0, voicePhone.Length);
        voicePhone[randomIndex].Play();
        Debug.Log("El jugador ha interactuado con el teléfono.");
    }

    private void ReleasePhone()
    {
        isHeld = false; 
        transform.position = originalPosition;
        for (int i = 0; i < voicePhone.Length; ++i)
        {
            voicePhone[i].Stop();
        }
        Debug.Log("El teléfono ha sido soltado.");
    }

    private IEnumerator RingCoroutine()
    {
        while (true)
        {
            isRinging = true;
            phoneRingtone.Play();

            while (isRinging)
            {
                Vibrate(); 
                yield return null; 
            }

            phoneRingtone.Stop();

            yield return new WaitForSeconds(15f);
        }
    }
}
