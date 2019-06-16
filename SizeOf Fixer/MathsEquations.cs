using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace SizeOf_Fixer
{
    internal class MathsEquations
    {
        public static void MathsFixer(ModuleDefMD module)
        {
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.HasBody)
                    {
                        for (int index = 0; index < method.Body.Instructions.Count; ++index)
                        {
                            if (method.Body.Instructions[index].OpCode == OpCodes.Add)
                            {
                                if (method.Body.Instructions[index - 1].IsLdcI4() && method.Body.Instructions[index - 2].IsLdcI4())
                                {
                                    int num = method.Body.Instructions[index - 2].GetLdcI4Value() + method.Body.Instructions[index - 1].GetLdcI4Value();
                                    method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4;
                                    method.Body.Instructions[index].Operand = num;
                                    method.Body.Instructions[index - 2].OpCode = OpCodes.Nop;
                                    method.Body.Instructions[index - 1].OpCode = OpCodes.Nop;
                                    ++Program.MathsAmount;
                                }
                            }
                            else if (method.Body.Instructions[index].OpCode == OpCodes.Mul)
                            {
                                if (method.Body.Instructions[index - 1].IsLdcI4() && method.Body.Instructions[index - 2].IsLdcI4())
                                {
                                    int num = method.Body.Instructions[index - 2].GetLdcI4Value() * method.Body.Instructions[index - 1].GetLdcI4Value();
                                    method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4;
                                    method.Body.Instructions[index].Operand = num;
                                    method.Body.Instructions[index - 2].OpCode = OpCodes.Nop;
                                    method.Body.Instructions[index - 1].OpCode = OpCodes.Nop;
                                    ++Program.MathsAmount;
                                }
                            }
                            else if (method.Body.Instructions[index].OpCode == OpCodes.Sub && (method.Body.Instructions[index - 1].IsLdcI4() && method.Body.Instructions[index - 2].IsLdcI4()))
                            {
                                int num = method.Body.Instructions[index - 2].GetLdcI4Value() - method.Body.Instructions[index - 1].GetLdcI4Value();
                                method.Body.Instructions[index].OpCode = OpCodes.Ldc_I4;
                                method.Body.Instructions[index].Operand = num;
                                method.Body.Instructions[index - 2].OpCode = OpCodes.Nop;
                                method.Body.Instructions[index - 1].OpCode = OpCodes.Nop;
                                ++Program.MathsAmount;
                            }
                        }
                    }
                }
            }
        }
    }
}
