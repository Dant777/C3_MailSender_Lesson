namespace MailSender_Lib.Data.BaseEntityes
{
    /// <summary> Персоналии </summary>
    public abstract class Human : NamedEntity
    {
        /// <summary>Адрес электронной почты </summary>
        public string Address { get; set; }
        public string Description { get; set; }
    }
}