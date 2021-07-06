

namespace Practice9june.Core.Models
{
    public class AudioFile : File
    {
        public int Bitrate { get; set; }

        public int SampleRate { get; set; }

        public int ChannelCount { get; set; }

        public int Duration { get; set; }
    }
}
