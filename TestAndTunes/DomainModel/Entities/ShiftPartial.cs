using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndTunes.DomainModel.Entities
{
    public partial class Shift : IComparable<Shift>,IComparable
    {
        int IComparable.CompareTo(object obj)
        {
            var shift = obj as Shift;
            return this.CompareTo(shift);
        }

        public int CompareTo(Shift other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}
