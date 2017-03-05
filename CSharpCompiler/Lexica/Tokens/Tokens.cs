using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpCompiler.Lexica.Tokens
{
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
        public static readonly Token FALSE = new Token("false", TokenTag.FALSE);
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
        public static readonly Token TRUE = new Token("true", TokenTag.TRUE);
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
        public static readonly Token BOOL = new Token("bool", TokenTag.BOOL);
        public static readonly Token BYTE = new Token("unsigned int8", TokenTag.BYTE);
        public static readonly Token CHAR = new Token("char", TokenTag.CHAR);
        public static readonly Token DECIMAL = new Token("decimal", TokenTag.DECIMAL);
        public static readonly Token DOUBLE = new Token("double", TokenTag.DOUBLE);
        public static readonly Token FLOAT = new Token("float", TokenTag.FLOAT);
        public static readonly Token INT = new Token("int32", TokenTag.INT);
        public static readonly Token LONG = new Token("int64", TokenTag.LONG);
        public static readonly Token OBJECT = new Token("object", TokenTag.OBJECT);
        public static readonly Token SBYTE = new Token("int8", TokenTag.SBYTE);
        public static readonly Token SHORT = new Token("int16", TokenTag.SHORT);
        public static readonly Token STRING = new Token("string", TokenTag.STRING);
        public static readonly Token USHORT = new Token("unsigned int16", TokenTag.USHORT);
        public static readonly Token UINT = new Token("unsigned int32", TokenTag.UINT);
        public static readonly Token ULONG = new Token("unsigned int64", TokenTag.ULONG);
        public static readonly Token VOID = new Token("void", TokenTag.VOID);
        
        // token fabric methods
        public static Func<string, Token> INT_CONST = val => new Token(val, TokenTag.INT_CONST);
        public static Func<string, Token> FLOAT_CONST = val => new Token(val, TokenTag.FLOAT_CONST);
        public static Func<string, Token> DOUBLE_CONST = val => new Token(val, TokenTag.DOUBLE_CONST);
        public static Func<string, Token> ID = val => new Token(val, TokenTag.ID);

        private static Dictionary<string, Token> _tokenMap = GetFieldMap<Token>();
        private static Dictionary<string, Func<string, Token>> _tokenFactoryMap = GetFieldMap<Func<string, Token>>();
        
        private static Dictionary<string, TField> GetFieldMap<TField>()
        {
            return Enum.GetNames(typeof(TokenTag))
                .Where(name => HasField<TField>(name))
                .ToDictionary(name => name, name => GetFieldValue<TField>(name));
        }
        
        private static bool HasField<TField>(string name)
        {
            var field = GetField(name);
            return (field == null) ? false : field.FieldType == typeof(TField);
        }

        private static FieldInfo GetField(string name)
        {
            return typeof(Tokens).GetField(name, BindingFlags.Public | BindingFlags.Static);
        }

        private static TField GetFieldValue<TField>(string name)
        {
            return (TField)GetField(name).GetValue(null);
        }

        public static Token GetToken(string alias, string lexeme)
        {
            Token token;
            if (_tokenMap.TryGetValue(alias, out token))
                return token;

            Func<string, Token> tokenFactory;
            if (_tokenFactoryMap.TryGetValue(alias, out tokenFactory))
                return tokenFactory(lexeme);
            
            throw new InvalidTokenException(alias);
        }
    }
}
