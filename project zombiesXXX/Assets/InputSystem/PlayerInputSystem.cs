using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class PlayerInputSystem : MonoBehaviour
	{
		[Header("Character Input Values")]
		[SerializeField] PlayerInput playerInput;
		public float JumpValue;
		public float EValue;
		public float QValue;
		public Vector2 move;
		public Vector2 look;
		public float Zoom;
		public bool jump;
		public bool sprint;
		public bool findTarget;
		public bool Q;
		public bool E;
		public bool n1;
		public bool n2;
		public bool n3;
		public bool n4;
		public bool n5;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnZoom(InputValue value)
		{
			Zoom = value.Get<float>();
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnJumpVlaue(InputValue value)
		{
			JumpValue = value.Get<float>();
		}
		public void OnEVlaue(InputValue value)
		{
			EValue = value.Get<float>();
		}
		public void OnQVlaue(InputValue value)
		{
			QValue = value.Get<float>();
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnUseQ(InputValue value)
		{
			QInput(value.isPressed);
		}
		public void OnUseE(InputValue value)
		{
			EInput(value.isPressed);
		}
		public void OnUseSkill1(InputValue value)
		{
			Skill1Input(value.isPressed);
		}
		public void OnUseSkill2(InputValue value)
		{
			Skill2Input(value.isPressed);
		}
		public void OnUseSkill3(InputValue value)
		{
			Skill3Input(value.isPressed);
		}
		public void OnUseSkill4(InputValue value)
		{
			Skill4Input(value.isPressed);
		}
		public void OnUseSkill5(InputValue value)
		{
			Skill5Input(value.isPressed);
		}
		

		public void OnFindTarget(InputValue value)
		{
			FindTargetInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void FindTargetInput(bool newFindTargetState)
		{
			findTarget = newFindTargetState;
		}
		public void QInput(bool value)
		{
			Q = value;
		}
		public void EInput(bool value)
		{
			E = value;
		}
		public void Skill1Input(bool value)
		{
			n1 = value;
		}
		public void Skill2Input(bool value)
		{
			n2 = value;
		}
		public void Skill3Input(bool value)
		{
			n3 = value;
		}
		public void Skill4Input(bool value)
		{
			n4 = value;
		}
		public void Skill5Input(bool value)
		{
			n5 = value;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}