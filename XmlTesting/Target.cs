using System.Xml;
using System.Xml.Serialization;

namespace XmlTesting;


[Serializable]
public class SetpointPoint 
{
    [XmlAttribute]
    public string InstrumentName { get; set; }
    [XmlAttribute]
    public string Property { get; set; }

    [XmlElement]
    public virtual object Value { get; set; }

}

[Serializable]
public class Root
{
    [XmlElement("Point")]
    public SetpointPoint Point { get; set; }
}

public interface IValueOwner
{
    object Value { get; set; }
}
