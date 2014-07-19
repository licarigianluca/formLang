using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum type {
    PLUS, TIMES, ID, INVALID_TOKEN, CLOSE_PAR, OPEN_PAR, EOF, INTEGER, SLASH, MINUS, NOT, COLON, OPEN_CURLY,
    CLOSE_CURLY, IF, ELSE, FORM, AND, OR, EQUAL, DISEQUAL, COMMA, HASHTAG, POWER, OPEN_SQUARE, CLOSE_SQUARE,
    BOOL, REAL, STRING, LT, LE, GT, GE, TYPE_INT, TYPE_REAL, TYPE_BOOL,TYPE_DATE, TYPE_MONEY, TYPE_STRING};

public class Tokenizer {
    private bool ignoreBlanks = true;
    private int idx;
    private String s;
    private LinkedList<Token> tokenList;
    private Dictionary<String, int> keyword = new Dictionary<string, int>(){
        {"true",(int)type.BOOL},{"false",(int)type.BOOL},{"if",(int)type.IF},{"form",(int)type.FORM},
        {"integer",(int)type.TYPE_INT},{"real",(int)type.TYPE_REAL},{"boolean",(int)type.TYPE_BOOL},
        {"date",(int)type.TYPE_DATE},{"money",(int)type.TYPE_MONEY},{"string",(int)type.TYPE_STRING}};
    public Tokenizer(String expr) {
        this.s = expr;
        this.idx = 0;
        this.tokenList = new LinkedList<Token>();
    }

    public Token nextToken() {

        String lexeme;
        Token t;

        if (idx >= s.Length) return new Token("EOF".ToString(), (int)type.EOF);
        if (s[idx] == ',') return new Token(s[idx++].ToString(), (int)type.COMMA);
        if (s[idx] == '^') return new Token(s[idx++].ToString(), (int)type.POWER);
        if (s[idx] == '+') return new Token(s[idx++].ToString(), (int)type.PLUS);
        if (s[idx] == '-') return new Token(s[idx++].ToString(), (int)type.MINUS);
        if (s[idx] == '*') return new Token(s[idx++].ToString(), (int)type.TIMES);
        if (s[idx] == '/') return new Token(s[idx++].ToString(), (int)type.SLASH);
        if (s[idx] == '(') return new Token(s[idx++].ToString(), (int)type.OPEN_PAR);
        if (s[idx] == ')') return new Token(s[idx++].ToString(), (int)type.CLOSE_PAR);
        if (s[idx] == '[') return new Token(s[idx++].ToString(), (int)type.OPEN_SQUARE);
        if (s[idx] == ']') return new Token(s[idx++].ToString(), (int)type.CLOSE_SQUARE);
        if (s[idx] == ':') return new Token(s[idx++].ToString(), (int)type.COLON);
        if (s[idx] == '}') return new Token(s[idx++].ToString(), (int)type.CLOSE_CURLY);
        if (s[idx] == '{') return new Token(s[idx++].ToString(), (int)type.OPEN_CURLY);
        if (s[idx] == '&') {
            if (s[idx + 1] == '&') {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.AND);
                idx += 2;
                ignoreBlanks = true;
            } else {
                ignoreBlanks = false;
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
        } else if (s[idx] == '|') {
            if (s[idx + 1] == '|') {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.OR);
                idx += 2;
                ignoreBlanks = true;
            } else {
                ignoreBlanks = false;
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
        } else if (s[idx] == '<') {
            if (s[idx + 1] == '=') {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.LE);
                idx += 2;
                ignoreBlanks = true;
            } else {
                t = new Token(s[idx++].ToString(), (int)type.LT);
            }
        } else if (s[idx] == '>') {
            if (s[idx + 1] == '=') {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.GE);
                idx += 2;
                ignoreBlanks = true;
            } else {
                t = new Token(s[idx++].ToString(), (int)type.GT);
            }

        } else if (s[idx] == '=') {
            if (s[idx + 1] == '=') {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.EQUAL);
                idx += 2;
                ignoreBlanks = true;
            } else {
                ignoreBlanks = false;
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
        } else if (s[idx] == '!') {
            if (s[idx + 1] == '=') {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.DISEQUAL);
                idx += 2;
                ignoreBlanks = true;
            } else {
                t = new Token(s[idx++].ToString(), (int)type.NOT);
            }
        } else if (s[idx] == '"') {

            lexeme = s[idx++].ToString();
            while (idx < s.Length && (isChar(s[idx]) || isDigit(s[idx]) || isBlank(s[idx])) && s[idx] != '"') {
                lexeme += s[idx++];
            }
            lexeme += s[idx++];
            t = new Token(lexeme.ToString(), (int)type.STRING);


        } else if (isDigit(s[idx])) {
            int dot = 0;
            lexeme = s[idx++].ToString();
            while (idx < s.Length && isDigit(s[idx])) {
                lexeme += s[idx++];
            }
            if (idx < s.Length - 1 && s[idx] == '.') {
                dot++;
                lexeme += s[idx++];
            }
            while (idx < s.Length && isDigit(s[idx])) {

                lexeme += s[idx++];
            }
            if (dot == 0) {
                t = new Token(lexeme.ToString(), (int)type.INTEGER);
            } else if (dot == 1) {
                t = new Token(lexeme.ToString(), (int)type.REAL);
            } else {
                t = new Token(lexeme.ToString(), (int)type.INVALID_TOKEN);
            }
        } else if (isChar(s[idx])) {
            lexeme = s[idx++].ToString();
            while (idx < s.Length && isChar(s[idx])) {
                lexeme += s[idx++];
            }
            if (keyword.ContainsKey(lexeme)) {
                t = new Token(lexeme.ToString(), keyword[lexeme]);
            } else {
                t = new Token(lexeme.ToString(), (int)type.ID);
            }
        } else {
            if (isBlank(s[idx]) && ignoreBlanks) {
                idx++;
                t = nextToken();
            } else {
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
        }

        return t;
    }
    private bool isDigit(char c) {return (c >= '0' && c <= '9');}
    private bool isChar(char c) {return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');}
    private bool isBlank(char c) {return (c != ' ') || (c != '\r') || (c != '\n');}
}