// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Xml.Serialization;

using XmlTesting;

Console.WriteLine("Hello, World!");


var builder = new DynamicTypeBuilder();
var doublePoint = builder.BuildCustomPoint(typeof(double));
var pointPoint = builder.BuildCustomPoint(typeof(Point));
var rootType = typeof(Root);
//var root = new Root();
//var root2 = new Root();
//var instance1 = (SetpointPoint)Activator.CreateInstance(doublePoint);
//var instance2 = (SetpointPoint)Activator.CreateInstance(pointPoint);

//instance1.Value = 1.2;
//instance2.Value = new Point(1,2);

//root.Point = instance1;
//root2.Point = instance2;

var serialzer = new XmlSerializer(rootType, new[] { typeof(SetpointPoint) });
//TextWriter textWriter = new StringWriter();
//serialzer.Serialize(textWriter, root);
//var r = textWriter.ToString();
/*
 output :
<?xml version=""1.0"" encoding=""UTF-8""?>
<Root xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
   <Point xsi:type=""SetpointPoint_Double"">
      <Value xsi:type=""xsd:double"">1.2</Value>
   </Point>
</Root>
 */
//textWriter.Dispose();
//textWriter = new StringWriter();
//serialzer.Serialize(textWriter, root2);

//var x = textWriter.ToString();
/*
 output 
<?xml version=""1.0"" encoding=""UTF-8""?>
<Root xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
   <Point xsi:type=""SetpointPoint_Point"">
      <Value xsi:type=""Point"">
         <X>3</X>
         <Y>5</Y>
      </Value>
   </Point>
</Root>
 */

var z = @"<?xml version=""1.0"" encoding=""utf-16""?><Root xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><Point xsi:type=""SetpointPoint""><Value xsi:type=""Rectangle""><Location><X>1</X><Y>2</Y></Location><Size><Width>3</Width><Height>4</Height></Size><X>1</X><Y>2</Y><Width>3</Width><Height>4</Height></Value></Point></Root>";

//var d = (Root)serialzer.Deserialize(new StringReader(x));
//var d2 = (Root)serialzer.Deserialize(new StringReader(r));
var d3 = (Root)serialzer.Deserialize(new StringReader(z));

//PrintTheValue(d);
//PrintTheValue(d2);

//void PrintTheValue(Root r)
//{
//    // you can use reflection here
//    if (r.Point.Value is Point p)
//    {
//        Console.WriteLine(p.X);
//    }
//    else if (r.Point.Value is double db)
//    {
//        Console.WriteLine(db);
//    }
//}


Console.WriteLine(doublePoint);