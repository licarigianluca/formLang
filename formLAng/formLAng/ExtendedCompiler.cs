using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExtendedCompiler : Compiler {
    public ExtendedCompiler() : base() { }

    protected override string compileTypeTail(TypeTail tt) {
        if (tt == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        if (tt.e != null) {
            output.Append(string.Format(", \"tag\" : \"input\" , \"readOnly\" : true , \"expr\" : \"{0}\"", 
                compileExpr(tt.e)));
        } else {
            try { output.Append(string.Format(", \"tag\" : \"select\" , \"list\" : {{{0}}} , \"target\" : \"{1}\"",
                compileSelectList(tt.sl), tt.id)); }
            catch (ArgumentNullException) { output.Append(string.Format(", \"tag\" : \"select\" , \"list\" : \"null\" , \"target\" : \"{0}\"",
                tt.id)); }
        }
        return output.ToString();
    }
    protected string compileSelectList(SelectList sl) {
        if (sl == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        output.Append(compileSelectListHead(sl.slh));
        try { output.Append(compileSelectListTail(sl.slt)); }
        catch (ArgumentNullException) { }
        return output.ToString();

    }
    protected string compileSelectListHead(SelectListHead slh) {
        StringBuilder output = new StringBuilder();
        output.Append(compileSelectType(slh.st1));
        output.Append(slh.colon.value);
        output.Append(compileSelectType(slh.st2));
        return output.ToString();
    }
    protected string compileSelectListTail(SelectListTail slt) {
        if (slt == null) throw new ArgumentNullException();
        StringBuilder output = new StringBuilder();
        output.Append(slt.comma.value);
        output.Append(compileSelectListHead(slt.slh));
        try { output.Append(compileSelectListTail(slt.slt)); }
        catch (ArgumentNullException) { }
        return output.ToString();
    }
    protected string compileSelectType(SelectType st) {
        StringBuilder output = new StringBuilder();
        if (st.type == "ID") {
            output.Append(st.id);
        } else if (st.type == "INTEGER") {
            output.Append(st.intValue);
        } else {
            output.Append(st.doubleValue);
        }
        return output.ToString();
    }
}

