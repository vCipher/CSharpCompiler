﻿namespace CSharpCompiler.Lexica.Tokens
{
    public enum TokenTag
    {
        UNKNOWN,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        MOD,
        LEFT_SHIFT,
        RIGHT_SHIFT,
        BIT_OR,
        BIT_AND,
        BIT_XOR,
        ASSIGN,
        PLUS_ASSIGN,
        MINUS_ASSIGN,
        MULTIPLY_ASSIGN,
        DIVIDE_ASSIGN,
        MOD_ASSIGN,
        LEFT_SHIFT_ASSIGN,
        RIGHT_SHIFT_ASSIGN,
        BIT_OR_ASSIGN,
        BIT_AND_ASSIGN,
        BIT_XOR_ASSIGN,
        LESS,
        LESS_OR_EQUAL,
        EQUAL,
        NOT_EQUAL,
        GREATER,
        GREATER_OR_EQUAL,
        NOT,
        AND,
        OR,
        ABSTRACT,
        AS,
        BASE,
        BOOL,
        BREAK,
        BYTE,
        CASE,
        CATCH,
        CHAR,
        CHECKED,
        CLASS,
        CONST,
        CONTINUE,
        DECIMAL,
        DEFAULT,
        DELEGATE,
        DO,
        DOUBLE,
        ELSE,
        ENUM,
        EVENT,
        EXPLICIT,
        EXTERN,
        FALSE,
        FINALLY,
        FIXED,
        FLOAT,
        FOR,
        FOREACH,
        GET,
        GOTO,
        IF,
        IMPLICIT,
        IN,
        INT,
        INTERFACE,
        INTERNAL,
        IS,
        LOCK,
        LONG,
        NAMESPACE,
        NEW,
        NULL,
        OBJECT,
        OPERATOR,
        OUT,
        OVERRIDE,
        PARAMS,
        PARTIAL,
        PRIVATE,
        PROTECTED,
        PUBLIC,
        READONLY,
        REF,
        RETURN,
        SBYTE,
        SEALED,
        SET,
        SHORT,
        SIZEOF,
        STACKALLOC,
        STATIC,
        STRING,
        STRUCT,
        SWITCH,
        THIS,
        THROW,
        TRUE,
        TRY,
        TYPEOF,
        UINT,
        ULONG,
        UNCHECKED,
        UNSAFE,
        USHORT,
        USING,
        VAR,
        VIRTUAL,
        VOID,
        VOLATILE,
        WHILE,
        IMPLICATION,
        OPEN_PAREN,
        CLOSE_PAREN,
        OPEN_CURLY_BRACE,
        CLOSE_CURLY_BRACE,
        OPEN_SQUARE_BRACE,
        CLOSE_SQUARE_BRACE,
        DOT,
        COMMA,
        INCREMENT,
        DECREMENT,
        QUESTION,
        COLON,
        SEMICOLON,
        INT_LITERAL,
        FLOAT_LITERAL,
        DOUBLE_LITERAL,
        STRING_LITERAL,
        ID,
        WHITE_SPACE,
        NEW_LINE,
        LINE_COMMENT,
        MULTI_LINE_COMMENT
    }
}
