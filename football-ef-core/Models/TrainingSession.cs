using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_ef_core.Models;

/*
 * TrainingSession Entity:
 * [x] all fields is required.
 * [x] 1:M relation with [Coachs] table
 * [x] M:M relation with [Players] table
 * [x] [Date] field should have default value of today's data
 * [x] [Title] field is unique 
*/

internal class TrainingSession
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();

    public int CoachId { get; set; }

    public Coach Coach { get; set; } = null;
}
