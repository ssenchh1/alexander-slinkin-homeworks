using System.Runtime.Serialization;

namespace Core.Models
{
    [DataContract]
    public class User
    {
        [DataMember(EmitDefaultValue = false)]
        public string firstName;

        [DataMember(EmitDefaultValue = false)]
        public string lastName;

        [DataMember]
        public string points;

        [DataMember(EmitDefaultValue = false)]
        public int Age;
    }
}