namespace MailSender_Lib.Data.BaseEntityes
{
    /// <summary> Именование сущности </summary>
    public abstract class NamedEntity : Entity
    {
        /// <summary> Имя </summary>
        public string Name { get; set; }
    }
}