using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace SizeOf_Fixer
{
    internal class SizeOf
    {
        public static void SizeOfFixer(ModuleDefMD module)
        {
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.HasBody)
                    {
                        for (int index = 0; index < method.Body.Instructions.Count; ++index)
                        {
                            if (method.Body.Instructions[index].OpCode == OpCodes.Sizeof)
                            {
                                switch ((method.Body.Instructions[index].Operand as ITypeDefOrRef).ToString())
                                {
                                    case "System.Boolean":
                                        method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4_1;
                                        ++Program.SizeOFAmount;
                                        break;
                                    case "System.Byte":
                                        method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4_1;
                                        ++Program.SizeOFAmount;
                                        break;
                                    case "System.Decimal":
                                        method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4;
                                        method.Body.Instructions[index].Operand = 16;
                                        ++Program.SizeOFAmount;
                                        break;
                                    case "System.Double":
                                    case "System.Int64":
                                    case "System.UInt64":
                                        method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4_8;
                                        ++Program.SizeOFAmount;
                                        break;
                                    case "System.Guid":
                                        method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4;
                                        method.Body.Instructions[index].Operand = 16;
                                        ++Program.SizeOFAmount;
                                        break;
                                    case "System.Int16":
                                    case "System.UInt16":
                                        method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4_2;
                                        ++Program.SizeOFAmount;
                                        break;
                                    case "System.Int32":
                                    case "System.Single":
                                    case "System.UInt32":
                                        method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4_4;
                                        ++Program.SizeOFAmount;
                                        break;
                                    case "System.SByte":
                                        method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4_1;
                                        ++Program.SizeOFAmount;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
