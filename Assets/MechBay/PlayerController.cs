﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject Pointer;
	private PointerController pointerController;

    //переменная для установки макс. скорости персонажа
    private float maxSpeed = 1.0f; 
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = false;
    //ссылка на компонент анимаций
    private Animator anim;
    private Rigidbody2D rigidbody2D;

	private float half_width;

	private void Start() {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

		half_width = Screen.width / 2.0f;

		pointerController = Pointer.GetComponent<PointerController>();
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
		//Debug.Log("FixedUpdate");
		float x = 0.0f;

		if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
			Vector2 touchPosition = Input.GetMouseButtonUp(0) ? (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) : Input.GetTouch(0).position;

			pointerController.showPointer(new Vector2 (touchPosition.x, -2.5f));

			Debug.Log("touchDeltaPosition : " + touchPosition);
			x = (touchPosition.x < half_width) ? -maxSpeed : maxSpeed;

			rigidbody2D.velocity = new Vector2(x, rigidbody2D.velocity.y);

			if (x > 0 && !isFacingRight) {
				//отражаем персонажа вправо
				Flip();
				//обратная ситуация. отражаем персонажа влево
			} else if (x < 0 && isFacingRight) {
				Flip();
			}
		}

		anim.SetFloat("Speed", Mathf.Abs(x));
    }

    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
    private void Flip() {
        Debug.Log("Flip");

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