using UnityEngine;
using System.Collections;

public class LevelUp : MonoBehaviour
{
    [HideInInspector] public float levelGap = 1.7f;

    public IEnumerator ShiftLevels(Vector3 end, float speed){
        while (Vector3.Distance(this.transform.position ,end) > speed * Time.deltaTime){
            this.transform.position = Vector3.MoveTowards(this.transform.position, end, speed * Time.deltaTime);
            yield return null;
        }
        this.transform.position = end;
    }
}
