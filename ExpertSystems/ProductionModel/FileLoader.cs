using System;
using System.IO;
using System.Text;

namespace ProductModel
{
    class FileLoader : Loader
    {
        private string FileName;

        public FileLoader(string FileName)
        {
            this.FileName = FileName;
        }

        public override KnowledgeBase Load()
        {
            string Content = (new StreamReader(FileName, Encoding.Default)).ReadToEnd();

            char[] sep1 = { '#' };
            char[] sep2 = { '\n' , '\r' };
            string[] sep3 = { ":-" };

            string[] Knowledges = Content.Split(sep1, StringSplitOptions.RemoveEmptyEntries);
            string[] Facts = Knowledges[0].Split(sep2, StringSplitOptions.RemoveEmptyEntries);
            string[] Rules = Knowledges[1].Split(sep2, StringSplitOptions.RemoveEmptyEntries);
            string[] OneRule;

            KnowledgeBase KB = new KnowledgeBase();
            foreach (string StrFact in Facts)
                KB.AddFact(Translator.StringToFact(StrFact));
            foreach (string StrRule in Rules)
            {
                OneRule = StrRule.Split(sep3, StringSplitOptions.RemoveEmptyEntries);
                KB.AddRule(Translator.StringsToRule(OneRule[0], OneRule[1]));
            }

            return KB;
        }
    }
}
