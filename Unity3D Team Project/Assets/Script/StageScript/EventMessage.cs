using UnityEngine;
using System.Collections;

public class EventMessage : MonoBehaviour
{
    public GameObject EventWindow;
    public Sprite[] MessageSprite = new Sprite[18];
    //Sprite List
    //1. 기름 뿌리기전 생성안된 발판 근처
    //2. 기름 뿌린후
    //3. 뻐꾸기 수리전
    //4, 타임어택 1차
    //5. 손잡이 돌리기전 안내려온 체인 근처
    //6. 손잡이 수리후
    //7. 스패너 획득 전
    //8. 스패너 획득 후
    //9. 체인부분 너트 삽입 전
    //10. 톱니 B 뺴고 A삽입전
    //11. 톱이 B 삽입 위치 근처
    //12. 기름 획득
    //13. 기름 분사기 획득
    //14. 너트 획득
    //15. 핸들 수리전
    //16. 톱니 A 획득
    //17. 톱니 B 획득
    //18. 펜치 획득
    //19. 타임어택 2차
	void Update ()
    {
	    switch(CharacterMoving.EventType)
        {
            case CharacterMoving.MessageType.Oil_Before:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[0];
                break;
            case CharacterMoving.MessageType.Oil_After:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[1];
                break;
            case CharacterMoving.MessageType.Cuckoo_Before:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[2];
                break;
            case CharacterMoving.MessageType.Cuckoo_After:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[3];
                break;
            case CharacterMoving.MessageType.Handle_Before:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[4];
                break;
            case CharacterMoving.MessageType.Handle_After:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[5];
                break;
            case CharacterMoving.MessageType.Spaner_Before:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[6];
                break;
            case CharacterMoving.MessageType.Spaner_After:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[7];
                break;
            case CharacterMoving.MessageType.ChainNut_Insert:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[8];
                break;
            case CharacterMoving.MessageType.SawToothA_Before:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[9];
                break;
            case CharacterMoving.MessageType.SawToothB_Before:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[10];
                break;
            case CharacterMoving.MessageType.GetOil:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[11];
                break;
            case CharacterMoving.MessageType.GetOilSpray:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[12];
                break;
            case CharacterMoving.MessageType.GetNut:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[13];
                break;
            case CharacterMoving.MessageType.Handle_Before2:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[14];
                break;
            case CharacterMoving.MessageType.GetSawToothA:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[15];
                break;
            case CharacterMoving.MessageType.GetSawToothB:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[16];
                break;
            case CharacterMoving.MessageType.GetPlier:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[17];
                break;
            case CharacterMoving.MessageType.Cuckoo_After2nd:
                EventWindow.GetComponent<UnityEngine.UI.Image>().sprite = MessageSprite[18];
                break;
        }
	}
}
