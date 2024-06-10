using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AmourLink.Infrastructure.Helpers;

public class GuidToBytesConverter : ValueConverter<Guid, byte[]>
{
    public GuidToBytesConverter() 
        : base(
            guid => FromGuid(guid),
            bytes => FromBytes(bytes))
    {
    }
    
    public static byte[] FromGuid(Guid uuid)
    {
        var guid = uuid.ToByteArray();
        Array.Reverse(guid, 6, 2);
        Array.Reverse(guid, 4, 2);
        Array.Reverse(guid, 0, 4);
        return guid;
    }

    public static Guid FromBytes(byte[] uuid)
    {
        Array.Reverse(uuid, 0, 4);
        Array.Reverse(uuid, 4, 2);
        Array.Reverse(uuid, 6, 2);
        return new Guid(uuid);
    }
}