using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szotar.Models
{
    // Egy szó lefordításakor kapott eredmény Modellje, egy az APIból vett JSON példa által létrehozott osztály (Paste JSON as class)
    public class WordResult
    {
        public Head Head { get; set; }
        public List<Def> Def { get; set; }
    }

    public class Head
    {
    }

    public class Def
    {
        public string Text { get; set; }
        public string Pos { get; set; }
        public List<Tr> Tr { get; set; }
    }

    public class Tr
    {
        public string Text { get; set; }
        public string Pos { get; set; }
        public List<Syn> Syn { get; set; }
        public List<Mean> Mean { get; set; }
        public List<Ex> Ex { get; set; }
    }

    public class Syn
    {
        public string Text { get; set; }
    }

    public class Mean
    {
        public string Text { get; set; }
    }

    public class Ex
    {
        public string Text { get; set; }
        public List<Tr1> Tr { get; set; }
    }

    public class Tr1
    {
        public string Text { get; set; }
    }

}
