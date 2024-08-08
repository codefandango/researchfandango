namespace CodeFandango.Flamenco.Models.Participation
{
    public enum ParticipantFieldType
    {
        String = 1,
        Number = 2,
        Boolean = 4,
        Date = 8,
        Time = 16,
        DateTime = 32,
        EmailAddress = 64,
        SurveySelector = 128,
        PhoneNumber = 256,
        PersonallyIdentifiableInformation = 512,
        ValueFromSet = 1024,
        SurveyLevel = 2048,
    }
}