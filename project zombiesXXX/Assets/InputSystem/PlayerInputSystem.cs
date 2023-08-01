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
		public float RightButtonValue;
		public float LeftButtonValue;
		public float RButtonValue;
		public Vector2 move;
		public Vector2 look;
		public float Zoom;
		public bool jump;
		public bool sprint;
		public bool findTarget;
		public bool LeftButton;
		public bool RightButton;
		public bool Intract;
		public bool R;
		public bool n1;
		public bool n2;
		public bool n3;
		public bool n4;

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
		public void OnRightButtonVlaue(InputValue value)
		{
			RightButtonValue = value.Get<float>();
		}
		public void OnLeftButtonVlaue(InputValue value)
		{
			LeftButtonValue = value.Get<float>();
		}
		public void OnRButtonVlaue(InputValue value)
		{
			RButtonValue = value.Get<float>();
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnUseLeftButton(InputValue value)
		{
			LeftButtonInput(value.isPressed);
		}
		public void OnUseRightButton(InputValue value)
		{
			RightButtonInput(value.isPressed);
		}
		public void OnUseR(InputValue value)
		{
			SkillRInput(value.isPressed);
		}
		
		public void OnIntract(InputValue value)
		{
			IntractInput(value.isPressed);
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
		public void LeftButtonInput(bool value)
		{
			LeftButton = value;
		}
		public void RightButtonInput(bool value)
		{
			RightButton = value;
		}

		public void SkillRInput(bool isPressed)
        {
            R = isPressed;
        }
		public void IntractInput(bool value)
		{
			Intract = value;
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