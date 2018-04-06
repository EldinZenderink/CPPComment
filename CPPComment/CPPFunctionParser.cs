using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPComment
{
    class CPPFunctionParser
    {
        public CPPFunctionParser()
        {

        }

        public string GenerateComment(string function)
        {
            StringBuilder commentGenerator = new StringBuilder();

            
            string className = String.Empty;
            string type = String.Empty; 
            string functionName = GetFunctionName(function);
           

            if(functionName != String.Empty)
            {
                Dictionary<string, string> parameters = GetParameters(function);

                bool isConstructor = CheckIfConstructor(function);
                bool typeIsPointer = CheckIfTypeIsPointer(function);
                type = GetTypeOfFunction(function);

                if ((className = GetClassName(function)) != String.Empty)
                {
                    

                    commentGenerator.AppendLine("/<summary>");
                    commentGenerator.AppendLine("///   <para>");
                    if (isConstructor)
                    {
                        if (parameters.Count != 0)
                        {
                            commentGenerator.AppendLine("///       Constructor with Parameters for Class: " + className);
                            commentGenerator.AppendLine("///   </para>");
                            commentGenerator.AppendLine("///</summary>");

                            foreach (KeyValuePair<string, string> pair in parameters)
                            {

                                commentGenerator.AppendLine("///<param name=\"" + pair.Value + "\">");
                                commentGenerator.AppendLine("///   <para>");
                                commentGenerator.AppendLine("///       Type: " + pair.Key);
                                commentGenerator.AppendLine("///   </para>");
                                commentGenerator.AppendLine("///</param>");
                            }

                        }
                        else
                        {
                            commentGenerator.AppendLine("///       Constructor for Class: " + className);
                            commentGenerator.AppendLine("///   </para>");
                            commentGenerator.AppendLine("///</summary>");
                        }
                    }
                    else
                    {
                        commentGenerator.AppendLine("///       Function name: " + functionName);
                        commentGenerator.AppendLine("///       Function is part of Class: " + className);
                        commentGenerator.AppendLine("///   </para>");
                        commentGenerator.AppendLine("///</summary>");
                        foreach (KeyValuePair<string, string> pair in parameters)
                        {

                            commentGenerator.AppendLine("///<param name=\"" + pair.Value + "\">");
                            commentGenerator.AppendLine("///   <para>");
                            commentGenerator.AppendLine("///       Type: " + pair.Key);
                            commentGenerator.AppendLine("///   </para>");
                            commentGenerator.AppendLine("///</param>");
                        }

                        if(type != "void")
                        {
                            commentGenerator.AppendLine("///<returns>");
                            if (typeIsPointer)
                            {
                                commentGenerator.AppendLine("///    Pointer of type: " + type);
                            } else
                            {
                                commentGenerator.AppendLine("///    Value of type: " + type);
                            }
                            commentGenerator.AppendLine("///</returns>");
                        }
                    }



                }
                else
                {

                    commentGenerator.AppendLine("/<summary>");
                    commentGenerator.AppendLine("///   <para>");
                    commentGenerator.AppendLine("///       Function name: " + functionName);
                    commentGenerator.AppendLine("///       Function type : " + type);
                    commentGenerator.AppendLine("///   </para>");
                    commentGenerator.AppendLine("///</summary>");
                    foreach (KeyValuePair<string, string> pair in parameters)
                    {

                        commentGenerator.AppendLine("///<param name=\"" + pair.Value + "\">");
                        commentGenerator.AppendLine("///   <para>");
                        commentGenerator.AppendLine("///       Type: " + pair.Key);
                        commentGenerator.AppendLine("///   </para>");
                        commentGenerator.AppendLine("///</param>");
                    }
                }
            } else
            {
                commentGenerator.AppendLine("/<summary>");
                commentGenerator.AppendLine("///</summary>");
            }  
            return commentGenerator.ToString();
        }

        public bool CheckIfConstructor(string input)
        {

            string possibleClassNames = input.Split('(')[0];
          

            if (possibleClassNames.Contains(" "))
            {
                return false;
            }

            return true;
        }


        public bool CheckIfTypeIsPointer(string input)
        {
            string type = input.Split(' ')[0].Trim();

            if (type.Contains("*"))
            {
                return true;
            }

            return false;
        }

        public string GetClassName(string input)
        {
            if (input.Contains("::"))
            {
                string[] possibleClassNames = input.Split('(')[0].Split(new string[] { "::" }, StringSplitOptions.None);
                string className =  String.Empty;
                if(possibleClassNames.Length >= 2)
                {
                    className = possibleClassNames[possibleClassNames.Length - 2];
                } else
                {
                    className = possibleClassNames[0];
                }
                if (className.Contains(" "))
                {
                    className = className.Split(' ')[1];
                }
                return className;
            }
            else
            {
                return String.Empty;
            }
        }

        public string GetTypeOfFunction(string input)
        {
            if (input.Contains(" "))
            {
                string type = input.Split(' ')[0];

                return type;
            }
            else
            {
                return String.Empty;
            }
        }

        public string GetFunctionName(string input)
        {
            if (input.Contains("(") && input.Contains(")"))
            {
                string functionName = input.Split('(')[0];
                if (functionName.Contains(" "))
                {
                    functionName = functionName.Split(' ')[1];
                }

                if (functionName.Contains("::"))
                {
                    functionName = functionName.Split(new string[] { "::" }, StringSplitOptions.None)[1];
                }
                return functionName;
            }
            else
            {
                return String.Empty;
            }
        }

        public Dictionary<string, string> GetParameters(string input)
        {
            if (input.Contains("(") && input.Contains(")"))
            {
                string paramteresAsCompleteString = input.Split('(')[1].Split(')')[0];

                string[] parametersPerParameter = paramteresAsCompleteString.Split(',');
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                foreach (string parameterString in parametersPerParameter)
                {
                    if (parameterString.Contains(" "))
                    {
                        string parameterType = parameterString.Trim().Split(' ')[0];
                        string parameterName = parameterString.Trim().Split(' ')[1];
                        parameters.Add(parameterType, parameterName);
                    }
                }
                return parameters;
            }
            else
            {
                return new Dictionary<string, string>();
            }
        }
    }
}
