using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_ef_core.Models;

/*
 * Coach Entity:
 * all fields is required, except the training sessions
 * Salary: monthly salary, defaults to 1500
 * 1:M relation with [TrainingSessions] table
*/

internal class Coach
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; } = 1500m; // monthly salary, standard starting value
    public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
}
