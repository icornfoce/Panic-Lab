using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Chemistry/Element")]
public class ElementData : ScriptableObject {
    public string elementName;
    public string symbol;
    public int atomicNumber;
    public float atomicMass;
    public Color categoryColor; // สีตามกลุ่ม เช่น โลหะ, อโลหะ
    [TextArea] public string description;
}