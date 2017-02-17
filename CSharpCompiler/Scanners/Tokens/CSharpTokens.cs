using System;

namespace CSharpCompiler.Scanners.Tokens
{
    /// <summary>
    /// Basic tokens for csharp language
    /// </summary>
    public static class CSharpTokens
    {
        private static int _tokenCounter = Enum.GetValues(typeof(CSharpTokenTag)).Length + 1;

        // arithmetic operations
        public static readonly Token PLUS = new Token("+", (int)CSharpTokenTag.PLUS);
        public static readonly Token MINUS = new Token("-", (int)CSharpTokenTag.MINUS);
        public static readonly Token MULTIPLY = new Token("*", (int)CSharpTokenTag.MULTIPLY);
        public static readonly Token DIVIDE = new Token("/", (int)CSharpTokenTag.DIVIDE);
        public static readonly Token MOD = new Token("%", (int)CSharpTokenTag.MOD);

        // binary operators
        public static readonly Token LEFT_SHIFT = new Token("<<", (int)CSharpTokenTag.LEFT_SHIFT);
        public static readonly Token RIGHT_SHIFT = new Token(">>", (int)CSharpTokenTag.RIGHT_SHIFT);
        public static readonly Token BIT_OR = new Token("|", (int)CSharpTokenTag.BIT_OR);
        public static readonly Token BIT_AND = new Token("&", (int)CSharpTokenTag.BIT_AND);
        public static readonly Token BIT_XOR = new Token("^", (int)CSharpTokenTag.BIT_XOR);

        // assignment operators
        public static readonly Token ASSIGN = new Token("=", (int)CSharpTokenTag.ASSIGN);
        public static readonly Token PLUS_ASSIGN = new Token("+=", (int)CSharpTokenTag.PLUS_ASSIGN);
        public static readonly Token MINUS_ASSIGN = new Token("-=", (int)CSharpTokenTag.MINUS_ASSIGN);
        public static readonly Token MULTIPLY_ASSIGN = new Token("*=", (int)CSharpTokenTag.MULTIPLY_ASSIGN);
        public static readonly Token DIVIDE_ASSIGN = new Token("/=", (int)CSharpTokenTag.DIVIDE_ASSIGN);
        public static readonly Token MOD_ASSIGN = new Token("%=", (int)CSharpTokenTag.MOD_ASSIGN);
        public static readonly Token LEFT_SHIFT_ASSIGN = new Token("<<=", (int)CSharpTokenTag.LEFT_SHIFT_ASSIGN);
        public static readonly Token RIGHT_SHIFT_ASSIGN = new Token(">>=", (int)CSharpTokenTag.RIGHT_SHIFT_ASSIGN);
        public static readonly Token BIT_OR_ASSIGN = new Token("|=", (int)CSharpTokenTag.BIT_OR_ASSIGN);
        public static readonly Token BIT_AND_ASSIGN = new Token("&=", (int)CSharpTokenTag.BIT_AND_ASSIGN);
        public static readonly Token BIT_XOR_ASSIGN = new Token("^=", (int)CSharpTokenTag.BIT_XOR_ASSIGN);

        // relation operators
        public static readonly Token LESS = new Token("<", (int)CSharpTokenTag.LESS);
        public static readonly Token LESS_OR_EQUAL = new Token("<=", (int)CSharpTokenTag.LESS_OR_EQUAL);
        public static readonly Token EQUAL = new Token("==", (int)CSharpTokenTag.EQUAL);
        public static readonly Token NOT_EQUAL = new Token("!=", (int)CSharpTokenTag.NOT_EQUAL);
        public static readonly Token GREATER = new Token(">", (int)CSharpTokenTag.GREATER);
        public static readonly Token GREATER_OR_EQUAL = new Token(">=", (int)CSharpTokenTag.GREATER_OR_EQUAL);

        // logical operators
        public static readonly Token NOT = new Token("!", (int)CSharpTokenTag.NOT);
        public static readonly Token AND = new Token("&&", (int)CSharpTokenTag.AND);
        public static readonly Token OR = new Token("||", (int)CSharpTokenTag.OR);

