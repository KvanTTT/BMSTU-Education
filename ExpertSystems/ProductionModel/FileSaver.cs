using System.IO;

namespace ProductModel
{
    class FileSaver : Saver
    {
        string FileName;

        public FileSaver(string FileName)
        {
            this.FileName = FileName;
        }

        public override void Save(KnowledgeBase KB)
        {
            FileStream FS = new FileStream(FileName, FileMode.Create);
            StreamWriter SW = new StreamWriter(FS);

            foreach (EBCell Cell in KB.BaseOfFacts)
                SW.WriteLine(Cell.Value.ToString());
            SW.WriteLine('#');
            foreach (EBCell Cell in KB.BaseOfRules)
                SW.WriteLine(((Rule)Cell.Value).IfExpression.ToString() + " :- " + ((Rule)Cell.Value).ThenExpression.ToString());
            SW.Close();
        }
    }
}
