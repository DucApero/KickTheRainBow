using UnityEngine;
using DG.Tweening;
public class RagdollCharacterController : MonoBehaviour
{
    private Transform ragdollGroup;
    public Rigidbody2D[] bodyPartRigidbodies;
    public GameObject body;
    private Vector3 initialMousePosition;
    private Vector3 initialRagdollPosition;
    public float force;

    private void Start()
    {
        ragdollGroup = transform;
    }

    private void OnMouseDown()
    {
        // Lấy vị trí chuột trong không gian thế giới
        initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initialRagdollPosition = ragdollGroup.position;
    }

    private void OnMouseDrag()
    {
        // Tính toán vị trí di chuyển của chuột
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        this.transform.position = mousePosition;
        

         // Di chuyển nhóm chứa nhân vật
         //ragdollGroup.position = initialRagdollPosition + mouseDelta;*/

        // Áp dụng lực vào từng bộ phận dựa trên vị trí của chuột và vị trí của bộ phận trong nhóm
        for (int i = 0; i < bodyPartRigidbodies.Length; i++)
        {
            Vector3 bodyPartPosition = bodyPartRigidbodies[i].transform.position;
            Vector3 forceDirection = ragdollGroup.position - bodyPartPosition;
            //bodyPartRigidbodies[i].AddForce(forceDirection * force);
            bodyPartRigidbodies[i].MovePosition(mousePosition);
            
        }
    }
}
