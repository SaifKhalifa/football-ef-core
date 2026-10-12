using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_ef_core.Models;

internal class Coach
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }

    public ICollection<TrainingSession> TrainingSessions { get; set; }
}
