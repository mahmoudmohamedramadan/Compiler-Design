using System.Text.RegularExpressions;

namespace Compiler_Design {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        List<object> varSize = new List<object>();

        List<string> Splitter(string sourceCode) {
            List<string> splitSourceCode = new List<string>();
            Regex RE = new Regex("\\d+\\.\\d+|\'.*\'|\".*\"|\\w+|\\(|\\)|\\++|-+|\\*|%|,|;|&+|\\|+|<=|<|>=|>|==|=|!=|!|\\{|\\}|\\/");
            foreach(Match m in RE.Matches(sourceCode)) {
                splitSourceCode.Add(m.Value);
            }
            return splitSourceCode;
        }

        private class Token {
            public string name = "", type = "";

            public Token() { }

            public Token(string name, string type) {
                this.name = name;
                this.type = type;
            }
        }

        private class Variable {
            public string name = "", str = "";
            public object varValue = 0;
        }

        int f = 0, ct = 0, fstop = 0, flagoutput = 0, firstNumber = 0, secondNumber = 0;
        string prevVar = "", firstVar = "", prevType = "", prevTypeVar = "", varType = "";
        List<Variable> Lvar = new List<Variable>();

        List<Token> Scanner(List<string> splitCode) {
            List<Token> output = new List<Token>();
            List<string> identifiers = new List<string>(new string[] { "int", "float", "string", "double", "bool", "char" });
            List<string> symbols = new List<string>(new string[] { "+", "-", "/", "%", "*", "(", ")", "{", "}", ",", ";", "&&", "||", "<", ">", "=", "!", "++", "==", ">=", "<=", "!=" });
            List<string> reservedWords = new List<string>(new string[] { "for", "while", "if", "do", "return", "break", "continue", "end" });

            for(int i = 0; i < splitCode.Count; i++) {
                if(identifiers.Contains(splitCode[i])) {
                    output.Add(new Token(splitCode[i], "identifier"));
                } else if(symbols.Contains(splitCode[i])) {
                    output.Add(new Token(splitCode[i], "symbol"));
                } else if(reservedWords.Contains(splitCode[i])) {
                    output.Add(new Token(splitCode[i], "reserved word"));
                } else if(float.TryParse(splitCode[i], out _)) {
                    output.Add(new Token(splitCode[i], "number"));
                } else if(bool.TryParse(splitCode[i], out _)) {
                    output.Add(new Token(splitCode[i], "boolean"));
                } else if(isValidVar(splitCode[i])) {
                    Variable myVariable = new Variable();
                    myVariable.name = splitCode[i];
                    Lvar.Add(myVariable);
                    output.Add(new Token(splitCode[i], "variable"));
                } else if(splitCode[i].StartsWith("\"") && splitCode[i].EndsWith("\"")) {
                    output.Add(new Token(splitCode[i], "string"));
                } else output.Add(new Token(splitCode[i], "unknown"));
            }

            return output;

            bool isValidVar(string v) {
                if(v.Length >= 1) {
                    if(char.IsLetter(v[0]) || v[0] == '_') return true;
                    return false;
                } else {
                    return false;
                }
            }
        }

