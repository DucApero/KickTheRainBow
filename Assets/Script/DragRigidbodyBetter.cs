using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragRigidbodyBetter : MonoBehaviour
{
    public  List<GameObject> lstTfBodyPart = new List<GameObject>();
    

    [Tooltip("The spring force applied when dragging rigidbody. The dragging is implemented by attaching an invisible spring joint.")]
    public float Spring = 5.0f;
    public float Damper = 5.0f;
    public float Drag = 10.0f;
    public float AngularDrag = 5.0f;
    public float Distance = 0.09f;
    public float ScrollWheelSensitivity = 5.0f;
    public float RotateSpringSpeed = 10.0f;

    [Tooltip("Pin dragged spring to its current location.")]
    public KeyCode KeyToPinSpring = KeyCode.Space;

    [Tooltip("Delete all pinned springs.")]
    public KeyCode KeyToClearPins = KeyCode.Delete;

    [Tooltip("Twist spring.")]
    public KeyCode KeyToRotateLeft = KeyCode.Z;

    [Tooltip("Twist spring.")]
    public KeyCode KeyToRotateRight = KeyCode.C;

    [Tooltip("Set any LineRenderer prefab to render the used springs for the drag.")]
    public LineRenderer SpringRenderer;

    private int m_SpringCount = 1;
    [SerializeField] GameObject Gun;
    private SpringJoint2D m_SpringJoint;
    private LineRenderer m_SpringRenderer;
    [SerializeField] GameObject gameObject;
    public GameObject target;
    private void Update()
    {
        
        //UpdatePinnedSprings();


        // Make sure the user pressed the mouse down
        if (!Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonUp(0) && target != null)
            {
                /*MouseUp();*/
            }
            if (Input.GetMouseButton(0) && target != null)
            {
                //SetDirParticleGunLine();
            }
            return;
            
        }
        //var mainCamera = FindCamera();

        // We need to actually hit an object
        // RaycastHit2D hit = new RaycastHit2D();
        Vector3 vt = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        target = GetRigid(vt);

        /*if (
            !Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                             mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
                             Physics.DefaultRaycastLayers)) {
            //return;
        }*/
        // We need to hit a rigidbody that is not kinematic
        if (target == null) return;
        /*UiController.Instance.ActiveSettingsGamePlay(false);
        GameController.Instance.DeActiveTutorial();
        SoundController.Instance.PlaySound(Common.TypeObjSound.gunstart);
        lineRenderController.SetTarget(target);
        BodyPart bodyPart = target.GetComponent<BodyPart>();*/
       /* if (bodyPart != null && bodyPart.DontMove)
        {
            return;
        }*/
        if (!m_SpringJoint)
        {
            var go = new GameObject("Rigidbody dragger-" + m_SpringCount);
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            Rigidbody2D body = go.AddComponent<Rigidbody2D>();
            m_SpringJoint = go.AddComponent<SpringJoint2D>();
            body.isKinematic = true;
            m_SpringCount++;

            if (SpringRenderer)
            {
                m_SpringRenderer = GameObject.Instantiate(SpringRenderer.gameObject, m_SpringJoint.transform, true).GetComponent<LineRenderer>();
                m_SpringRenderer.sortingOrder = -10;
            }
        }

        m_SpringJoint.transform.position = target.transform.position;
        m_SpringJoint.anchor = Vector3.zero;
        m_SpringJoint.autoConfigureDistance = false;
        m_SpringJoint.distance = 0.09f;
        m_SpringJoint.dampingRatio = Damper;
        m_SpringJoint.frequency = Spring;
        m_SpringJoint.connectedBody = target.GetComponent<Rigidbody2D>();
        if (m_SpringRenderer)
        {
            m_SpringRenderer.enabled = true;
        }
        //UpdatePinnedSprings();

        StartCoroutine(DragObject());
    }

    float oldDrag;
    float oldAngularDrag;

    private IEnumerator DragObject()
    {
        oldDrag = m_SpringJoint.connectedBody.drag;
        oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
        m_SpringJoint.connectedBody.drag = Drag;
        m_SpringJoint.connectedBody.angularDrag = AngularDrag;
        //var mainCamera = FindCamera();
        while (Input.GetMouseButton(0) && !Input.GetKeyDown(KeyToPinSpring))
        {
            //distance += Input.GetAxis("Mouse ScrollWheel") * ScrollWheelSensitivity;

            //var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 vt = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            if (m_SpringJoint != null )
            {
                m_SpringJoint.transform.position = vt;
                if(m_SpringJoint.connectedBody != null)
                {
                    var connectedPosition = m_SpringJoint.connectedBody.transform.position * m_SpringJoint.connectedAnchor;
                    Vector3 vector3 = new Vector3(connectedPosition.x, connectedPosition.y, 0);
                    var axis = m_SpringJoint.transform.position - vector3;
                }
                
            }
               
            /*   if (Input.GetKey(KeyToRotateLeft))
               {
                   m_SpringJoint.connectedBody.transform.Rotate(axis, RotateSpringSpeed, Space.World);
               }
               if (Input.GetKey(KeyToRotateRight))
               {
                   m_SpringJoint.connectedBody.transform.Rotate(axis, -RotateSpringSpeed, Space.World);
               }*/
            yield return null;
        }

        ResetSpringJoint();


    }
    public void ResetLine()
    {
        //lineRenderController.ResetLine();
    }
   /* public void MouseUp()
    {
        levelUnit.MouseUp();
        if(target != null)
        {
            BodyPart bodyPartTarget = target.GetComponent<BodyPart>();
            Obstacle obstacle = target.GetComponent<Obstacle>();
            target = null;
            if (bodyPartTarget != null)
                bodyPartTarget.SetDirParticleGunLine(null);
            if (obstacle != null)
                obstacle.SetDirParticleGunLine(null);
            SoundController.Instance.StopSoundLoop();
        }
        
    }*/
