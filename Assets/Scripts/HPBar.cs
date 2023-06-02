using UnityEngine;

public class HPBar : MonoBehaviour
{
    private Renderer hpBarRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    private int hpValueID;

    private void Start()
    {
        hpBarRenderer = GetComponent<Renderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        hpValueID = Shader.PropertyToID("_HPValue");
    }


    public void SetHPPercentage(float hpPercentage)
    {
        materialPropertyBlock.SetFloat(hpValueID, hpPercentage);
        hpBarRenderer.SetPropertyBlock(materialPropertyBlock);
    }

}
