namespace FinalProject.Domain.Enums;

public enum OrderState
{
    Canceled = -1,
    WaitingForExpertBid = 1,
    WaitingToChooseExpert = 2,
    WaitingForExpertToArrive = 3,
    WorkStarted = 4,
    WorkFinished = 5,
    Paid = 6
}