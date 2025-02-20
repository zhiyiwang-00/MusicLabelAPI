using MusicLabelApi.Data;
using MusicLabelApi.Models;


MusicDbContext dbContext = new MusicDbContext();
 
// dbContext.MusicLabels.Add(new MusicLabel { Name = "Universal Music", Description = "Leading global music company" });
// dbContext.MusicLabels.Add(new MusicLabel { Name = "Sony Music", Description = "Worldwide music corporation" });
// dbContext.MusicLabels.Add(new MusicLabel { Name = "Warner Music", Description = "Major global music company" });
 
// dbContext.Albums.Add(new Album { Title = "Pop Hits", Genre = "Pop", ReleaseYear = 2021, MusicLabelId = 1 });
// dbContext.Albums.Add(new Album { Title = "Rock Classics", Genre = "Rock", ReleaseYear = 2020, MusicLabelId = 2 });
// dbContext.Albums.Add(new Album { Title = "R&B Vibes", Genre = "R&B", ReleaseYear = 2019, MusicLabelId = 3 });
// dbContext.Albums.Add(new Album { Title = "Indie Sounds", Genre = "Indie", ReleaseYear = 2022, MusicLabelId = 1 });
// dbContext.Albums.Add(new Album { Title = "Country Roads", Genre = "Country", ReleaseYear = 2021, MusicLabelId = 2 });
// dbContext.Albums.Add(new Album { Title = "Soulful Beats", Genre = "Pop", ReleaseYear = 2022, MusicLabelId = 3 });
 
// dbContext.Artists.Add(new Artist { FullName = "John Doe", StageName = "JD", Biography = "Famous pop artist" });
// dbContext.Artists.Add(new Artist { FullName = "Jane Smith", StageName = "JS", Biography = "Rock star with a unique voice" });
// dbContext.Artists.Add(new Artist { FullName = "Michael Johnson", StageName = "MJ", Biography = "Legendary R&B singer" });
// dbContext.Artists.Add(new Artist { FullName = "Emily Clark", StageName = "EC", Biography = "Upcoming indie artist" });
// dbContext.Artists.Add(new Artist { FullName = "Daniel Brown", StageName = "DB", Biography = "Country singer-songwriter" });
// dbContext.Artists.Add(new Artist { FullName = "Olivia White", StageName = "OW", Biography = "Pop artist with a soulful twist" });
// dbContext.Artists.Add(new Artist { FullName = "Sophia Green", StageName = "SG", Biography = "Rock band frontwoman" });
// dbContext.Artists.Add(new Artist { FullName = "Ethan Lee", StageName = "EL", Biography = "Hip hop artist with impactful lyrics" });
// dbContext.Artists.Add(new Artist { FullName = "Ava Wilson", StageName = "AW", Biography = "Classical musician" });
// dbContext.Artists.Add(new Artist { FullName = "Lucas Adams", StageName = "LA", Biography = "Jazz and blues expert" });
 
dbContext.SaveChanges();
 