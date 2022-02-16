using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaylist.Models
{
    public class MyPlaylistOdaberiVM
    {
        public class Row
        {
            public int PjesmaId { get; set; }
            public string NazivPjesme { get; set; }
            public string NazivIzvodjaca { get; set; }
            public string Url { get; set; }
            public int Ocjena { get; set; }
            public bool IsFavorit { get; set; }
            public string DatumUnosa { get; set; }
            public string DatumEditovanja { get; set; }
        }
        public List<Row> rows { get; set; }
        public int KategorijaId { get; set; }
        public string Kategorija { get; set; }

    }
}
