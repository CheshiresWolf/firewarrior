using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour
{
    //переменная для установки макс. скорости персонажа
    public float maxSpeed = 10f; 
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;
    //ссылка на компонент анимаций
    private Animator anim;
    private Rigidbody2D rigidbody2D;

	private void Start() {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
    /// <summary>
    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    /// </summary>
	private void FixedUpdate() {
        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //при стандартных настройках проекта 
        //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
        //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
        //float move = Input.GetAxis("Horizontal");

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			float move = touchDeltaPosition.x;

	        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
	        //приэтом нам нужен модуль значения
	        anim.SetFloat("Speed", Mathf.Abs(move));

	        //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
	        //равную значению оси Х умноженное на значение макс. скорости
	        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

	        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
	        if (move > 0 && !isFacingRight) {
	            //отражаем персонажа вправо
	            Flip();
	        //обратная ситуация. отражаем персонажа влево
	        } else if (move < 0 && isFacingRight) {
	            Flip();
	        }
	    }
    }

    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
    private void Flip() {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
}