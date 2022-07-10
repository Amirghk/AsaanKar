namespace FinalProject.Domain.Enums;

public enum OrderState
{
    WaitingForExpertBid,
    WaitingToChooseExpert,
    WaitingForExpertToArrive,
    WorkStarted,
    WorkFinished,
    Paid
}