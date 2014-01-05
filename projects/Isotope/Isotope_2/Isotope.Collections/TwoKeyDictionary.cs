using System.Collections.Generic;

namespace Isotope.Collections
{
    public class TwoKeyDictionary<PK, SK, V>
    {
        Dictionary<PK, Dictionary<SK, V>> dic_pk;

        public TwoKeyDictionary()
        {
            this.dic_pk = new Dictionary<PK, Dictionary<SK, V>>();
        }

        public bool ContainsPrimaryKey(PK pk)
        {
            return this.dic_pk.ContainsKey(pk);
        }

        public bool ContainsKey(PK pk, SK sk)
        {
            V v;
            bool haskey = this.TryGetValue(pk, sk, out v);
            return haskey;
        }

        public bool TryGetValue(PK pk, SK sk, out V v)
        {
            Dictionary<SK, V> sk_dic;
            bool has_pk = this.dic_pk.TryGetValue(pk, out sk_dic);
            if (!has_pk)
            {
                v = default(V);
                return false;
            }

            return sk_dic.TryGetValue(sk, out v);
        }

        public V GetValue(PK pk, SK sk)
        {
            V v;
            bool haskey = this.TryGetValue(pk, sk, out v);
            if (!haskey)
            {
                string msg = string.Format("(pk,sk) missing");
                throw new KeyNotFoundException(msg);
            }

            return v;
        }

        public void SetValue(PK pk, SK sk, V v)
        {
            Dictionary<SK, V> sk_dic;
            bool has_pk = this.dic_pk.TryGetValue(pk, out sk_dic);
            if (!has_pk)
            {
                sk_dic = new Dictionary<SK, V>();
                this.dic_pk[pk] = sk_dic;
            }

            sk_dic[sk] = v;
        }

        public V this[PK pk, SK sk]
        {
            get
            {
                return this.GetValue(pk, sk);
            }

            set
            {
                this.SetValue(pk, sk, value);
            }
        }

        public int Count
        {
            get
            {
                int n = 0;
                foreach (var i in this.dic_pk.Values)
                {
                    n += i.Count;
                }

                return n;
            }
        }
    }
}