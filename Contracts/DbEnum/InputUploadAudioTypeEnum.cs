using System.Runtime.Serialization;

namespace Services.DbEnum;

public enum InputUploadAudioTypeEnum
{
    //[EnumMember(Value = "Base64File")]
    //Base64File = 1,
    [EnumMember(Value = "YoutubeLink")]
    YoutubeLink = 2,
    [EnumMember(Value = "AudioLink")]
    AudioLink = 3,
    [EnumMember(Value = "VideoLink")]
    VideoLink = 4
}