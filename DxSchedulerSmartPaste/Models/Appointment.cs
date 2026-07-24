namespace DxSchedulerSmartPaste.Models;

public class Appointment
{
    public int Id { get; set; }
    public string? Subject { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool AllDay { get; set; }
}
