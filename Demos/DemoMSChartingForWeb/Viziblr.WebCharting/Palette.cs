using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Viziblr.WebCharting
{
    public class Palette : IEnumerable<PaletteItem>
    {
        private List<PaletteItem> colors;
        public string Name { get; set; }

        public Palette()
        {
            this.colors = new List<PaletteItem>();
        }

        public void Add(PaletteItem item)
        {
            this.colors.Add(item);
        }

        public void AddARGB(uint color)
        {
            var item = new PaletteItem(color);
            this.colors.Add(item);
        }

        public void AddARGB(uint color,uint secondarycolor)
        {
            var item = new PaletteItem(color,secondarycolor);
            this.colors.Add(item);
        }

        private PaletteItem GetItem(int i)
        {
            return this.colors[i%this.colors.Count];
        }

        public PaletteItem this[int index]
        {
            get { return this.GetItem(index);  }
        }

        public Palette Clone()
        {
            var o = new Palette();
            foreach (var item in this)
            {
                o.Add(item.Clone());
            }
            return o;
        }

        public IEnumerator<PaletteItem> GetEnumerator()
        {
            foreach (var i in this.colors)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}