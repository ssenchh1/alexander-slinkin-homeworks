using System.Runtime.Serialization;

namespace Core.Models
{
    [DataContract]
    public class User
    {
        [DataMember(EmitDefaultValue = false)]
        public string FirstName;

        [DataMember(EmitDefaultValue = false)]
        public string LastName;

        [DataMember]
        public string Points;

        [DataMember(EmitDefaultValue = false)]
        public int Age;
    }
}