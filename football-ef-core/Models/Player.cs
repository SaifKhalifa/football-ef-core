using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_ef_core.Models;

internal class Player
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public short Age { get; set; }
}
