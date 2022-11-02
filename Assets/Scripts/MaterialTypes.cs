using UnityEngine;

public class MaterialTypes : MonoBehaviour {

    public MaterialTypeEnum TypeOfMaterial = MaterialTypeEnum.Plaster;

    [System.Serializable]
    public enum MaterialTypeEnum
    {
        Plaster,
        Metall,
        Folliage,
        Enemy,
        Rock,
        Wood,
        Brick,
        Concrete,
        Dirt,
        Glass,
        Water
    }
}
