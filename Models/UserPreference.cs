using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models 
{
    public class UserPreference
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    public bool ShowMovies { get; set; }

    public bool ShowTVShows { get; set; }

    public User User { get; set; }
}

}
