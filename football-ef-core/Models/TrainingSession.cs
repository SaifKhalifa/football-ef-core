using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_ef_core.Models;

[Index(nameof(Title), IsUnique = true)]
internal class TrainingSession
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    public ICollection<Player> Players { get; set; }

    public int CoachId { get; set; }

    public Coach Coach { get; set; }
}
