using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementsScript : MonoBehaviour
{

    public Rigidbody2D rb;

    public float MoveSpead = 10f;
    public float ForceJump =0f;

    public bool jump = true;

    #region методи класа transform
    /// <summary>
    /// Перемещение при помощи transform.SetPositionAndRotation
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MoveTransformSetPositionAndRotation(float Direction)
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x + Direction * MoveSpead * Time.deltaTime, transform.position.y), transform.rotation);
    }
    /// <summary>
    /// Перемещение при помощи transform.Translate
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MoveTransformTranslate(float Direction)
    {
        transform.Translate(new Vector3(Direction, 0, 0) * Time.deltaTime * MoveSpead);
    }

    #endregion

    #region transform.position
    /// <summary>
    /// Перемещение при помощи transform.position
    /// создает новий вектор и заданими кординатами
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MovePposition(float Direction)
    {
        transform.position = new Vector3(transform.position.x + Direction * MoveSpead * Time.deltaTime, transform.position.y);
    }
    /// <summary>
    /// Перемещение при помощи transform.position
    /// изменяет значение припомощи метода Set
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MovePpositionSet(float Direction)
    {
        Vector3 pos = transform.position;
        pos.Set(transform.position.x + Direction * MoveSpead * Time.deltaTime, transform.position.y, transform.position.z);
        transform.position = pos;
    }
    /// <summary>
    /// Перемещение при помощи transform.position
    /// изменяет переменую position.x
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MovePpositionX(float Direction)
    {
        Vector3 pos = transform.position;
        pos.x += Direction * MoveSpead * Time.deltaTime;
        transform.position = pos;
    }
    #endregion

    #region transform.localPosition
    /// <summary>
    /// Перемещение при помощи transform.localPosition
    /// создает новий вектор и заданими кординатами
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MovelocalPosition(float Direction)
    {
        transform.localPosition = new Vector3(transform.localPosition.x + Direction * MoveSpead * Time.deltaTime, transform.localPosition.y);
    }
    /// <summary>
    /// Перемещение при помощи transform.localPosition
    /// изменяет значение припомощи метода Set
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MovelocalPositionSet(float Direction)
    {
        Vector3 pos = transform.localPosition;
        pos.Set(pos.x + Direction * MoveSpead * Time.deltaTime, pos.y, pos.z);
        transform.position = pos;
    }
    /// <summary>
    /// Перемещение при помощи transform.localPosition
    /// изменяет переменую position.x
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MovelocalPositionX(float Direction)
    {
        Vector3 pos = transform.localPosition;
        pos.x += Direction * MoveSpead * Time.deltaTime;
        transform.position = pos;
    }
    #endregion

    #region RigitBody2d
    /// <summary>
    /// Перемещение при помощи MovePosition
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MoveRigitBody2dMovePosition(float Direction)
    {
        if(Direction != 0)
        {
            rb.MovePosition(new Vector2(rb.position.x + Direction * MoveSpead * Time.deltaTime,rb.position.y + rb.velocity.y * Time.deltaTime));
        }
        
    }
    /// <summary>
    /// Перемещение при помощи Velocity
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MoveRigitBody2dVelocity(float Direction)
    {

            Vector3 targetVelocity = new Vector2(Direction * MoveSpead, rb.velocity.y);
            Vector3 currentVelocity = rb.velocity;
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity ,ref currentVelocity, .05f);
    }

    /// <summary>
    /// Перемещение при помощи Velocity
    /// </summary>
    /// <param name="Direction">направление движения</param>
    public void MoveRigitBody2dAddForce(float Direction)
    {
        rb.velocity.Set(0, rb.velocity.y);
        rb.AddForce(new Vector2(MoveSpead * Direction, 0), ForceMode2D.Force);

        
    }

    #endregion

    public void Jump()
    {
        rb.velocity.Set(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0,ForceJump), ForceMode2D.Impulse);
    }




    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(rb  == null)
        {
            rb  =  gameObject.AddComponent<Rigidbody2D>();
        }
        
    }

    private void FixedUpdate()
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        MoveRigitBody2dAddForce(Input.GetAxisRaw("Horizontal"));
        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log(ForceJump);
            jump = false;
            Jump();
        }

    }

}
