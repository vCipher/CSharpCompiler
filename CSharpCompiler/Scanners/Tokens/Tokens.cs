using System;

namespace CSharpCompiler.Scanners.Tokens
{
    /// <summary>
    /// Basic tokens for csharp language
    /// </summary>
    public static class Tokens
    {
        public static readonly Token EOF = new Token("eof", TokenTag.EOF);

        // arithmetic operations
        public static readonly Token PLUS = new Token("+", TokenTag.PLUS);
        public static readonly Token MINUS = new Token("-", TokenTag.MINUS);
        public static readonly Token MULTIPLY = new Token("*", TokenTag.MULTIPLY);
        public static readonly Token DIVIDE = new Token("/", TokenTag.DIVIDE);
        public static readonly Token MOD = new Token("%", TokenTag.MOD);

        // binary operators
        public static readonly Token LEFT_SHIFT = new Token("<<", TokenTag.LEFT_SHIFT);
        public static readonly Token RIGHT_SHIFT = new Token(">>", TokenTag.RIGHT_SHIFT);
        public static readonly Token BIT_OR = new Token("|", TokenTag.BIT_OR);
        public static readonly Token BIT_AND = new Token("&", TokenTag.BIT_AND);
        public static readonly Token BIT_XOR = new Token("^", TokenTag.BIT_XOR);

        // assignment operators
        public static readonly Token ASSIGN = new Token("=", TokenTag.ASSIGN);
        public static readonly Token PLUS_ASSIGN = new Token("+=", TokenTag.PLUS_ASSIGN);
        public static readonly Token MINUS_ASSIGN = new Token("-=", TokenTag.MINUS_ASSIGN);
        public static readonly Token MULTIPLY_ASSIGN = new Token("*=", TokenTag.MULTIPLY_ASSIGN);
        public static readonly Token DIVIDE_ASSIGN = new Token("/=", TokenTag.DIVIDE_ASSIGN);
        public static readonly Token MOD_ASSIGN = new Token("%=", TokenTag.MOD_ASSIGN);
        public static readonly Token LEFT_SHIFT_ASSIGN = new Token("<<=", TokenTag.LEFT_SHIFT_ASSIGN);
        public static readonly Token RIGHT_SHIFT_ASSIGN = new Token(">>=", TokenTag.RIGHT_SHIFT_ASSIGN);
        public static readonly Token BIT_OR_ASSIGN = new Token("|=", TokenTag.BIT_OR_ASSIGN);
        public static readonly Token BIT_AND_ASSIGN = new Token("&=", TokenTag.BIT_AND_ASSIGN);
        public static readonly Token BIT_XOR_ASSIGN = new Token("^=", TokenTag.BIT_XOR_ASSIGN);

        // relation operators
        public static readonly Token LESS = new Token("<", TokenTag.LESS);
        public static readonly Token LESS_OR_EQUAL = new Token("<=", TokenTag.LESS_OR_EQUAL);
        public static readonly Token EQUAL = new Token("==", TokenTag.EQUAL);
        public static readonly Token NOT_EQUAL = new Token("!=", TokenTag.NOT_EQUAL);
        public static readonly Token GREATER = new Token(">", TokenTag.GREATER);
        public static readonly Token GREATER_OR_EQUAL = new Token(">=", TokenTag.GREATER_OR_EQUAL);

        // logical operators
        public static readonly Token NOT = new Token("!", TokenTag.NOT);
        public static readonly Token AND = new Token("&&", TokenTag.AND);
        public static readonly Token OR = new Token("||", TokenTag.OR);

        // special symbols
        public static readonly Token IMPLICATION = new Token("=>", TokenTag.IMPLICATION);
        public static readonly Token OPEN_PAREN = new Token("(", TokenTag.OPEN_PAREN);
        public static readonly Token CLOSE_PAREN = new Token(")", TokenTag.CLOSE_PAREN);
        public static readonly Token OPEN_CURLY_BRACE = new Token("{", TokenTag.OPEN_CURLY_BRACE);
        public static readonly Token CLOSE_CURLY_BRACE = new Token("}", TokenTag.CLOSE_CURLY_BRACE);
        public static readonly Token OPEN_SQUARE_BRACE = new Token("[", TokenTag.OPEN_SQUARE_BRACE);
        public static readonly Token CLOSE_SQUARE_BRACE = new Token("]", TokenTag.CLOSE_SQUARE_BRACE);
        public static readonly Token INCREMENT = new Token("++", TokenTag.INCREMENT);
        public static readonly Token DECREMENT = new Token("--", TokenTag.DECREMENT);
        public static readonly Token COLON = new Token(":", TokenTag.COLON);
        public static readonly Token SEMICOLON = new Token(";", TokenTag.SEMICOLON);
        public static readonly Token DOT = new Token(".", TokenTag.DOT);
        public static readonly Token COMMA = new Token(",", TokenTag.COMMA);
        public static readonly Token QUESTION = new Token("?", TokenTag.QUESTION);

