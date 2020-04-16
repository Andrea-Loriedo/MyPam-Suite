using UnityEngine;
using UXF;

public class WorkspaceManager : MonoBehaviour
{
    public Session session;
    public float referenceScale = 3f;

    public void ScaleWorkspace()
    {
        float radius = session.settings.GetFloat("workspace_radius_cm")/10;
        float scaleFactor = radius/referenceScale;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
