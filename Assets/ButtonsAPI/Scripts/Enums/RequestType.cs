using AETOS.Scripts.API;

namespace ButtonsAPI.Enums
{
    public enum RequestType
    {
        [StringValue("POST")]
        Post,
        [StringValue("GET")]
        Get,
        [StringValue("PUT")]
        Put,
        [StringValue("DELETE")]
        Delete
    }
}