        List<string> SemanticAnalyzer(List<Token> tokens) {
            List<string> errors = new List<string>();
            Token prevInput1 = new Token();
            Token prevInput2 = new Token();
            Token prevInput3 = new Token();
            int selectedRule = 0;

            for(int i = 0; i < tokens.Count; i++) {
                if(selectedRule == 0) {
                    if(Rule1(tokens[i]).StartsWith("Start")) {
                        selectedRule = 1;
                        continue;
                    }

                    if(Rule2(tokens[i]).StartsWith("Start")) {
                        selectedRule = 2;
                        continue;
                    }

                    if(Rule3(tokens[i]).StartsWith("Start")) {
                        selectedRule = 3;
                        continue;
                    }
                }

                if(selectedRule == 1) {
                    var state = Rule1(tokens[i]);
                    if(state.StartsWith("Ok") || state.StartsWith("Error")) {
                        errors.Add(state);
                        selectedRule = 0;
                    }
                }

                if(selectedRule == 2) {
                    var state = Rule2(tokens[i]);
                    if(state.StartsWith("Ok") || state.StartsWith("Error")) {
                        errors.Add(state);
                        selectedRule = 0;
                    }
                }

                if(selectedRule == 3) {
                    var state = Rule3(tokens[i]);
                    if(state.StartsWith("Ok") || state.StartsWith("Error")) {
                        errors.Add(state);
                        selectedRule = 0;
                    }
                }
            }

            if(selectedRule == 1) errors.Add(Rule1(new Token()));
            if(selectedRule == 2) errors.Add(Rule2(new Token()));
            if(selectedRule == 3) errors.Add(Rule3(new Token()));

            string Rule1(Token input) {
                if(prevInput1.name == "" && input.type == "identifier") {
                    prevInput1 = input;
                    if(prevInput1.name == "int") f = 1;
                    else if(prevInput1.name == "float") f = 2;
                    else if(prevInput1.name == "string") f = 3;
                    else if(prevInput1.name == "double") f = 4;
                    else if(prevInput1.name == "bool") f = 5;
                    else if(prevInput1.name == "char") f = 6;
                    return "Start Rule 1";
                } else if(prevInput1.name != "" && prevInput1.type == "identifier") {
                    string state = Rule2(input);
                    if(state.StartsWith("Ok")) prevInput1 = new Token();
                    if(state != "Error: Rule 2") return state.Substring(0, state.IndexOf("Rule 2") - 1) + " Rule 1";
                }

                if(prevInput1.type == "identifier") {
                    prevInput1 = new Token();
                    return "Error Expected 'variable': Rule 1";
                }

                prevInput1 = new Token();
                return "Error: Rule 1";
            }

            string Rule2(Token input) {
                List<string> operators = new List<string>(new string[] { "+", "-", "/", "%", "*" });

                if(prevInput2.name == "" && input.type == "variable") {
                    prevInput2 = input;
                    return "Start Rule 2";
                } else if(prevInput2.type == "variable" && input.name == "=") {
                    firstVar = prevInput2.name;
                    prevVar = prevInput2.name;
                    prevInput2 = input;
                    return "Continue Rule 2";
                } else if(prevInput2.name == "=" && input.type == "variable") {
                    prevInput2 = input;
                    prevTypeVar = input.name;
                    flagoutput = 0;
                    for(int i = 0; i < Lvar.Count; i++) {
                        if(Lvar[i].name == prevInput2.name && flagoutput == 0) {
                            for(int j = 0; j < Lvar.Count; j++) {
                                if(Lvar[j].name == prevVar) {
                                    if(fstop == 1) {
                                        Lvar[j].varValue = Lvar[i].varValue;
                                        varSize.Add(Lvar[j].varValue);

                                        flagoutput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    return "Continue Rule 2";
                } else if(prevInput2.name == "=" && input.type == "number") {
                    prevInput2 = input;
                    prevTypeVar = input.name;
                    Lvar[ct].varValue = Convert.ToInt32(prevInput2.name);
                    return "Continue Rule 2";
                } else if(prevInput2.name == "=" && input.type == "boolean") {
                    prevInput2 = input;
                    prevTypeVar = input.name;
                    Lvar[ct].varValue = Convert.ToBoolean(prevInput2.name);
                    return "Continue Rule 2";
                } else if(prevInput2.name == "=" && input.type == "string") {
                    prevInput2 = input;
                    prevTypeVar = input.name;
                    Lvar[ct].varValue = prevInput2.name;
                    return "Continue Rule 2";
                } else if(
                    new List<string>(new string[] { "number", "boolean", "string" }).Contains(prevInput2.type) && input.name == ";") {
                    prevInput2 = new Token();
                    ct++;
                    return "Ok Rule 2";
                } else if(prevInput2.type == "number" && operators.Contains(input.name)) {
                    firstNumber = Convert.ToInt32(prevInput2.name);
                    secondNumber = Convert.ToInt32(prevInput2.name);
                    prevType = prevInput2.type;
                    prevInput2 = input;
                    return "Continue Rule 2";
                } else if(prevInput2.type == "variable" && operators.Contains(input.name)) {
                    prevVar = prevInput2.name;
                    varType = prevInput2.type;
                    fstop = 1;
                    prevInput2 = input;
                    return "Continue Rule 2";
                } else if(operators.Contains(prevInput2.name) && input.type == "number") {
                    if(prevInput2.name == "+") {
                        if(prevType == "number") {
                            Lvar[ct].varValue = firstNumber + input.name;
                            varSize.Add(Lvar[ct].varValue);
                            secondNumber = Convert.ToInt32(input.name);
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == prevTypeVar && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            Lvar[j].varValue = Convert.ToInt32(Lvar[i].varValue) + Convert.ToInt32(input.name);
                                            varSize.Add(Lvar[j].varValue);
                                            flagoutput = 1;
                                            break;
                                        }
                                    }
                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    if(prevInput2.name == "-") {
                        if(prevType == "number") {
                            Lvar[ct].varValue = firstNumber - Convert.ToInt32(input.name);
                            varSize.Add(Lvar[ct].varValue);
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == prevTypeVar && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            Lvar[j].varValue = Convert.ToInt32(Lvar[i].varValue) - Convert.ToInt32(input.name);
                                            varSize.Add(Lvar[j].varValue);
                                            flagoutput = 1;
                                            break;
                                        }
                                    }

                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    if(prevInput2.name == "*") {
                        if(prevType == "number") {
                            Lvar[ct].varValue = firstNumber * Convert.ToInt32(input.name);
                            varSize.Add(Lvar[ct].varValue);
                            secondNumber = Convert.ToInt32(input.name);
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == prevTypeVar && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            Lvar[j].varValue = Convert.ToInt32(Lvar[i].varValue) * Convert.ToInt32(input.name);
                                            varSize.Add(Lvar[j].varValue);
                                            flagoutput = 1;
                                            break;
                                        }
                                    }

                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    if(prevInput2.name == "/") {
                        if(prevType == "number") {
                            Lvar[ct].varValue = firstNumber / Convert.ToInt32(input.name);
                            varSize.Add(Lvar[ct].varValue);
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == prevTypeVar && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            Lvar[j].varValue = Convert.ToInt32(Lvar[i].varValue) / Convert.ToInt32(input.name);
                                            varSize.Add(Lvar[j].varValue);
                                            flagoutput = 1;
                                            break;
                                        }
                                    }

                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    if(prevInput2.name == "%") {
                        if(prevType == "number") {
                            Lvar[ct].varValue = firstNumber % Convert.ToInt32(input.name);
                            varSize.Add(Lvar[ct].varValue);
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == prevTypeVar && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            Lvar[j].varValue = Convert.ToInt32(Lvar[i].varValue) % Convert.ToInt32(input.name);
                                            varSize.Add(Lvar[j].varValue);
                                            flagoutput = 1;
                                            break;
                                        }
                                    }

                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    prevInput2 = input;
                    return "Continue Rule 2";
                } else if(operators.Contains(prevInput2.name) && input.type == "variable") {
                    if(prevInput2.name == "+") {
                        fstop = 0;

                        if(prevType == "number") {
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            if(fstop == 0) {
                                                Lvar[j].varValue = secondNumber + Convert.ToInt32(Lvar[i].varValue);
                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }

                                    if(flagoutput == 1) break;
                                }
                            }
                        }

                        if(varType == "variable") {
                            flagoutput = 0;

                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == prevVar) {
                                            for(int k = 0; k < Lvar.Count; k++) {
                                                if(firstVar == Lvar[k].name) {
                                                    Lvar[k].varValue = Convert.ToInt32(Lvar[j].varValue) + Convert.ToInt32(Lvar[i].varValue);
                                                    varSize.Add(Lvar[k].varValue);
                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }

                                        if(flagoutput == 1) break;
                                    }

                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    if(prevInput2.name == "-") {
                        fstop = 0;

                        if(prevType == "number") {
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            if(fstop == 0) {
                                                Lvar[j].varValue = secondNumber - Convert.ToInt32(Lvar[i].varValue);

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if(flagoutput == 1) break;
                                }
                            }
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == prevVar) {
                                            for(int k = 0; k < Lvar.Count; k++) {
                                                if(firstVar == Lvar[k].name) {
                                                    Lvar[k].varValue = Convert.ToInt32(Lvar[j].varValue) - Convert.ToInt32(Lvar[i].varValue);
                                                    varSize.Add(Lvar[k].varValue);

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if(flagoutput == 1) break;
                                    }

                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    if(prevInput2.name == "*") {
                        fstop = 0;

                        if(prevType == "number") {
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            if(fstop == 0) {
                                                Lvar[j].varValue = secondNumber * Convert.ToInt32(Lvar[i].varValue);

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if(flagoutput == 1) break;
                                }
                            }
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == prevVar) {
                                            for(int k = 0; k < Lvar.Count; k++) {
                                                if(firstVar == Lvar[k].name) {
                                                    Lvar[k].varValue = Convert.ToInt32(Lvar[j].varValue) * Convert.ToInt32(Lvar[i].varValue);
                                                    varSize.Add(Lvar[k].varValue);

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if(flagoutput == 1) break;
                                    }

                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    if(prevInput2.name == "/") {
                        fstop = 0;

                        if(prevType == "number") {
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            if(fstop == 0) {
                                                Lvar[j].varValue = secondNumber / Convert.ToInt32(Lvar[i].varValue);

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if(flagoutput == 1) break;
                                }
                            }
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == prevVar) {
                                            for(int k = 0; k < Lvar.Count; k++) {
                                                if(firstVar == Lvar[k].name) {
                                                    Lvar[k].varValue = Convert.ToInt32(Lvar[j].varValue) / Convert.ToInt32(Lvar[i].varValue);
                                                    varSize.Add(Lvar[k].varValue);

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if(flagoutput == 1) break;
                                    }
                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    if(prevInput2.name == "%") {
                        fstop = 0;

                        if(prevType == "number") {
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == firstVar) {
                                            if(fstop == 0) {
                                                Lvar[j].varValue = secondNumber % Convert.ToInt32(Lvar[i].varValue);

                                                flagoutput = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if(flagoutput == 1) break;
                                }
                            }
                        }

                        if(varType == "variable") {
                            flagoutput = 0;
                            for(int i = 0; i < Lvar.Count; i++) {
                                if(Lvar[i].name == input.name && flagoutput == 0) {
                                    for(int j = 0; j < Lvar.Count; j++) {
                                        if(Lvar[j].name == prevVar) {
                                            for(int k = 0; k < Lvar.Count; k++) {
                                                if(firstVar == Lvar[k].name) {
                                                    Lvar[k].varValue = Convert.ToInt32(Lvar[j].varValue) % Convert.ToInt32(Lvar[i].varValue);
                                                    varSize.Add(Lvar[k].varValue);

                                                    flagoutput = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if(flagoutput == 1) break;
                                    }
                                    if(flagoutput == 1) break;
                                }
                            }
                        }
                    }

                    prevInput2 = input;
                    return "Continue Rule 2";
                }

                if(prevInput2.type == "variable") {
                    prevInput2 = new Token();
                    return "Error Expected ';' or '=': Rule 2";
                }

                if(prevInput2.name == "=") {
                    prevInput2 = new Token();
                    return "Error Expected 'number' or 'variable': Rule 2";
                }

                if(prevInput2.type == "variable") {
                    prevInput2 = new Token();
                    return "Error Expected ';' or 'operator': Rule 2";
                }

                if(prevInput2.type == "number") {
                    prevInput2 = new Token();
                    return "Error Expected ';' or 'operator': Rule 2";
                }

                if(operators.Contains(prevInput2.name)) {
                    prevInput2 = new Token();
                    return "Error Expected 'number' or 'variable': Rule 2";
                }

                prevInput2 = new Token();
                return "OK Rule 2";
            }

            string Rule3(Token input) {
                List<string> compOperators = new List<string>(new string[] { "==", "!=", "<=", "<", ">", ">=" });
                List<string> boolOperators = new List<string>(new string[] { "&&", "||" });

                if(prevInput3.name == "" && input.name == "if") {
                    prevInput3 = input;
                    return "Start Rule 3";
                } else if(prevInput3.name == "if" && input.name == "(") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(prevInput3.name == "(" && input.type == "variable") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(prevInput3.type == "variable" && compOperators.Contains(input.name)) {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(compOperators.Contains(prevInput3.name) && input.type == "number") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(compOperators.Contains(prevInput3.name) && input.type == "variable") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(prevInput3.type == "number" && boolOperators.Contains(input.name)) {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(prevInput3.type == "variable" && boolOperators.Contains(input.name)) {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(boolOperators.Contains(prevInput3.name) && input.type == "variable") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(prevInput3.type == "number" && input.name == ")") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(prevInput3.type == "variable" && input.name == ")") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(prevInput3.name == ")" && input.name == "{") {
                    prevInput3 = input;
                    return "Continue Rule 3";
                } else if(prevInput3.name == "{" && input.name == "}") {
                    prevInput3 = new Token();
                    return "Ok Rule 3";
                } else if(prevInput3.name == "{" && input.name != "") {
                    return "Continue Rule 3";
                }

                if(prevInput3.name == "if") {
                    prevInput3 = new Token();
                    return "Error Expected '(': Rule 3";
                }

                if(prevInput3.name == "(") {
                    prevInput3 = new Token();
                    return "Error Expected 'variable': Rule 3";
                }

                if(prevInput3.type == "variable") {
                    prevInput3 = new Token();
                    return "Error Expected 'comp_operator' or 'bool operator' or ')': Rule 3";
                }

                if(compOperators.Contains(prevInput3.name)) {
                    prevInput3 = new Token();
                    return "Error Expected 'number' or 'variable': Rule 3";
                }

                if(boolOperators.Contains(prevInput3.name)) {
                    prevInput3 = new Token();
                    return "Error Expected 'variable': Rule 3";
                }

                if(prevInput3.type == "number") {
                    prevInput3 = new Token();
                    return "Error Expected 'bool operator' or ')': Rule 3";
                }

                if(prevInput3.name == ")") {
                    prevInput3 = new Token();
                    return "Error Expected '{': Rule 3";
                }

                if(prevInput3.name == "{") {
                    prevInput3 = new Token();
                    return "Error Expected '}': Rule 3";
                }

                prevInput3 = new Token();
                return "Error Rule 3";
            }
            return errors;
        }
        private void btnRun_Click(object sender, EventArgs e) {
            txtConsole.Text = "";
            txtError.Text = "";
            var tokens = Scanner(Splitter(textEditor.Text));

            foreach(var s in tokens) {
                txtConsole.Text += s.name + ", " + s.type + "\r\n";
            }

            txtConsole.Text += "\r\n";
            var errors = SemanticAnalyzer(tokens);

            foreach(var s in errors) {
                txtError.Text += s + "\r\n";
            }

            DGVVE.Rows.Clear();

            for(int i = 0; i < Lvar.Count(); i++) {
                string[] row = { Lvar[i].name, Lvar[i].varValue.GetType().ToString(), Lvar[i].varValue + "" };
                if(Lvar[i].varValue + "" != "0") DGVVE.Rows.Add(row);
            }

            ct = 0;
            Lvar.Clear();
        }
    }
}