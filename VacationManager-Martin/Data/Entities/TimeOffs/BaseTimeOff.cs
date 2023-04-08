namespace VacationManager_Martin.Data.Entities.TimeOffs
{
    public class BaseTimeOff
    {
        public int Id { get; set; }
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual bool IsHalfDay { get; set; }

        public bool IsApproved { get; set; }

        public string RequestorId { get; set; }
        public virtual User Requestor { get; set; }
    }
}
