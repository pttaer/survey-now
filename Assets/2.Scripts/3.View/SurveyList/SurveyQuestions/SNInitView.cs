using UnityEngine;
using static SNDoSurveyDTO;

public abstract class SNInitView : MonoBehaviour
{
    public abstract void Init(SNSectionQuestionDTO data);

    public abstract AnswerDTO GetAnswer();
}
