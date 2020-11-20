using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Brach", menuName="CustomObject/Branch")]
public class DialogueBranch : ScriptableObject
{
    public string dialogueID = "";
    public List<string> DialogueLines;
    public List<ResponseBranch> ResponseOptions;

    // Start is called before the first frame update
  
}
[System.Serializable]
public class ResponseBranch
{
    public string text;
    public DialogueBranch nextBranch;

}