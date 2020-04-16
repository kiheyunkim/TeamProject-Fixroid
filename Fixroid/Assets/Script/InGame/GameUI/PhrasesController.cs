using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhrasesController : MonoBehaviour
{
    static public PhrasesController GetInstance { get; private set; }
    
    public enum AreaNotify { SpnaerGet,NeedHandle,HandleGame,NeedNut,NeedSpray,NeedSaw,SpringGame };
    public List<Sprite> AreaNotifySprites = new List<Sprite>();
    public enum MiniGameEndNotify { Handle,Spray,Spring};
    public List<Sprite> minGameEndNotifySprite = new List<Sprite>();
    public enum ItemGetNotify { Spanner, Nut, SawA, Spray, MakeFullSpray, SawB, Piler };
    public List<Sprite> itemGetNotifySprites = new List<Sprite>();

    private AudioSource phraseSound;
    private AudioSource itempharaseSound;

    private const int highPoint = 414;
    private const int lowPoint = 306;

    private UnityEngine.UI.Image phraseBg;
    private Coroutine phraseMove;

    protected IEnumerator Appearing()
    {
        while (transform.localPosition.y > lowPoint)
        {
            Vector3 currentPos = transform.localPosition;
            currentPos.y -= 4;

            transform.localPosition = currentPos;
            yield return new WaitForEndOfFrame();
        }
    }

    private void Awake()
    {
        phraseSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/UI/Phrase/PhraseSound");
        itempharaseSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/UI/Phrase/ItemPhraseSound");

        phraseBg = GetComponent<UnityEngine.UI.Image>();
        GetInstance = this;

        Sprite[] sprites = Resources.LoadAll<Sprite>("Stage/Stage1/Notify/Pharase");
        for (int i = 0; i < 8; i++)
            AreaNotifySprites.Add(sprites[i]);
        for (int i = 8; i < 11; i++)
            minGameEndNotifySprite.Add(sprites[i]);
        for (int i = 11; i < 18; i++)
            itemGetNotifySprites.Add(sprites[i]);
    }

    protected IEnumerator Disappearing()
    {
        while (transform.localPosition.y < highPoint)
        {
            Vector3 currentPos = transform.localPosition;
            currentPos.y += 4;

            transform.localPosition = currentPos;
            yield return new WaitForEndOfFrame();
        }
    }

    protected IEnumerator PhraseMoving()
    {
        yield return StartCoroutine(Appearing());
        yield return new WaitForSeconds(2.0f);
        yield return StartCoroutine(Disappearing());
    }

    public void StartPhrase(AreaNotify type)
    {
        phraseBg.sprite = AreaNotifySprites[(int)type];

        transform.localPosition = new Vector3(0, highPoint, 0);
        if (phraseMove != null)
            StopCoroutine(phraseMove);

        phraseSound.Play();
        phraseMove = StartCoroutine(PhraseMoving());
    }

    public void StartPhrase(MiniGameEndNotify type)
    {
        phraseBg.sprite = minGameEndNotifySprite[(int)type];

        if (phraseMove != null)
            StopCoroutine(phraseMove);
        transform.localPosition = new Vector3(0, highPoint, 0);

        phraseSound.Play();
        phraseMove = StartCoroutine(PhraseMoving());
    }

    public void StartPhrase(ItemGetNotify type)
    {
        phraseBg.sprite = itemGetNotifySprites[(int)type];

        transform.localPosition = new Vector3(0, highPoint, 0);
        if (phraseMove != null)
            StopCoroutine(phraseMove);

        itempharaseSound.Play();
        phraseMove = StartCoroutine(PhraseMoving());
    }
}