        // special symbols
        public static readonly Token IMPLEMENTATION = new Token("=>", (int)CSharpTokenTag.IMPLEMENTATION);
        public static readonly Token ROUND_BRACE_OPEN = new Token("(", (int)CSharpTokenTag.ROUND_BRACE_OPEN);
        public static readonly Token ROUND_BRACE_CLOSE = new Token(")", (int)CSharpTokenTag.ROUND_BRACE_CLOSE);
        public static readonly Token CURLY_BRACE_OPEN = new Token("{", (int)CSharpTokenTag.CURLY_BRACE_OPEN);
        public static readonly Token CURLY_BRACE_CLOSE = new Token("}", (int)CSharpTokenTag.CURLY_BRACE_CLOSE);
        public static readonly Token SQUARE_BRACE_OPEN = new Token("[", (int)CSharpTokenTag.SQUARE_BRACE_OPEN);
        public static readonly Token SQUARE_BRACE_CLOSE = new Token("]", (int)CSharpTokenTag.SQUARE_BRACE_CLOSE);
        public static readonly Token INCREMENT = new Token("++", (int)CSharpTokenTag.INCREMENT);
        public static readonly Token DECREMENT = new Token("--", (int)CSharpTokenTag.DECREMENT);
        public static readonly Token COLON = new Token(":", (int)CSharpTokenTag.COLON);
        public static readonly Token SEMICOLON = new Token(";", (int)CSharpTokenTag.SEMICOLON);
        public static readonly Token DOT = new Token(".", (int)CSharpTokenTag.DOT);
        public static readonly Token COMMA = new Token(",", (int)CSharpTokenTag.COMMA);
        public static readonly Token QUESTION = new Token("?", (int)CSharpTokenTag.QUESTION);

