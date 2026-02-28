using UnityEngine;

public enum ElementState { Solid, Liquid, Gas, Unknown }
 
[CreateAssetMenu(fileName = "NewElement", menuName = "Chemistry/Element")]
public class ElementData : ScriptableObject {
    public string elementName;
    public string symbol;
    public int atomicNumber;
    public float atomicMass;
    public ElementState state;
    public Color categoryColor;
    [TextArea] public string description;
}