        // reserved keywords
        public static readonly Token ABSTRACT = new Token("abstract", TokenTag.ABSTRACT);
        public static readonly Token AS = new Token("as", TokenTag.AS);
        public static readonly Token BASE = new Token("base", TokenTag.BASE);
        public static readonly Token BREAK = new Token("break", TokenTag.BREAK);
        public static readonly Token CASE = new Token("case", TokenTag.CASE);
        public static readonly Token CATCH = new Token("catch", TokenTag.CATCH);
        public static readonly Token CHECKED = new Token("checked", TokenTag.CHECKED);
        public static readonly Token CLASS = new Token("class", TokenTag.CLASS);
        public static readonly Token CONST = new Token("const", TokenTag.CONST);
        public static readonly Token CONTINUE = new Token("continue", TokenTag.CONTINUE);
        public static readonly Token DEFAULT = new Token("default", TokenTag.DEFAULT);
        public static readonly Token DELEGATE = new Token("delegate", TokenTag.DELEGATE);
        public static readonly Token DO = new Token("do", TokenTag.DO);
        public static readonly Token ELSE = new Token("else", TokenTag.ELSE);
        public static readonly Token ENUM = new Token("enum", TokenTag.ENUM);
        public static readonly Token EVENT = new Token("event", TokenTag.EVENT);
        public static readonly Token EXPLICIT = new Token("explicit", TokenTag.EXPLICIT);
        public static readonly Token EXTERN = new Token("extern", TokenTag.EXTERN);
        public static readonly Token FINALLY = new Token("finally", TokenTag.FINALLY);
        public static readonly Token FIXED = new Token("fixed", TokenTag.FIXED);
        public static readonly Token FOR = new Token("for", TokenTag.FOR);
        public static readonly Token FOREACH = new Token("foreach", TokenTag.FOREACH);
        public static readonly Token GET = new Token("get", TokenTag.GET);
        public static readonly Token GOTO = new Token("goto", TokenTag.GOTO);
        public static readonly Token IF = new Token("if", TokenTag.IF);
        public static readonly Token IMPLICIT = new Token("implicit", TokenTag.IMPLICIT);
        public static readonly Token IN = new Token("in", TokenTag.IN);
        public static readonly Token INTERFACE = new Token("interface", TokenTag.INTERFACE);
        public static readonly Token INTERNAL = new Token("internal", TokenTag.INTERNAL);
        public static readonly Token IS = new Token("is", TokenTag.IS);
        public static readonly Token LOCK = new Token("lock", TokenTag.LOCK);
        public static readonly Token NAMESPACE = new Token("namespace", TokenTag.NAMESPACE);
        public static readonly Token NEW = new Token("new", TokenTag.NEW);
        public static readonly Token NULL = new Token("null", TokenTag.NULL);
        public static readonly Token OPERATOR = new Token("operator", TokenTag.OPERATOR);
        public static readonly Token OUT = new Token("out", TokenTag.OUT);
        public static readonly Token OVERRIDE = new Token("override", TokenTag.OVERRIDE);
        public static readonly Token PARAMS = new Token("params", TokenTag.PARAMS);
        public static readonly Token PARTIAL = new Token("partial", TokenTag.PARTIAL);
        public static readonly Token PRIVATE = new Token("private", TokenTag.PRIVATE);
        public static readonly Token PROTECTED = new Token("protected", TokenTag.PROTECTED);
        public static readonly Token PUBLIC = new Token("public", TokenTag.PUBLIC);
        public static readonly Token READONLY = new Token("readonly", TokenTag.READONLY);
        public static readonly Token REF = new Token("ref", TokenTag.REF);
        public static readonly Token RETURN = new Token("return", TokenTag.RETURN);
        public static readonly Token SEALED = new Token("sealed", TokenTag.SEALED);
        public static readonly Token SET = new Token("set", TokenTag.SET);
        public static readonly Token SIZEOF = new Token("sizeof", TokenTag.SIZEOF);
        public static readonly Token STACKALLOC = new Token("stackalloc", TokenTag.STACKALLOC);
        public static readonly Token STATIC = new Token("static", TokenTag.STATIC);
        public static readonly Token STRUCT = new Token("struct", TokenTag.STRUCT);
        public static readonly Token SWITCH = new Token("switch", TokenTag.SWITCH);
        public static readonly Token THIS = new Token("this", TokenTag.THIS);
        public static readonly Token THROW = new Token("throw", TokenTag.THROW);
        public static readonly Token TRY = new Token("try", TokenTag.TRY);
        public static readonly Token TYPEOF = new Token("typeof", TokenTag.TYPEOF);
        public static readonly Token UNCHECKED = new Token("unchecked", TokenTag.UNCHECKED);
        public static readonly Token UNSAFE = new Token("unsafe", TokenTag.UNSAFE);
        public static readonly Token USING = new Token("using", TokenTag.USING);
        public static readonly Token VAR = new Token("var", TokenTag.VAR);
        public static readonly Token VIRTUAL = new Token("virtual", TokenTag.VIRTUAL);
        public static readonly Token VOLATILE = new Token("volatile", TokenTag.VOLATILE);
        public static readonly Token WHILE = new Token("while", TokenTag.WHILE);

