using UnityEngine;

namespace Building
{
    public class BuildManager : MonoBehaviour
    {
        
        [SerializeField] private Material successBuildMaterial;
        [SerializeField] private Material failBuildMaterial;
        [SerializeField] private GameObject buildElement;
        [SerializeField] private float buildDistance = 5f;
        [SerializeField] private Transform lookFrom;
        
        private Material _defaultMaterial;

        private void Start()
        {
            _defaultMaterial = buildElement.GetComponent<MeshRenderer>().material;
        }

        private void Update()
        {
            var ray = new Ray(lookFrom.position, lookFrom.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, buildDistance))
            {
                var buildElement = hit.collider.GetComponent<BuildElement>();
                if (buildElement != null)
                {
                    BuildElement element = Instantiate(buildElement, hit.point, Quaternion.identity); 
                    if (buildElement.IsBuild)
                    {
                        buildElement.GetComponent<MeshRenderer>().material = successBuildMaterial;
                    }
                    else
                    {
                        buildElement.GetComponent<MeshRenderer>().material = failBuildMaterial;
                    }
                }
            }
        }
    }
}