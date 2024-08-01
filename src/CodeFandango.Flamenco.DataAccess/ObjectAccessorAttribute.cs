
namespace CodeFandango.Flamenco.DataAccess
{
    public class ObjectAccessorAttribute : Attribute
    {
        public Type ObjectType { get; set; }

        public ObjectAccessorAttribute(Type objectType)
        {
            ObjectType = objectType;
        }
    }
}