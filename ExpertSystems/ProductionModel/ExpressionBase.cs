using System;
using System.Collections;
using System.Collections.Generic;

namespace ProductModel
{
    class EBCell
    {
        public Expression Value { get; set; }
        private bool deletemark = false;

        public bool DeleteMark { get { return deletemark; } private set { deletemark = value; } }

        public EBCell(Expression Value) { this.Value = Value; }

        public void MarkToDel() { deletemark = true; }
        public void UnMarkToDel() { deletemark = false; }
    }

    class ExpressionBase : IEnumerable
    {
        private List<EBCell> Items;        

        public EBCell this[int index] { get { return Items[index]; } }

        public int Count { get { return Items.Count; } }

        public ExpressionBase() { Items = new List<EBCell>(); }
        public ExpressionBase(List<EBCell> Items) { this.Items = Items; }

        IEnumerator IEnumerable.GetEnumerator() { return new EBC_Enumerator(Items); }

        public Expression FindExpression(Predicate<Expression> Pr)
        {
            EBCell Cell = Items.Find((cell) => Pr(cell.Value));
            return Cell == null ? null : Cell.Value; 
        }

        public void AddExpression(Expression Expr) { Items.Add(new EBCell(Expr)); }
        public void RemoveExpression(int Index) { Items.RemoveAt(Index); }
        public void RemoveAll(Predicate<EBCell> Pr) { Items.RemoveAll(Pr); }
        public void Clear() { Items.Clear(); }
    }

    class EBC_Enumerator : IEnumerator
    {
        private List<EBCell> Items;
        private int pos = -1;

        public EBC_Enumerator(List<EBCell> Items) { this.Items = Items; }

        public bool MoveNext() { return ++pos < Items.Count; }
        public void Reset() { pos = -1; }
        public object Current
        { 
            get 
            { 
                try { return Items[pos]; } 
                catch (IndexOutOfRangeException) { throw new InvalidOperationException(); } 
            } 
        }
    }
}