        // type tokens
        public static readonly TypeToken BOOL = new TypeToken("bool", TokenTag.BOOL, 1);
        public static readonly TypeToken BYTE = new TypeToken("unsigned int8", TokenTag.BYTE, 1);
        public static readonly TypeToken CHAR = new TypeToken("char", TokenTag.CHAR, 1);
        public static readonly TypeToken DECIMAL = new TypeToken("decimal", TokenTag.DECIMAL, 8);
        public static readonly TypeToken DOUBLE = new TypeToken("double", TokenTag.DOUBLE, 8);
        public static readonly TypeToken FLOAT = new TypeToken("float", TokenTag.FLOAT, 8);
        public static readonly TypeToken INT = new TypeToken("int32", TokenTag.INT, 4);
        public static readonly TypeToken LONG = new TypeToken("int64", TokenTag.LONG, 8);
        public static readonly TypeToken OBJECT = new TypeToken("object", TokenTag.OBJECT, 8);
        public static readonly TypeToken SBYTE = new TypeToken("int8", TokenTag.SBYTE, 1);
        public static readonly TypeToken SHORT = new TypeToken("int16", TokenTag.SHORT, 2);
        public static readonly TypeToken STRING = new TypeToken("string", TokenTag.STRING, 8);
        public static readonly TypeToken USHORT = new TypeToken("unsigned int16", TokenTag.USHORT, 2);
        public static readonly TypeToken UINT = new TypeToken("unsigned int32", TokenTag.UINT, 4);
        public static readonly TypeToken ULONG = new TypeToken("unsigned int64", TokenTag.ULONG, 8);
        public static readonly TypeToken VOID = new TypeToken("void", TokenTag.VOID, 0);

        // constan tokens
        public static readonly BoolToken TRUE = new BoolToken(true, TokenTag.TRUE);
        public static readonly BoolToken FALSE = new BoolToken(false, TokenTag.FALSE);

        // token fabric methods
        public static Func<string, IntegerToken> INT_CONST = val => new IntegerToken(val);
        public static Func<string, FloatToken> FLOAT_CONST = val => new FloatToken(val);
        public static Func<string, DoubleToken> DOUBLE_CONST = val => new DoubleToken(val);
        public static Func<string, Token> ID = val => new Token(val, TokenTag.ID);

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
                case "IMPLICATION":
                    return IMPLICATION;
                case "OPEN_PAREN":
                    return OPEN_PAREN;
                case "CLOSE_PAREN":
                    return CLOSE_PAREN;
                case "OPEN_CURLY_BRACE":
                    return OPEN_CURLY_BRACE;
                case "CLOSE_CURLY_BRACE":
                    return CLOSE_CURLY_BRACE;
                case "OPEN_SQUARE_BRACE":
                    return OPEN_SQUARE_BRACE;
                case "CLOSE_SQUARE_BRACE":
                    return CLOSE_SQUARE_BRACE;
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

            throw new ScanException(string.Format("Unsupported token {0}", alias));
        }
    }
}
