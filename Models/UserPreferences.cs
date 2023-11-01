using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class UserPreferences
{
    [Key]
    public int Id { get; set; }

    // Foreign key to the user
    public int UserId { get; set; }
    public User User { get; set; }

    public bool ShowMovies { get; set; }
    public bool ShowTVShows { get; set; }
}

}