        // reserved keywords
        public static readonly Token ABSTRACT = new Token("abstract", (int)CSharpTokenTag.ABSTRACT);
        public static readonly Token AS = new Token("as", (int)CSharpTokenTag.AS);
        public static readonly Token BASE = new Token("base", (int)CSharpTokenTag.BASE);
        public static readonly Token BREAK = new Token("break", (int)CSharpTokenTag.BREAK);
        public static readonly Token CASE = new Token("case", (int)CSharpTokenTag.CASE);
        public static readonly Token CATCH = new Token("catch", (int)CSharpTokenTag.CATCH);
        public static readonly Token CHECKED = new Token("checked", (int)CSharpTokenTag.CHECKED);
        public static readonly Token CLASS = new Token("class", (int)CSharpTokenTag.CLASS);
        public static readonly Token CONST = new Token("const", (int)CSharpTokenTag.CONST);
        public static readonly Token CONTINUE = new Token("continue", (int)CSharpTokenTag.CONTINUE);
        public static readonly Token DEFAULT = new Token("default", (int)CSharpTokenTag.DEFAULT);
        public static readonly Token DELEGATE = new Token("delegate", (int)CSharpTokenTag.DELEGATE);
        public static readonly Token DO = new Token("do", (int)CSharpTokenTag.DO);
        public static readonly Token ELSE = new Token("else", (int)CSharpTokenTag.ELSE);
        public static readonly Token ENUM = new Token("enum", (int)CSharpTokenTag.ENUM);
        public static readonly Token EVENT = new Token("event", (int)CSharpTokenTag.EVENT);
        public static readonly Token EXPLICIT = new Token("explicit", (int)CSharpTokenTag.EXPLICIT);
        public static readonly Token EXTERN = new Token("extern", (int)CSharpTokenTag.EXTERN);
        public static readonly Token FINALLY = new Token("finally", (int)CSharpTokenTag.FINALLY);
        public static readonly Token FIXED = new Token("fixed", (int)CSharpTokenTag.FIXED);
        public static readonly Token FOR = new Token("for", (int)CSharpTokenTag.FOR);
        public static readonly Token FOREACH = new Token("foreach", (int)CSharpTokenTag.FOREACH);
        public static readonly Token GET = new Token("get", (int)CSharpTokenTag.GET);
        public static readonly Token GOTO = new Token("goto", (int)CSharpTokenTag.GOTO);
        public static readonly Token IF = new Token("if", (int)CSharpTokenTag.IF);
        public static readonly Token IMPLICIT = new Token("implicit", (int)CSharpTokenTag.IMPLICIT);
        public static readonly Token IN = new Token("in", (int)CSharpTokenTag.IN);
        public static readonly Token INTERFACE = new Token("interface", (int)CSharpTokenTag.INTERFACE);
        public static readonly Token INTERNAL = new Token("internal", (int)CSharpTokenTag.INTERNAL);
        public static readonly Token IS = new Token("is", (int)CSharpTokenTag.IS);
        public static readonly Token LOCK = new Token("lock", (int)CSharpTokenTag.LOCK);
        public static readonly Token NAMESPACE = new Token("namespace", (int)CSharpTokenTag.NAMESPACE);
        public static readonly Token NEW = new Token("new", (int)CSharpTokenTag.NEW);
        public static readonly Token NULL = new Token("null", (int)CSharpTokenTag.NULL);
        public static readonly Token OPERATOR = new Token("operator", (int)CSharpTokenTag.OPERATOR);
        public static readonly Token OUT = new Token("out", (int)CSharpTokenTag.OUT);
        public static readonly Token OVERRIDE = new Token("override", (int)CSharpTokenTag.OVERRIDE);
        public static readonly Token PARAMS = new Token("params", (int)CSharpTokenTag.PARAMS);
        public static readonly Token PARTIAL = new Token("partial", (int)CSharpTokenTag.PARTIAL);
        public static readonly Token PRIVATE = new Token("private", (int)CSharpTokenTag.PRIVATE);
        public static readonly Token PROTECTED = new Token("protected", (int)CSharpTokenTag.PROTECTED);
        public static readonly Token PUBLIC = new Token("public", (int)CSharpTokenTag.PUBLIC);
        public static readonly Token READONLY = new Token("readonly", (int)CSharpTokenTag.READONLY);
        public static readonly Token REF = new Token("ref", (int)CSharpTokenTag.REF);
        public static readonly Token RETURN = new Token("return", (int)CSharpTokenTag.RETURN);
        public static readonly Token SEALED = new Token("sealed", (int)CSharpTokenTag.SEALED);
        public static readonly Token SET = new Token("set", (int)CSharpTokenTag.SET);
        public static readonly Token SIZEOF = new Token("sizeof", (int)CSharpTokenTag.SIZEOF);
        public static readonly Token STACKALLOC = new Token("stackalloc", (int)CSharpTokenTag.STACKALLOC);
        public static readonly Token STATIC = new Token("static", (int)CSharpTokenTag.STATIC);
        public static readonly Token STRUCT = new Token("struct", (int)CSharpTokenTag.STRUCT);
        public static readonly Token SWITCH = new Token("switch", (int)CSharpTokenTag.SWITCH);
        public static readonly Token THIS = new Token("this", (int)CSharpTokenTag.THIS);
        public static readonly Token THROW = new Token("throw", (int)CSharpTokenTag.THROW);
        public static readonly Token TRY = new Token("try", (int)CSharpTokenTag.TRY);
        public static readonly Token TYPEOF = new Token("typeof", (int)CSharpTokenTag.TYPEOF);
        public static readonly Token UNCHECKED = new Token("unchecked", (int)CSharpTokenTag.UNCHECKED);
        public static readonly Token UNSAFE = new Token("unsafe", (int)CSharpTokenTag.UNSAFE);
        public static readonly Token USING = new Token("using", (int)CSharpTokenTag.USING);
        public static readonly Token VAR = new Token("var", (int)CSharpTokenTag.VAR);
        public static readonly Token VIRTUAL = new Token("virtual", (int)CSharpTokenTag.VIRTUAL);
        public static readonly Token VOLATILE = new Token("volatile", (int)CSharpTokenTag.VOLATILE);
        public static readonly Token WHILE = new Token("while", (int)CSharpTokenTag.WHILE);

        // type tokens
        public static readonly TypeToken BOOL = new TypeToken("bool", (int)CSharpTokenTag.BOOL, 1);
        public static readonly TypeToken BYTE = new TypeToken("unsigned int8", (int)CSharpTokenTag.BYTE, 1);
        public static readonly TypeToken CHAR = new TypeToken("char", (int)CSharpTokenTag.CHAR, 1);
        public static readonly TypeToken DECIMAL = new TypeToken("decimal", (int)CSharpTokenTag.DECIMAL, 8);
        public static readonly TypeToken DOUBLE = new TypeToken("double", (int)CSharpTokenTag.DOUBLE, 8);
        public static readonly TypeToken FLOAT = new TypeToken("float", (int)CSharpTokenTag.FLOAT, 8);
        public static readonly TypeToken INT = new TypeToken("int32", (int)CSharpTokenTag.INT, 4);
        public static readonly TypeToken LONG = new TypeToken("int64", (int)CSharpTokenTag.LONG, 8);
        public static readonly TypeToken OBJECT = new TypeToken("object", (int)CSharpTokenTag.OBJECT, 8);
        public static readonly TypeToken SBYTE = new TypeToken("int8", (int)CSharpTokenTag.SBYTE, 1);
        public static readonly TypeToken SHORT = new TypeToken("int16", (int)CSharpTokenTag.SHORT, 2);
        public static readonly TypeToken STRING = new TypeToken("string", (int)CSharpTokenTag.STRING, 8);
        public static readonly TypeToken USHORT = new TypeToken("unsigned int16", (int)CSharpTokenTag.USHORT, 2);
        public static readonly TypeToken UINT = new TypeToken("unsigned int32", (int)CSharpTokenTag.UINT, 4);
        public static readonly TypeToken ULONG = new TypeToken("unsigned int64", (int)CSharpTokenTag.ULONG, 8);
        public static readonly TypeToken VOID = new TypeToken("void", (int)CSharpTokenTag.VOID, 0);

