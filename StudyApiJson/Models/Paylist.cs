using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Models;

namespace StudyApiJson.Models
{
    public class Paylist : ICollection<Musica>
    {
        List<Musica> musicas = new List<Musica>();
        public int Count => musicas.Count();

        public bool IsReadOnly => false;

        public void Clear()
        {
            musicas .Clear();
        }

        public void CopyTo(Musica[] array, int arrayIndex)
        {
            musicas.CopyTo(array, arrayIndex);
        }

        public IEnumerator GetEnumerator()
        {
            return musicas.GetEnumerator();
        }

        void ICollection<Musica>.Add(Musica item)
        {
            musicas.Add(item);
        }

        bool ICollection<Musica>.Contains(Musica item)
        {
            return musicas.Contains(item);
        }

        IEnumerator<Musica> IEnumerable<Musica>.GetEnumerator()
        {
            return musicas.GetEnumerator();
        }

        bool ICollection<Musica>.Remove(Musica item)
        {
            return musicas.Remove(item);
        }
    }
}