using System.ComponentModel.DataAnnotations;

namespace RestSampleApp.Entities;

public class Item
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}