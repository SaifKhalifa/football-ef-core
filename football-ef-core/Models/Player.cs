using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_ef_core.Models;

/*
 * Player Entity:
 * all fields is required, except the training sessions
 * age field have range
 * M:M relation with [TrainingSessions] table
*/ 

internal class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;   
    public byte Age { get; set; }
    public ICollection<TrainingSession>? TrainingSessions { get; set; } = new List<TrainingSession>();
}
