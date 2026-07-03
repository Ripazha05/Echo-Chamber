using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Flicker Settings")]
    [SerializeField] private float minIntensity = 10f;
    [SerializeField] private float maxIntensity = 18f;
    [SerializeField] private float flickerSpeed = 5f;

    [Header("Mode")]
    [SerializeField] private bool useSmoothFlicker = true; // true = pelan & halus, false = patah-patah kayak lampu rusak

    private Light lightSource;
    private float baseIntensity;
    private float noiseOffset;

    void Awake()
    {
        lightSource = GetComponent<Light>();
        baseIntensity = lightSource.intensity;
        noiseOffset = Random.Range(0f, 100f); // biar tiap lampu beda pola kalau dipakai di banyak object
    }

    void Update()
    {
        if (useSmoothFlicker)
        {
            // Flicker halus menggunakan Perlin Noise (natural, gak patah-patah)
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, noiseOffset);
            lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }
        else
        {
            // Flicker random patah-patah (kesan monitor/lampu rusak)
            if (Random.value < 0.05f) // 5% chance tiap frame buat "kedip"
            {
                lightSource.intensity = Random.Range(minIntensity, maxIntensity);
            }
        }
    }
}