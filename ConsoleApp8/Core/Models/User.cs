namespace Core.Models
{
    [DataContract]
    internal class User
    {
        [DataMember]
        internal string firstname;

        [DataMember]
        internal string lastname;

        [DataMember]
        internal string points;

        [DataMember]
        internal int totalpoints;
    }
}