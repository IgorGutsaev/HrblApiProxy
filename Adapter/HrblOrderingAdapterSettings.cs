namespace Filuet.Hrbl.Ordering.Adapter
{
    public class HrblOrderingAdapterSettings
    {
        public string ApiUri { get; internal set;}
        public string Consumer { get; internal set; }
        public string Login { get; internal set; }
        public string Password { get; internal set; }
        public uint OrganizationId { get; internal set; }
        public string SSOAuthServiceUri { get; internal set; }

        public HrblOrderingAdapterPollRequestSettings PollSettings { get; internal set; }
    }
}