        // constan tokens
        public static readonly BoolToken TRUE = new BoolToken(true, (int)CSharpTokenTag.TRUE);
        public static readonly BoolToken FALSE = new BoolToken(false, (int)CSharpTokenTag.FALSE);

        // token fabric methods
        public static Func<string, IntegerToken> INT_CONST = val => new IntegerToken(val);
        public static Func<string, FloatToken> FLOAT_CONST = val => new FloatToken(val);
        public static Func<string, DoubleToken> DOUBLE_CONST = val => new DoubleToken(val);
        public static Func<string, Token> ID = val => new Token(val, (int)CSharpTokenTag.ID);

        public static Token GetToken(string alias, string lexeme)
        {
            switch (alias)
            {
                case "PLUS":
                    return PLUS;
                case "MINUS":
                    return MINUS;
                case "MULTIPLY":
                    return MULTIPLY;
                case "DIVIDE":
                    return DIVIDE;
                case "MOD":
                    return MOD;
                case "LEFT_SHIFT":
                    return LEFT_SHIFT;
                case "RIGHT_SHIFT":
                    return RIGHT_SHIFT;
                case "BIT_OR":
                    return BIT_OR;
                case "BIT_AND":
                    return BIT_AND;
                case "BIT_XOR":
                    return BIT_XOR;
                case "ASSIGN":
                    return ASSIGN;
                case "PLUS_ASSIGN":
                    return PLUS_ASSIGN;
                case "MINUS_ASSIGN":
                    return MINUS_ASSIGN;
                case "MULTIPLY_ASSIGN":
                    return MULTIPLY_ASSIGN;
                case "DIVIDE_ASSIGN":
                    return DIVIDE_ASSIGN;
                case "MOD_ASSIGN":
                    return MOD_ASSIGN;
                case "LEFT_SHIFT_ASSIGN":
                    return LEFT_SHIFT_ASSIGN;
                case "RIGHT_SHIFT_ASSIGN":
                    return RIGHT_SHIFT_ASSIGN;
                case "BIT_OR_ASSIGN":
                    return BIT_OR_ASSIGN;
                case "BIT_AND_ASSIGN":
                    return BIT_AND_ASSIGN;
                case "BIT_XOR_ASSIGN":
                    return BIT_XOR_ASSIGN;
                case "LESS":
                    return LESS;
                case "LESS_OR_EQUAL":
                    return LESS_OR_EQUAL;
                case "EQUAL":
                    return EQUAL;
                case "NOT_EQUAL":
                    return NOT_EQUAL;
                case "GREATER":
                    return GREATER;
                case "GREATER_OR_EQUAL":
                    return GREATER_OR_EQUAL;
                case "NOT":
                    return NOT;
                case "AND":
                    return AND;
                case "OR":
                    return OR;
                case "ABSTRACT":
                    return ABSTRACT;
                case "AS":
                    return AS;
                case "BASE":
                    return BASE;
                case "BOOL":
                    return BOOL;
                case "BREAK":
                    return BREAK;
                case "BYTE":
                    return BYTE;
                case "CASE":
                    return CASE;
                case "CATCH":
                    return CATCH;
                case "CHAR":
                    return CHAR;
                case "CHECKED":
                    return CHECKED;
                case "CLASS":
                    return CLASS;
                case "CONST":
                    return CONST;
                case "CONTINUE":
                    return CONTINUE;
                case "DECIMAL":
                    return DECIMAL;
                case "DEFAULT":
                    return DEFAULT;
                case "DELEGATE":
                    return DELEGATE;
                case "DO":
                    return DO;
                case "DOUBLE":
                    return DOUBLE;
                case "ELSE":
                    return ELSE;
                case "ENUM":
                    return ENUM;
                case "EVENT":
                    return EVENT;
                case "EXPLICIT":
                    return EXPLICIT;
                case "EXTERN":
                    return EXTERN;
                case "FALSE":
                    return FALSE;
                case "FINALLY":
                    return FINALLY;
                case "FIXED":
                    return FIXED;
                case "FLOAT":
                    return FLOAT;
                case "FOR":
                    return FOR;
                case "FOREACH":
                    return FOREACH;
                case "GET":
                    return GET;
                case "GOTO":
                    return GOTO;
                case "IF":
                    return IF;
                case "IMPLICIT":
                    return IMPLICIT;
                case "IN":
                    return IN;
                case "INT":
                    return INT;
                case "INTERFACE":
                    return INTERFACE;
                case "INTERNAL":
                    return INTERNAL;
                case "IS":
                    return IS;
                case "LOCK":
                    return LOCK;
                case "LONG":
                    return LONG;
                case "NAMESPACE":
                    return NAMESPACE;
                case "NEW":
                    return NEW;
                case "NULL":
                    return NULL;
                case "OBJECT":
                    return OBJECT;
                case "OPERATOR":
                    return OPERATOR;
                case "OUT":
                    return OUT;
                case "OVERRIDE":
                    return OVERRIDE;
                case "PARAMS":
                    return PARAMS;
                case "PARTIAL":
                    return PARTIAL;
                case "PRIVATE":
                    return PRIVATE;
                case "PROTECTED":
                    return PROTECTED;
                case "PUBLIC":
                    return PUBLIC;
                case "READONLY":
                    return READONLY;
                case "REF":
                    return REF;
                case "RETURN":
                    return RETURN;
                case "SBYTE":
                    return SBYTE;
                case "SEALED":
                    return SEALED;
                case "SET":
                    return SET;
                case "SHORT":
                    return SHORT;
                case "SIZEOF":
                    return SIZEOF;
                case "STACKALLOC":
                    return STACKALLOC;
                case "STATIC":
                    return STATIC;
                case "STRING":
                    return STRING;
                case "STRUCT":
                    return STRUCT;
                case "SWITCH":
                    return SWITCH;
                case "THIS":
                    return THIS;
                case "THROW":
                    return THROW;
                case "TRUE":
                    return TRUE;
                case "TRY":
                    return TRY;
                case "TYPEOF":
                    return TYPEOF;
                case "UINT":
                    return UINT;
                case "ULONG":
                    return ULONG;
                case "UNCHECKED":
                    return UNCHECKED;
                case "UNSAFE":
                    return UNSAFE;
                case "USHORT":
                    return USHORT;
                case "USING":
                    return USING;
                case "VAR":
                    return VAR;
                case "VIRTUAL":
                    return VIRTUAL;
                case "VOID":
                    return VOID;
                case "VOLATILE":
                    return VOLATILE;
                case "WHILE":
                    return WHILE;
                case "IMPLEMENTATION":
                    return IMPLEMENTATION;
                case "ROUND_BRACE_OPEN":
                    return ROUND_BRACE_OPEN;
                case "ROUND_BRACE_CLOSE":
                    return ROUND_BRACE_CLOSE;
                case "CURLY_BRACE_OPEN":
                    return CURLY_BRACE_OPEN;
                case "CURLY_BRACE_CLOSE":
                    return CURLY_BRACE_CLOSE;
                case "SQUARE_BRACE_OPEN":
                    return SQUARE_BRACE_OPEN;
                case "SQUARE_BRACE_CLOSE":
                    return SQUARE_BRACE_CLOSE;
                case "DOT":
                    return DOT;
                case "COMMA":
                    return COMMA;
                case "INCREMENT":
                    return INCREMENT;
                case "DECREMENT":
                    return DECREMENT;
                case "QUESTION":
                    return QUESTION;
                case "COLON":
                    return COLON;
                case "SEMICOLON":
                    return SEMICOLON;
                case "INT_CONST":
                    return INT_CONST(lexeme);
                case "FLOAT_CONST":
                    return FLOAT_CONST(lexeme);
                case "DOUBLE_CONST":
                    return DOUBLE_CONST(lexeme);
                case "ID":
                    return ID(lexeme);
            }

            return new Token(lexeme, ++_tokenCounter);
        }
    }
}
