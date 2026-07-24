using DevExpress.Blazor;

namespace DxSchedulerSmartPaste.Models;

public class CustomAppointmentFormInfo : SchedulerAppointmentFormInfo
{
    public CustomAppointmentFormInfo(
        DxSchedulerAppointmentItem appointmentItem,
        DxSchedulerDataStorage dataStorage,
        DxScheduler scheduler)
        : base(appointmentItem, dataStorage, scheduler) { }
}
