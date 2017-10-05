using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Data;
using ComicBookShared.Models;
using System.Data.Entity;


namespace ComicBookShared.Data
{
    public class ArtistRepository: BaseRepository<Artist>
    {
        public ArtistRepository(Context context):base(context)
        { }
        public override Artist Get(int id, bool includeRelatedEntities = true)
        {
            var artist = Context.Artists.AsQueryable();
            if (includeRelatedEntities)
            {
                artist = artist.Include(s => s.ComicBooks.Select(a=>a.ComicBook.Series))
                               .Include(s => s.ComicBooks.Select(a=>a.Role));
            }
            return artist.Where(cb => cb.Id == id).SingleOrDefault();
        }

        public override IList<Artist> GetList()
        {
            return Context.Artists.OrderBy(s => s.Name).ToList();
        }
        
        public bool ArtistExists(int artistId, string name)
        {
            return Context.Artists.Any(s => s.Id == artistId && s.Name == name);
        }
    }
}
