using Godot;
using System;

namespace PlayerController {

	public class PlayerController : KinematicBody2D {

		// =====================
		// = Physics variables =
		// =====================
		private const UInt16 movementSpeed = 100;
		private const UInt16 gravity = 0;//1000;
		private Vector2 velocity = new Vector2(0, 0);


		// ===========================
		// = Environmental variables =
		// ===========================
		private bool effectedByGravity = true;


		// ==================
		// = Custom Methods =
		// ==================
		private void GetInput() {

			if (Input.IsActionPressed("right")) {
				velocity.x += movementSpeed;
			}

			if (Input.IsActionPressed("left")) {
				velocity.x -= movementSpeed;
			}
		
			if (Input.IsActionPressed("jump") && IsOnFloor()) {

				velocity.y -= 250;
			}
			
			velocity = MoveAndSlide(velocity, new Vector2(0, -1));
		}

		private void ApplyGravity(float delta) {

			if (effectedByGravity) {
				velocity.y += gravity * delta;
			}
		}


		// ====================
		// = Override Methods =
		// ====================
		public override void _PhysicsProcess(float delta) {

			velocity.x = 0;
			ApplyGravity(delta);
			GetInput();
		}
	}
}
