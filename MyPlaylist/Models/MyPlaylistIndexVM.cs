using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaylist.Models
{
    public class MyPlaylistIndexVM
    {
        public int KategorijaId { get; set; }
        public List<SelectListItem> Kategorija { get; set; }
    }
}