/*    public void SetDirParticleGunLine()
    {
        Obstacle obstacle = target.GetComponent<Obstacle>();
        BodyPart bodyPartTarget = target.GetComponent<BodyPart>();
        if (bodyPartTarget != null)
            bodyPartTarget.SetDirParticleGunLine(lineRenderController.TfHeadGun);
        if (obstacle != null)
            obstacle.SetDirParticleGunLine(lineRenderController.TfHeadGun);
    }*/
    public void ResetSpringJoint() 
    {
        if (m_SpringJoint != null && m_SpringJoint.connectedBody)
        {
            m_SpringJoint.connectedBody.drag = oldDrag;
            m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;

            if (Input.GetKeyDown(KeyToPinSpring))
            {
                m_SpringJoint = null;
                m_SpringRenderer = null;
            }
            else
            {
                m_SpringJoint.connectedBody = null;
                if (m_SpringRenderer)
                {
                    m_SpringRenderer.enabled = false;
                }
            }
        }
    }
    /*    public void UpdateLevel(LevelUnit levelUnit)
        {
            this.levelUnit = levelUnit;
            UpdateLineRender(levelUnit.LineRender);
        }
        public void UpdateLineRender(LineRenderController lineRenderController)
        {
            this.lineRenderController = lineRenderController;
        }*/

    public GameObject GetRigid(Vector3 vector3)
    {
        int indexTf = 0;
        Distance = (lstTfBodyPart[0].transform.position - vector3).magnitude; // Mathf.Sqrt(Mathf.Pow((lstTfBodyPart[0].position.x - vector3.x),2) + Mathf.Pow((lstTfBodyPart[0].position.y - vector3.y), 2));
        for (int i = 0; i < lstTfBodyPart.Count; i++)
        {
            if (lstTfBodyPart[i] == null) continue;
            float dis = (lstTfBodyPart[i].transform.position - vector3).magnitude;// Mathf.Sqrt(Mathf.Pow((lstTfBodyPart[i].position.x - vector3.x), 2) + Mathf.Pow((lstTfBodyPart[i].position.y - vector3.y), 2));
            if (Distance > dis)
            {
                Distance = dis;
                indexTf = i;
            }
        }
        
        //Distance = distance;

        if (Distance > 3) return null;
        /*currentObstacle = lstTfBodyPart[indexTf].gameObject.GetComponent<Obstacle>();
        if (currentObstacle != null)
        {
            Distance = 0f;
            currentObstacle.CollisionTouch();
        }
        currentBodyPart = lstTfBodyPart[indexTf].gameObject.GetComponent<BodyPart>();
        if (currentBodyPart != null)
        {
            currentBodyPart.Enemy.ActionTouch(true, false);
        }*/
        return lstTfBodyPart[indexTf].gameObject;
    }
}
