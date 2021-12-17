using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlTesting
{
    public class DynamicTypeBuilder
    {
        private static readonly MethodAttributes getSetAttr =
            MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.SpecialName |
                MethodAttributes.HideBySig;

        private static readonly AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
        private static readonly AssemblyBuilder ab =
             AssemblyBuilder.DefineDynamicAssembly(
                aName,
                AssemblyBuilderAccess.Run);
        private static readonly ModuleBuilder mb =
            ab.DefineDynamicModule(aName.Name + ".dll");

        public Type BuildCustomPoint(Type valueType)
        {
            var tb = mb.DefineType(
                "SetpointPoint_" + valueType.Name,
                 TypeAttributes.Public, typeof(SetpointPoint));
            var pi = typeof(SetpointPoint).GetProperty("Value");
            var propertyBuilder = tb.DefineProperty(pi.Name,
                                                           PropertyAttributes.HasDefault,
                                                           pi.PropertyType,
                                                           null);
            var fieldBuilder = tb.DefineField("_value",
                                                       pi.PropertyType,
                                                       FieldAttributes.Private);
            var getBuilder =
          tb.DefineMethod($"get_{pi.Name}",
                                     getSetAttr,
                                     pi.PropertyType,
                                     Type.EmptyTypes);

            var getIL = getBuilder.GetILGenerator();

            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getIL.Emit(OpCodes.Ret);

            var setBuilder =
                tb.DefineMethod($"set_{pi.Name}",
                                           getSetAttr,
                                           null,
                                           new Type[] { pi.PropertyType });

            var setIL = setBuilder.GetILGenerator();

            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fieldBuilder);
            setIL.Emit(OpCodes.Ret);

            // Last, we must map the two methods created above to our PropertyBuilder to
            // their corresponding behaviors, "get" and "set" respectively.
            propertyBuilder.SetGetMethod(getBuilder);
            propertyBuilder.SetSetMethod(setBuilder);

            var xmlElemCtor = typeof(XmlElementAttribute).GetConstructor(new[] { typeof(Type) });
            var attributeBuilder = new CustomAttributeBuilder(xmlElemCtor, new[] { valueType });
            propertyBuilder.SetCustomAttribute(attributeBuilder);

            return tb.CreateType();
        }
    }
}
