using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Token
{
    public string value { get; private set; }
    public int type { get; private set; }

    public Token(String value, int type)
    {
        this.value = value;
        this.type = type;
    }
}